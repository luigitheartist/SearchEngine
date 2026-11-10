using System.Diagnostics;
using RoverSearch.Models;

namespace RoverSearch.Services;

public class SearchService
{
    private string path = @"./Data/";

    

    /// <summary>
    /// Less naive search implementation
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public SearchResults Search(string query)
    {
        
        string[] queries = query.Split(" ");
        var sw = new Stopwatch();
        sw.Start();

        var results = new List<Result>();

        foreach (string file in Directory.GetFiles(path))
        {
            bool isMatch = true;
            foreach (string q in queries)
            {

                if (!File.ReadAllText(file).Contains(q, StringComparison.OrdinalIgnoreCase))
                {
                  isMatch = false;   
                }

                if (isMatch)
                {
                    Random rand = new Random();
                    string season = File.ReadLines(file).Take(1).First().Replace("season: ", "");
                    string episode = File.ReadLines(file).Skip(1).Take(1).First().Replace("episode: ", "");
                    string title = File.ReadLines(file).Skip(2).Take(1).First().Replace("title: ", "");
                    string randStr = File.ReadLines(file).Skip(rand.Next(40, 140)).Take(1).First()
                        .Replace("title: ", "");
                    var filename = $"Season {season}, Episode {episode}: \'{title}\'";
                    results.Add(new Result { Filename = filename, Description = randStr, ResultFile = Path.GetFileName(file) });
                }
                
            }
        }

        sw.Stop();

        return new SearchResults
        {
            Query = query,
            Results = results,
            Duration = sw.Elapsed
        };
    }
}
