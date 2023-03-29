using Microsoft.AspNetCore.Mvc;
using TravelClient.Models;
using Newtonsoft.Json;

namespace TravelClient.Controllers;

public class DestinationsController : Controller
{
  // [Route("Destinations/Page/{page}")]
  // public async Task<IActionResult> Index(int page = 1)
  // {
  //   HttpClient client = new HttpClient();
  //   HttpResponseMessage response = await client.GetAsync($"https://localhost:5001/Destinations/page/{page}");
  //   response.EnsureSuccessStatusCode();
  //   string responseBody = await response.Content.ReadAsStringAsync();
  //   DestinationResponse destinationResponse = JsonConvert.DeserializeObject<DestinationResponse>(responseBody);

  //   return View(destinationResponse);
  // }

  public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 5)
{
    var httpClient = new HttpClient();
    var response = await httpClient.GetAsync($"https://localhost:5001/api/destinations?page={pageNumber}&pageSize={pageSize}");
    var content = await response.Content.ReadAsStringAsync();
    var destinations = JsonConvert.DeserializeObject<List<Destination>>(content);

    ViewBag.PageNumber = pageNumber;
    ViewBag.PageSize = pageSize;
    ViewBag.TotalPages = (int)Math.Ceiling((double)destinations.Count / pageSize);

    return View(destinations);
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