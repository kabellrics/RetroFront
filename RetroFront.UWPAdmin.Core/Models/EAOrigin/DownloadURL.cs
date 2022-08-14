using System;

namespace RetroFront.UWPAdmin.EAOrigin
{
    public class DownloadURL
    {
        public string buildReleaseVersion { get; set; }
        public string buildMetaData { get; set; }
        public string downloadURL { get; set; }
        public string downloadURLType { get; set; }
        public DateTime? effectiveDate { get; set; }
    }
}
