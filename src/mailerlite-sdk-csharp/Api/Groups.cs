using mailerlite_sdk_csharp.Common;
using mailerlite_sdk_csharp.Exceptions;
using System.Collections.Generic;
using System.IO;

namespace mailerlite_sdk_csharp.Api
{
    public class Groups : ApiBase
    {
        private string Endpoint { get; }
        private string Url { get; }
        private HttpClient Http { get; }

        public Groups(HttpClient http)
        {
            this.Endpoint = "groups";
            this.Http = http;
            this.Url = $"{ this.BaseUrl}{ this.Endpoint}/";
        }

        public Stream List(int? limit, int? offset, string filters)
        {
            return this.Http.Get(this.AddQueryParams(Url, limit, offset, filters)).ResponseStream;
        }

        public Stream GetById(int groupId)
        {
            return this.Http.Get($"{Url}/{groupId}").ResponseStream;
        }

        public Stream Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new MailerLiteException("Group name cannot be empty");
            }

            return this.Http.Post(Url, new
            {
                name = name
            }, HttpContentTypes.ApplicationJson).ResponseStream;
        }

        public Stream Update(int groupId, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new MailerLiteException("Group name cannot be empty");
            }

            return this.Http.Put($"{Url}/{groupId}", new
            {
                name = name
            }, HttpContentTypes.ApplicationJson).ResponseStream;
        }

        public Stream Delete(int groupId)
        {
            return this.Http.Delete($"{Url}/{groupId}").ResponseStream;
        }

        public Stream GetSubscribersByGroupId(int groupId, int? limit, int? offset, string filters, CampaignSubscriberType? type)
        {
            return this.Http.Get(this.AddQueryParams($"{Url}/{groupId}/subscribers", limit, offset, filters, type)).ResponseStream;
        }

        public Stream GetSubscribersCountByGroupId(int groupId)
        {
            return this.Http.Get($"{Url}/{groupId}/subscribers/count").ResponseStream;
        }

        public Stream GetSubscribersByTypeByGroupId(int groupId, CampaignSubscriberType type, string filters,
            int offset, int limit)
        {
            return this.Http.Get(this.AddQueryParams($"{Url}/{groupId}/subscribers/{type.ToString()}", limit: limit,
                offset: offset, filters: filters)).ResponseStream;
        }

        public Stream GetSubscribersCountByTypeByGroupId(int groupId, CampaignSubscriberType type)
        {
            return this.Http.Get($"{Url}/{groupId}/subscribers/{type.ToString()}/count").ResponseStream;
        }

        public Stream GetSubscribersByGroupId(int groupId, int subscriberId)
        {
            return this.Http.Get($"{Url}/{groupId}/subscribers/{subscriberId}").ResponseStream;
        }

        public Stream DeleteSubscriberByGroupId(int groupId, int? subscriberId, string email)
        {
            if (subscriberId.HasValue)
            {
                return this.Http.Delete($"{Url}/{groupId}/subscribers/{subscriberId}").ResponseStream;
            }
            else
            {
                return this.Http.Delete($"{Url}/{groupId}/subscribers/{email}").ResponseStream;
            }
        }

        public Stream AddSubscriberToGroup(int groupId, string email, string name, List<string> fields,
            bool resubcribe, bool autoresponders, AddCampaignSubscriberType type)
        {
            return this.Http.Post($"{Url}/{groupId}/subscribers", new
            {
                email = email,
                name = name,
                fields = fields,
                resubcribe = resubcribe,
                autoresponders = autoresponders,
                type = type.ToString()
            }, HttpContentTypes.ApplicationJson).ResponseStream;
        }
    }
}
