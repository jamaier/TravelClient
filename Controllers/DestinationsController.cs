using Microsoft.AspNetCore.Mvc;
using TravelClient.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;
using System.Diagnostics;

namespace TravelClient.Controllers;

public class DestinationsController : Controller
{
  public async Task<IActionResult> Index(int page = 1, int pageSize = 6)
  {
    Destination destination = new Destination();
    List<Destination> destList = new List<Destination> { };
    using (var httpClient = new HttpClient())
    {
      using (var response = await httpClient.GetAsync($"https://localhost:5001/api/Destinations?page={page}&pagesize={pageSize}"))
      {
        var destContent = await response.Content.ReadAsStringAsync();
        JArray destArray = JArray.Parse(destContent);
        destList = destArray.ToObject<List<Destination>>();
      }
    }

    ViewBag.TotalPages = destList.Count();
    //page number inside the url
    ViewBag.CurrentPage = page;
    //amnt of items on the page
    ViewBag.PageSize = pageSize;
     //the amount of destinations returned from our database
    // ViewBag.Pages = pageCount;

    return View(destList);
  }

  public IActionResult Details(int id)
  {
    Destination destination = Destination.GetDetails(id);
    return View(destination);
  }

  public ActionResult Create()
  {
    return View();
  }

  [HttpPost]
  public ActionResult Create(Destination destination)
  {
    Destination.Post(destination);
    return RedirectToAction("Index");
  }

  public ActionResult Edit(int id)
  {
    Destination destination = Destination.GetDetails(id);
    return View(destination);
  }

  [HttpPost]
  public ActionResult Edit(Destination destination)
  {
    Destination.Put(destination);
    return RedirectToAction("Details", new { id = destination.DestinationId });
  }

  public ActionResult Delete(int id)
  {
    Destination destination = Destination.GetDetails(id);
    return View(destination);
  }

  [HttpPost, ActionName("Delete")]
  public ActionResult DeleteConfirmed(int id)
  {
    Destination.Delete(id);
    return RedirectToAction("Index");
  }
}