﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RetroFront.Models
{
    public class SteamResult
    {
    }
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class PcRequirements
    {
        public string minimum { get; set; }
        public string recommended { get; set; }
    }

    public class Demo
    {
        public int appid { get; set; }
        public string description { get; set; }
    }

    public class PriceOverview
    {
        public string currency { get; set; }
        public int initial { get; set; }
        public int final { get; set; }
        public int discount_percent { get; set; }
        public string initial_formatted { get; set; }
        public string final_formatted { get; set; }
    }

    public class Sub
    {
        public int packageid { get; set; }
        public string percent_savings_text { get; set; }
        public int percent_savings { get; set; }
        public string option_text { get; set; }
        public string option_description { get; set; }
        public string can_get_free_license { get; set; }
        public bool is_free_license { get; set; }
        public int price_in_cents_with_discount { get; set; }
    }

    public class PackageGroup
    {
        public string name { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string selection_text { get; set; }
        public string save_text { get; set; }
        public int display_type { get; set; }
        public string is_recurring_subscription { get; set; }
        public List<Sub> subs { get; set; }
    }

    public class Platforms
    {
        public bool windows { get; set; }
        public bool mac { get; set; }
        public bool linux { get; set; }
    }

    public class Metacritic
    {
        public int score { get; set; }
        public string url { get; set; }
    }

    public class Category
    {
        public int id { get; set; }
        public string description { get; set; }
    }

    public class Genre
    {
        public string id { get; set; }
        public string description { get; set; }
    }

    public class Screenshot
    {
        public int id { get; set; }
        public string path_thumbnail { get; set; }
        public string path_full { get; set; }
    }

    public class Webm
    {
        public string _480 { get; set; }
        public string max { get; set; }
    }

    public class Mp4
    {
        public string _480 { get; set; }
        public string max { get; set; }
    }

    public class Movy
    {
        public int id { get; set; }
        public string name { get; set; }
        public string thumbnail { get; set; }
        public Webm webm { get; set; }
        public Mp4 mp4 { get; set; }
        public bool highlight { get; set; }
    }

    public class Recommendations
    {
        public int total { get; set; }
    }

    public class Highlighted
    {
        public string name { get; set; }
        public string path { get; set; }
    }

    public class Achievements
    {
        public int total { get; set; }
        public List<Highlighted> highlighted { get; set; }
    }

    public class ReleaseDate
    {
        public bool coming_soon { get; set; }
        public string date { get; set; }
    }

    public class SupportInfo
    {
        public string url { get; set; }
        public string email { get; set; }
    }

    public class ContentDescriptors
    {
        public List<object> ids { get; set; }
        public object notes { get; set; }
    }

    public class Data
    {
        public string type { get; set; }
        public string name { get; set; }
        public int steam_appid { get; set; }
        public int required_age { get; set; }
        public bool is_free { get; set; }
        public List<int> dlc { get; set; }
        public string detailed_description { get; set; }
        public string about_the_game { get; set; }
        public string short_description { get; set; }
        public string supported_languages { get; set; }
        public string header_image { get; set; }
        public string website { get; set; }
        public PcRequirements pc_requirements { get; set; }
        //public List<object> mac_requirements { get; set; }
        //public List<object> linux_requirements { get; set; }
        public string legal_notice { get; set; }
        public List<string> developers { get; set; }
        public List<string> publishers { get; set; }
        public List<Demo> demos { get; set; }
        public PriceOverview price_overview { get; set; }
        public List<int> packages { get; set; }
        public List<PackageGroup> package_groups { get; set; }
        public Platforms platforms { get; set; }
        public Metacritic metacritic { get; set; }
        public List<Category> categories { get; set; }
        public List<Genre> genres { get; set; }
        public List<Screenshot> screenshots { get; set; }
        public List<Movy> movies { get; set; }
        public Recommendations recommendations { get; set; }
        public Achievements achievements { get; set; }
        public ReleaseDate release_date { get; set; }
        public SupportInfo support_info { get; set; }
        public string background { get; set; }
        public ContentDescriptors content_descriptors { get; set; }
    }

    public class _378860
    {
        public bool success { get; set; }
        public Data data { get; set; }
    }

    public class Root
    {
        public _378860 _378860 { get; set; }
    }
}
