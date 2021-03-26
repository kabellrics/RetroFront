namespace RetroFront.Models.EAOrigin
{
    public class Software
    {
        public string softwareId { get; set; }
        public FulfillmentAttributes fulfillmentAttributes { get; set; }
        public DownloadURLs downloadURLs { get; set; }
        public string softwarePlatform { get; set; }
    }
}
