using EasyHttp.Http;
using mailerlite_sdk_csharp.Common;
using mailerlite_sdk_csharp.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Utils;

namespace mailerlite_sdk_csharp.Api
{
    public class Campaigns : ApiBase
    {
        private string Endpoint { get; }
        private string Url { get; }
        private HttpClient Http { get; }

        public Campaigns(HttpClient http)
        {
            this.Endpoint = "campaigns";
            this.Http = http;
            this.Url = $"{ this.BaseUrl}{ this.Endpoint}/";
        }

        public Stream Status(CampaignStatus status, int? limit, int? offset, ApiQueryOrder? order)
        {
            this.Http.Get(this.AddQueryParams($"{Url}{status.ToString()}", limit, offset, order));
            return this.Http.Response.ResponseStream;
        }

        public int StatusCount(CampaignStatus status, int? limit, int? offset, ApiQueryOrder? order)
        {
            HttpResponse result = this.Http.Get($"{this.BaseUrl}{this.Endpoint}/{status.ToString()}/count");
            return result.DynamicBody;
        }

        public Stream Create(CampaignType type, string subject, string from, string fromName, string language,
            IEnumerable<int> groups, object abSettings)
        {
            if(type == CampaignType.regular)
            {
                if(subject.IsNullOrEmpty())
                {
                    throw new MailerLiteException("If the campaign type is regular the subject is required.");
                }
            }

            if(type == CampaignType.ab)
            {
                if(abSettings.IsNull())
                {
                    throw new MailerLiteException("If the campaign type is ab the ab_settings object is required.");
                }
            }

            if(groups.Count() == 0)
            {
                throw new MailerLiteException("Groups ids are required");
            }

            return this.Http.Post(Url, new {
                subject = subject,
                from = from,
                fromName = fromName,
                language = language,
                groups = groups,
                abSettings = abSettings
            }, HttpContentTypes.ApplicationJson).ResponseStream;
        }

        public Stream Delete(int campaignId)
        {
            return this.Http.Delete($"{Url}/{campaignId}").ResponseStream;
        }

        public Stream Content(int campaignId, string html, string plain, bool autoInline)
        {
            if(html.IsNullOrEmpty())
            {
                throw new MailerLiteException("HTML param cannot be empty");
            }
            if (plain.IsNullOrEmpty())
            {
                throw new MailerLiteException("HTML param cannot be empty");
            }

            return this.Http.Put($"{Url}/{campaignId}/content", new {
                html = html,
                plain = plain,
                auto_inline = autoInline
            }, HttpContentTypes.ApplicationJson).ResponseStream;
        }

        public Stream Actions(int campaignId, CampaignActions action, int type, string followupSchedule, bool analytics,
            DateTime date, int timezoneId, DateTime followupDate, int followupTimezoneId)
        {
            return this.Http.Post($"{Url}/{campaignId}/actions/{action.ToString()}", new {
                type = type,
                followupSchedule = followupSchedule,
                analytics = analytics ? 1 : 0,
                date = date.ToString("yyyy-MM-dd HH:mm:ss"),
                timezone_id = timezoneId,
                followup_date = followupDate.ToString("yyyy-MM-dd HH:mm:ss"),
                followup_timezone_id = followupTimezoneId
            }, HttpContentTypes.ApplicationJson).ResponseStream;
        }
    }
}
