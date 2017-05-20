using mailerlite_sdk_csharp.Common;

namespace mailerlite_sdk_csharp.Api
{
    public class ApiBase
    {
        protected string BaseUrl { get; } = $"{ApiConstants.BASE_URL}{ApiConstants.VERSION}/ ";

        protected string AddQueryParams(string url, int? limit, int? offset, ApiQueryOrder? order)
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

            return url;
        }
    }
}
