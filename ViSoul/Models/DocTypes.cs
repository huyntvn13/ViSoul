using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UmbracoBP.Models
{
    public class ItemDocType
    {
        public int id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string cat { get; set; }
        public List<ImageInfo> images { get; set; }
        public bool show_content { get; set; }

        public List<string> tags { get; set; }
        public string url { get; set; }
        public string created { get; set; }
    }

    public class ImageInfo {
        public string desc { get; set; }
        public string img { get; set; }
    }
    
    public class ItemOverviewDocType
    {
        public int id { get; set; }
        public string title { get; set; }
        public string cat { get; set; }
        public List<string> tags { get; set; }
        public string link { get; set; }
        public string url { get; set; }
        public bool lightbox { get; set; }
        public string created { get; set; }

        public string image { get; set; }
        public string imageOrig { get; set; }
    }
    
    //public class ItemDocType
    //{
    //    public int id { get; set; }
    //    public string title {get; set;}
    //    public string cat { get; set; }
    //    public List<string> tags { get; set; }
    //    public string link { get; set; }
    //    public bool lightbox { get; set; }
    //    public string created { get; set; }

    //    public string image { get; set; }
    //    public string picture01 { get; set; }
    //    public string picture02 { get; set; }
    //    public string picture03 { get; set; }
    //    public string picture04 { get; set; }
    //    public string picture05 { get; set; }
    //    public string picture06 { get; set; }
    //    public string picture07 { get; set; }

    //    public string imageOrig { get; set; }
    //    public string picture01Orig { get; set; }
    //    public string picture02Orig { get; set; }
    //    public string picture03Orig { get; set; }
    //    public string picture04Orig { get; set; }
    //    public string picture05Orig { get; set; }
    //    public string picture06Orig { get; set; }
    //    public string picture07Orig { get; set; }
    //}
}