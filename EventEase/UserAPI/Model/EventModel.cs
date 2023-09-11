namespace UserAPI.Model
{


    public class EventModel
    {
        public int EventId { get; set; }
        public string Event_Name { get; set; }
        public string Description { get; set; }
        public string Artists { get; set; }
        public string Category { get; set; }
        public bool Is_Free { get; set; }
        public bool Mode { get; set; }
        public string Venue { get; set; }
        public bool Is_Postponed { get; set; }
        public bool Is_Cancelled { get; set; }
        public DateTime EventStartingDate { get; set; }
        public TimeSpan StartingTime { get; set; }
        public TimeSpan EndingTime { get; set; }
        public DateTime EventEndingDate { get; set; }
        public string Language { get; set; }
        public decimal TicketPrice { get; set; }
        public byte[] ArtistPhoto { get; set; }
        public string ArtistSocialMediaURLs { get; set; }
        public string City { get; set; }
        public int TicketsAvailable { get; set; }
        public string Status { get; set; }
        public int NoOfBookings { get; set; }
        public int NoOfInterested { get; set; }
    }

}
