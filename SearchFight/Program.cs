using SearchFight.Infrastructure.SearchEngine;
using SearchFight.Models;
using System;
using System.Threading.Tasks;

namespace SearchFight
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string[] searchValues = args;

            if (searchValues.Length <= 0)
            {
                Console.WriteLine("Debe ingresar los terminos de busqueda");
            }
            else
            {
                try
                {
                    var yahoo = new Yahoo();
                    var bing = new Bing();

                    var searchEngines = new SearchEngine[]
                    {
                        yahoo,
                        bing
                    };

                    for (int i = 0; i < searchValues.Length; i++)
                    {
                        Console.WriteLine(searchValues[i] + ":");
                        string strSearchValues = searchValues[i];

                        await yahoo.LoadResponse(strSearchValues);
                        yahoo.LoadNumber();

                        await bing.LoadResponse(strSearchValues);
                        bing.LoadNumber();

                        for (int j = 0; j < searchEngines.Length; j++)
                        {
                            var searchEngine = searchEngines[j];
                            var currentResult = searchEngine.GetCurrentResult();
                            var request = searchEngine.GetRequest();

                            string title = request.Title;
                            long number = currentResult.Number;

                            Console.Write(title + ": " + number + " ");
                        }

                        Console.WriteLine("\r\n");
                    }

                    Result totalWinnerResult = null;

                    for (int i = 0; i < searchEngines.Length; i++)
                    {
                        var searchEngine = searchEngines[i];
                        var winnerResult = searchEngine.GetWinnerResult();
                        var request = searchEngine.GetRequest();

                        if (totalWinnerResult == null || totalWinnerResult.Number < totalWinnerResult.Number)
                        {
                            totalWinnerResult = winnerResult;
                        }

                        string engineTitle = request.Title;
                        string valueTitle = winnerResult.Text;

                        Console.WriteLine(engineTitle + " winner: " + valueTitle);
                    }

                    string totalWinnerResultName = totalWinnerResult.Text;
                    Console.WriteLine("Total winner: " + totalWinnerResultName);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace + " : " + ex.Message);
                }
            }
        }
    }
}
