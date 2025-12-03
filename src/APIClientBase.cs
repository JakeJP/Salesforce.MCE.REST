using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading;

namespace Yokinsoft.Salesforce.MCE
{
    public abstract class APIClientBase
    {
        protected AccessToken AccessToken { get; private set; }
        protected APIClientBase( AccessToken accessToken)
        {
            AccessToken = accessToken ?? throw new ArgumentNullException( nameof( accessToken ) );
        }

        protected TResponse Get<TResponse>(string url, Dictionary<string,string> qsData = null )
        {
            if (AccessToken == null)
                throw new InvalidOperationException("AccessToken is required.");
            if (!AccessToken.IsValid)
                throw new InvalidOperationException("AccessToken is not valid.");

            if (qsData != null && qsData.Count > 0)
            {
                var sb = new StringBuilder();
                sb.Append(url.Contains("?") ? "&" : "?");
                foreach (var kvp in qsData.Where(kv => !string.IsNullOrEmpty(kv.Value)))
                {
                    sb.Append(Uri.EscapeDataString(kvp.Key));
                    sb.Append("=");
                    sb.Append(Uri.EscapeDataString(kvp.Value));
                    sb.Append("&");
                }
                // Remove trailing '&'
                sb.Length--;
                url += sb.ToString();
            }
            return SendWithBody<TResponse>(HttpMethod.Get, url, null);
        }
       
        protected TResponse Post<TResponse>(string url, object contentObject)
        {
            return SendWithBody<TResponse>( HttpMethod.Post, url, contentObject );
        }
        protected TResponse Put<TResponse>(string url, object contentObject = null )
        {
            return SendWithBody<TResponse>(HttpMethod.Put, url, contentObject);
        }
        protected TResponse Delete<TResponse>(string url, object contentObject = null)
        {
            return SendWithBody<TResponse>(HttpMethod.Delete, url, contentObject );
        }
        protected TResponse Patch<TResponse>(string url, object contentObject = null)
            => SendWithBody<TResponse>(new HttpMethod("PATCH"), url, contentObject);

        internal static HttpClient _sharedHttpClient = new HttpClient();
        private TResponse SendWithBody<TResponse>(HttpMethod method, string url, object contentObject)
        {
            if (AccessToken == null)
                throw new InvalidOperationException("AccessToken is required.");
            if (!AccessToken.IsValid)
                throw new InvalidOperationException("AccessToken is not valid.");

            if( url.StartsWith( "/" ))
                url = AccessToken.RestInstanceUrl.TrimEnd(new [] { '/' }) + url;

            var http = _sharedHttpClient;

            var request = new HttpRequestMessage(method, url)
            {
                Content = contentObject == null ? null :
                    new StringContent(
                        JsonSerializer.Serialize(contentObject),
                        Encoding.UTF8,
                        "application/json"),
            };
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AccessToken.Token);
            var resp = http.SendAsync(request, CancellationToken.None).GetAwaiter().GetResult();
            var body = resp.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            if (!resp.IsSuccessStatusCode)
                throw new InvalidOperationException($"Failed to get data ({resp.StatusCode}): {body}");
            var opts = new JsonSerializerOptions( JsonSerializerDefaults.Web )
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                //PropertyNameCaseInsensitive = true,
                AllowTrailingCommas = true
            };
            return string.IsNullOrEmpty(body) ? default(TResponse)
                : JsonSerializer.Deserialize<TResponse>(body, opts);
        }
    }
}
