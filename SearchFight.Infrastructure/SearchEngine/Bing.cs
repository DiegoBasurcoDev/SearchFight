using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchFight.Infrastructure.Extension;
using SearchFight.Models;

namespace SearchFight.Infrastructure.SearchEngine
{
    public class Bing : SearchEngine
    {
        public Bing()
        {
            request = new Request()
            {
                Title = "Bing"
            };
        }

        public async Task LoadResponse(string searchValue)
        {
            currentResult.Text = searchValue;

            var requestUri = new Uri("https://www.bing.com/search");
            requestUri = requestUri.AddQuery("q", searchValue);

            await LoadHttpResponse(requestUri);
        }

        public void LoadNumber()
        {
            string regexPattern = @"(?:<span)(?:[^>]*)(?:class=\""sb_count\"")(?:[^>]*)(?:>)(.*?)(?:</span>)";
            string splitSeparator = " ";
            LoadResultNumber(regexPattern, splitSeparator);
        }
    }
}
