using System;
using System.Collections.Generic;
using System.Text;

namespace RetroFront.Models.IGDB
{
    public class Company
    {
        public int id { get; set; }
        public int change_date { get; set; }
        public int change_date_category { get; set; }
        public int changed_company_id { get; set; }
        public int created_at { get; set; }
        public List<int> developed { get; set; }
        public int logo { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
        public Int64 start_date { get; set; }
        public int start_date_category { get; set; }
        public int updated_at { get; set; }
        public string url { get; set; }
        public string checksum { get; set; }
    }
}
