using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Yokins.Salesforce.MCE
{
    public class Auth
    {
        private const string TokenUrlPath = "/v2/token";
        private const string TokenUrlFormat = "https://{0}.auth.marketingcloudapis.com/v2/token";

        public string TokenUrl { get; set; }
        /// <summary>
        /// tokenUrl can be either a full URL or a subdomain.
        /// </summary>
        /// <param name="tokenUrl"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public Auth( string tokenUrl )
        {
            if (string.IsNullOrWhiteSpace(tokenUrl))
                throw new ArgumentNullException(nameof(tokenUrl));

            if( Regex.IsMatch(tokenUrl, @"^[a-zA-Z0-9\-]+$"))
            {
                TokenUrl = string.Format(TokenUrlFormat, tokenUrl);
            }
            else
            {
                TokenUrl = new Uri( new Uri(tokenUrl), TokenUrlPath).ToString();
            }
        }
        public AccessToken GetAccessToken( string accountId, string clientId, string clientSecret )
        {
            if (string.IsNullOrWhiteSpace(clientId))
                throw new ArgumentException("clientId must not be empty", nameof(clientId));
            if (string.IsNullOrWhiteSpace(clientSecret))
                throw new ArgumentException("clientSecret must not be empty", nameof(clientSecret));

            var http = APIClientBase._sharedHttpClient;

            // Marketing Cloud v2 token endpoint expects JSON body with camelCase keys.
            var requestBody = new
            {
                grant_type = "client_credentials",
                client_id = clientId,
                client_secret = clientSecret,
                account_id = accountId
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = http.PostAsync(TokenUrl, content).GetAwaiter().GetResult();
            var responseBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            if (!response.IsSuccessStatusCode)
                throw new InvalidOperationException($"Token request failed ({response.StatusCode}): {responseBody}");

            return AccessToken.FromJson(responseBody);
        }

        public void GetBaseUrls()
        {
            throw new NotImplementedException();
        }

        public void GetUserInfo()
        {
            throw new NotImplementedException();
        }
    }
}
