using SearchFight.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using SearchFight.Infrastructure.Extension;

namespace SearchFight.Infrastructure.SearchEngine
{
    public abstract class SearchEngine
    {
        protected Result currentResult;
        protected Result winnerResult;
        protected Request request;
        protected Response response;
        protected string winnerTitle;

        public SearchEngine()
        {
            currentResult = new Result();
            winnerResult = new Result();
        }

        public Result GetCurrentResult()
        {
            return currentResult;
        }

        public Result GetWinnerResult()
        {
            return winnerResult;
        }

        public Request GetRequest()
        {
            return request;
        }

        public void LoadResultNumber(string regexPattern, string splitSeparator = " ")
        {
            string currentTitle = currentResult.Text;
            long winnerNumber = winnerResult.Number;
            var responseCharacters = response.Characters.ToString();

            responseCharacters.TryToLong(out long resultNumber, regexPattern, splitSeparator);

            currentResult.Number = resultNumber;

            if (resultNumber > winnerNumber)
            {
                winnerTitle = currentTitle;
                winnerResult = currentResult;
            }
            else
            {
                winnerResult.Text = winnerTitle;
                winnerResult.Number = winnerNumber;
            }
            currentResult.Number = resultNumber;
        }

        public async Task LoadHttpResponse(Uri uri)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            httpWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36";
            httpWebRequest.Timeout = 10000;

            await LoadResponse(httpWebRequest);
        }

        public async Task LoadResponse(HttpWebRequest httpWebRequest)
        {
            using (var response = await httpWebRequest.GetResponseAsync())
            {
                using (var responseStream = response.GetResponseStream())
                {
                    using (var streamReader = new StreamReader(responseStream))
                    {
                        this.response = new Response
                        {
                            Characters = await streamReader.ReadToEndAsync(),
                            Encoding = streamReader.CurrentEncoding,
                            Uri = httpWebRequest.RequestUri
                        };
                    }
                }
            }
        }

    }
}
