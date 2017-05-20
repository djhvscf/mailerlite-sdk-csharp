using EasyHttp.Http;
using mailerlite_sdk_csharp.Common;
using mailerlite_sdk_csharp.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

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
            if(name.IsNullOrEmpty())
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
            if (name.IsNullOrEmpty())
            {
                throw new MailerLiteException("Group name cannot be empty");
            }

            return this.Http.Put($"{Url}/{groupId}", new {
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
    }
}
