using RoverSearch.Models;
using System.Diagnostics;
using RoverSearch.Services;
using Microsoft.AspNetCore.Mvc;

namespace RoverSearch.Controllers;
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly SearchService _search;

    public HomeController(ILogger<HomeController> logger, SearchService search)
    {
        _logger = logger;
        _search = search;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Search(string query)
    {
        var results = _search.Search(query);

        return View(results);
    }

    public IActionResult Privacy()
    {
        return Redirect("/images/rickroll.webm");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
