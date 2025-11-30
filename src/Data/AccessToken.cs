using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Yokins.Salesforce.MCE
{
    public class AccessToken
    {
        [JsonIgnore]
        private bool userProvidedToken = false;

        [JsonPropertyName("access_token")]
        public string Token { get; set; }

        // TokenExpiry is computed from ExpiresIn and IssuedAt; do not serialize it directly.
        [JsonIgnore]
        public DateTime TokenExpiry { get; private set; }

        private int? _expiresIn;
        [JsonPropertyName("expires_in")]
        public int? ExpiresIn
        {
            get => _expiresIn;
            set
            {
                _expiresIn = value;
                if (value.HasValue)
                {
                    // If IssuedAt is set use it, otherwise use now as issue time.
                    var issued = IssuedAt ?? DateTime.UtcNow;
                    TokenExpiry = issued.AddSeconds(value.Value);
                }
            }
        }

        private DateTime? _issuedAt;
        // `issued_at` in some SFMC responses is a milliseconds-since-epoch string;
        // System.Text.Json will attempt to parse into DateTime when possible.
        [JsonPropertyName("issued_at")]
        public DateTime? IssuedAt
        {
            get => _issuedAt;
            set
            {
                _issuedAt = value;
                if (_expiresIn.HasValue && value.HasValue)
                    TokenExpiry = value.Value.AddSeconds(_expiresIn.Value);
            }
        }

        [JsonPropertyName("soap_instance_url")]
        public string SoapInstanceUrl { get; set; }

        [JsonPropertyName("rest_instance_url")]
        public string RestInstanceUrl { get; set; }

        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }

        // Preserve original raw JSON if available (not required for deserialization)
        [JsonIgnore]
        public string RawJson { get; set; }

        // Backwards-compatible constructor that treated expiracy as seconds string
        public AccessToken(string token, string expiracy)
        {
            Token = token;
            if (double.TryParse(expiracy, out var seconds))
            {
                ExpiresIn = (int)seconds;
                TokenExpiry = DateTime.UtcNow.AddSeconds(seconds);
            }
            else
            {
                if (DateTime.TryParse(expiracy, out var dt))
                {
                    TokenExpiry = dt.ToUniversalTime();
                }
                else
                {
                    TokenExpiry = DateTime.MinValue;
                }
            }
        }

        // User provided token: considered always valid (keeps previous behavior)
        public AccessToken(string token)
        {
            Token = token;
            userProvidedToken = true;
        }

        // Public parameterless ctor so System.Text.Json can deserialize into this type
        public AccessToken() { }

        // Parse Salesforce / Marketing Cloud token JSON (keeps previous tolerant parsing)
        public static AccessToken FromJson(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                throw new ArgumentException("json must not be empty", nameof(json));

            var at = new AccessToken
            {
                RawJson = json
            };

            try
            {
                var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;

                if (root.TryGetProperty("access_token", out var accessTokenProp) ||
                    root.TryGetProperty("accessToken", out accessTokenProp))
                {
                    at.Token = accessTokenProp.GetString();
                }

                if (root.TryGetProperty("expires_in", out var expiresInProp) ||
                    root.TryGetProperty("expiresIn", out expiresInProp))
                {
                    if (expiresInProp.ValueKind == JsonValueKind.Number && expiresInProp.TryGetInt32(out var sec))
                    {
                        at.ExpiresIn = sec;
                    }
                    else if (expiresInProp.ValueKind == JsonValueKind.String && int.TryParse(expiresInProp.GetString(), out sec))
                    {
                        at.ExpiresIn = sec;
                    }
                }

                if (root.TryGetProperty("issued_at", out var issuedAtProp) || root.TryGetProperty("issuedAt", out issuedAtProp))
                {
                    var issuedStr = issuedAtProp.ValueKind == JsonValueKind.String ? issuedAtProp.GetString() : issuedAtProp.ToString();
                    if (long.TryParse(issuedStr, out var ms))
                    {
                        at.IssuedAt = DateTimeOffset.FromUnixTimeMilliseconds(ms).UtcDateTime;
                    }
                    else if (DateTime.TryParse(issuedStr, out var dt))
                    {
                        at.IssuedAt = dt.ToUniversalTime();
                    }
                }
                else
                {
                    at.IssuedAt = DateTime.UtcNow;
                }

                if (root.TryGetProperty("soap_instance_url", out var soapInstanceUrlProp) || root.TryGetProperty("instanceUrl", out soapInstanceUrlProp))
                {
                    at.SoapInstanceUrl = soapInstanceUrlProp.GetString();
                }

                if (root.TryGetProperty("rest_instance_url", out var restInstanceUrlProp) || root.TryGetProperty("instanceUrl", out restInstanceUrlProp))
                {
                    at.RestInstanceUrl = restInstanceUrlProp.GetString();
                }

                if (root.TryGetProperty("token_type", out var tokenTypeProp) || root.TryGetProperty("tokenType", out tokenTypeProp))
                {
                    at.TokenType = tokenTypeProp.GetString();
                }
            }
            catch (JsonException ex)
            {
                throw new ArgumentException("Invalid JSON for Salesforce token response", nameof(json), ex);
            }

            return at;
        }

        public string ToJson()
        {
            var map = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(Token))
                map["access_token"] = Token;

            if (ExpiresIn.HasValue)
            {
                map["expires_in"] = ExpiresIn.Value;
            }
            else if (TokenExpiry != default && TokenExpiry != DateTime.MinValue)
            {
                var remaining = (int)Math.Max(0, Math.Floor((TokenExpiry - DateTime.UtcNow).TotalSeconds));
                map["expires_in"] = remaining;
            }

            if (IssuedAt.HasValue)
            {
                var ms = new DateTimeOffset(IssuedAt.Value).ToUnixTimeMilliseconds();
                map["issued_at"] = ms.ToString();
            }

            if (!string.IsNullOrEmpty(SoapInstanceUrl))
                map["soap_instance_url"] = SoapInstanceUrl;

            if (!string.IsNullOrEmpty(RestInstanceUrl))
                map["rest_instance_url"] = RestInstanceUrl;

            if (!string.IsNullOrEmpty(TokenType))
                map["token_type"] = TokenType;

            var options = new JsonSerializerOptions
            {
                WriteIndented = false,
#if true || NET5_0_OR_GREATER
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
#else
                IgnoreNullValues = true
#endif
            };

            return JsonSerializer.Serialize(map, options);
        }

        public bool IsValid
        {
            get
            {
                if (userProvidedToken)
                    return true;

                if (TokenExpiry == default || TokenExpiry == DateTime.MinValue)
                    return false;

                return (TokenExpiry - DateTime.UtcNow).TotalSeconds > 60;
            }
        }
    }
}
