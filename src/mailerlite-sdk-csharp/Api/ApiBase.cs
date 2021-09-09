using mailerlite_sdk_csharp.Common;

namespace mailerlite_sdk_csharp.Api
{
    public class ApiBase
    {
        protected string BaseUrl { get; } = $"{ApiConstants.BASE_URL}{ApiConstants.VERSION}/ ";

        protected string AddQueryParams(string url, int? limit, int? offset, ApiQueryOrder? order, 
            string filters, CampaignSubscriberType? type)
        {
            if (limit.HasValue)
            {
                url = $"{url}?limit={limit}";
            }

            if (offset.HasValue)
            {
                if (!url.Contains("?"))
                {
                    url = $"{url}?offset={offset}";
                }
                else
                {
                    url = $"{url}&offset={offset}";
                }
            }

            if (order.HasValue)
            {
                if (!url.Contains("?"))
                {
                    url = $"{url}?order={order}";
                }
                else
                {
                    url = $"{url}&order={order}";
                }
            }

            if (type.HasValue)
            {
                if (!url.Contains("?"))
                {
                    url = $"{url}?type={type}";
                }
                else
                {
                    url = $"{url}&type={type}";
                }
            }

            if (!string.IsNullOrWhiteSpace(filters))
            {
                if (!url.Contains("?"))
                {
                    url = $"{url}?filters={filters}";
                }
                else
                {
                    url = $"{url}&filters={filters}";
                }
            }

            return url;
        }

        protected string AddQueryParams(string url, int? limit, int? offset, ApiQueryOrder? order)
        {
            return this.AddQueryParams(url, limit, offset, order, string.Empty, null);
        }

        protected string AddQueryParams(string url, int? limit, int? offset, string filters)
        {
            return this.AddQueryParams(url, limit, offset, null, filters, null);
        }

        protected string AddQueryParams(string url, int? limit, int? offset, string filters, CampaignSubscriberType? type)
        {
            return this.AddQueryParams(url, limit, offset, null, filters, type);
        }
    }
}
