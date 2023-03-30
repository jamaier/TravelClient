namespace TravelClient.Models;

    public class DestinationResponse
    {
        public List<Destination> Destinations { get; set; } = new List<Destination>();
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }