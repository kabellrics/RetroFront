using RetroFront.Models.ScreenScraper;
using System;
using System.Collections.Generic;
using System.Text;

namespace RetroFront.Models.ScreenScraper
{
    public class SCSPGameRequest
    {
        public Header header { get; set; }
        public GameSearch.Response response { get; set; }
    }
}
