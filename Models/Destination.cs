using System.Collections.Generic;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;

namespace TravelClient.Models
{
  public class Destination
  {
    public int DestinationId { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Review { get; set; }
    public int Rating { get; set; }
    public string UserName { get; set; }

    public static Destination[] GetDestinations()
    {
      Task<string> apiCallTask = ApiHelper.GetAll();
      string result =  apiCallTask.Result;
      
      JArray jsonResponse = JArray.Parse(result);
      List<Destination> destinationList = JsonConvert.DeserializeObject<List<Destination>>(jsonResponse.ToString());

      return destinationList.ToArray();
    }
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

    public static Destination GetDetails(int id)
    {
      var apiCallTask = ApiHelper.Get(id);
      var result = apiCallTask.Result;

      JObject jsonResponse = JObject.Parse(result);
      Destination destination = JsonConvert.DeserializeObject<Destination>(jsonResponse.ToString());

      return destination;
    }

    public static void Post(Destination destination)
    {
      string jsonDestination = JsonConvert.SerializeObject(destination);
      ApiHelper.Post(jsonDestination);
    }

    public static void Put(Destination destination)
    {
      string jsonDestination = JsonConvert.SerializeObject(destination);
      ApiHelper.Put(destination.DestinationId, jsonDestination);
    }

    public static void Delete(int id)
    {
      ApiHelper.Delete(id);
    }
  }
}