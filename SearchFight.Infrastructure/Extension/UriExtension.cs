using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SearchFight.Infrastructure.Extension
{
    public static class UriExtension
    {
        public static Uri AddQuery(this Uri uri, string queryName, string queryValue)
        {
            var nameValueCollection = HttpUtility.ParseQueryString(uri.Query);

            nameValueCollection.Remove(queryName);
            nameValueCollection.Add(queryName, queryValue);

            var uriBuilder = new UriBuilder(uri);

            if (nameValueCollection.Count <= 0)
            {
                uriBuilder.Query = string.Empty;
            }
            else
            {
                var stringBuilder = new StringBuilder();

                for (var i = 0; i < nameValueCollection.Count; i++)
                {
                    var key = nameValueCollection.GetKey(i);
                    key = HttpUtility.UrlEncode(key);

                    var queryKey = key != null ? $"{key}=" : string.Empty;
                    var values = nameValueCollection.GetValues(i);

                    if (stringBuilder.Length > 0)
                    {
                        stringBuilder.Append("&");
                    }

                    if (values == null || values.Length <= 0)
                    {
                        stringBuilder.Append(queryKey);
                    }
                    else
                    {
                        for (var j = 0; j < values.Length; j++)
                        {
                            if (j > 0)
                            {
                                stringBuilder.Append("&");
                            }

                            stringBuilder.Append(queryKey);
                            stringBuilder.Append(HttpUtility.UrlEncode(values[j]));
                        }
                    }
                }

                uriBuilder.Query = stringBuilder.ToString();
            }

            return uriBuilder.Uri;
        }

        public static string[] GetQueryValue(this Uri uri, string queryName)
        {
            var nameValueCollection = HttpUtility.ParseQueryString(uri.Query);

            return nameValueCollection.GetValues(queryName);
        }
    }
}
