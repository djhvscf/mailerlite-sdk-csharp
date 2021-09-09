using mailerlite_sdk_csharp.Api;
using mailerlite_sdk_csharp.Common;
using mailerlite_sdk_csharp.Exceptions;

namespace mailerlite_sdk_csharp
{
    public class MailerLite
    {
        public string ApiKey { get; }
        private HttpClient Http { get; }
        public Campaigns Campaigns { get; }

        public MailerLite(string apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new MailerLiteException("API key is not provided");
            }
            this.ApiKey = apiKey;

            this.Http = new HttpClient();
            this.Http.Request.AddExtraHeader("User-Agent", $"{ApiConstants.SDK_USER_AGENT}/{ApiConstants.SDK_VERSION}");
            this.Http.Request.AddExtraHeader("X-MailerLite-ApiKey", this.ApiKey);
            this.Http.Request.AddExtraHeader("Content-Type", HttpContentTypes.ApplicationJson);
            this.Http.Request.Accept = HttpContentTypes.ApplicationJson;

            Campaigns = new Campaigns(this.Http);
        }
    }
}
