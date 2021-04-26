using SearchFight.Infrastructure.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchFight.Models;

namespace SearchFight.Infrastructure.SearchEngine
{
    public class Yahoo : SearchEngine
    {
        public Yahoo()
        {
            request = new Request()
            {
                Title = "Yahoo"
            };
        }

        public async Task LoadResponse(string searchValue)
        {
            currentResult.Text = searchValue;

            var requestUri = new Uri("https://search.yahoo.com/search");
            requestUri = requestUri.AddQuery("q", searchValue);

            await LoadHttpResponse(requestUri);
        }

        public void LoadNumber()
        {
            string regexPattern = @"(?:<div)(?:[^>]*)(?:class=\""compPagination\"")(?:.*?)(?:<span)(?:[^>]*)(?:>)(.*?)(?:</span>)";
            string splitSeparator = " ";
            LoadResultNumber(regexPattern, splitSeparator);
        }
    }
}
