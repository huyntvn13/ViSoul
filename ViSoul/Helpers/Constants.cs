using ImageResizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UmbracoBP
{
    public class Constants
    {
        public static class Path
        {
            public static string Image = "/media/";
            public static string ImageAbsolute = System.IO.Directory.GetCurrentDirectory() + "/media/";
        }
        public static class ImageResizeSize
        {
            public static Instructions thumbnails_700 = new Instructions { Width = 700 };
            public static Instructions thumbnails_200 = new Instructions { Width = 200 };
            public static Instructions thumbnails_85 = new Instructions { Width = 85 };

            public static class Name
            {
                public static string thumbnails_700 = "_thumb_700";
                public static string thumbnails_200 = "_thumb_200";
                public static string thumbnails_85 = "_thumb_85";
            }
        }
        public static class PostType
        {
            public static string MumOfTheMonth = "Mum Of The Month";
            public static string MumOfTheMoment = "Mum Of The Moment";
            public static string WeeklyGiveAway = "Weekly Giveaway";
            public static string EditorsPick = "Editor's Pick";
            public static string OurLittleThing = "Our Little Things";
        }
        public static class PostStatus
        {
            public static string Unapproved = "Unapproved";
            public static string Approved = "Approved";
            public static string Rejected = "Rejected";
        }
        public static class EmailType
        {
            public static string Welcome = "Welcome";
            public static string PostApproved = "PostApproved";
            public static string MumOfTheMonth = "MumOfTheMonth";
            public static string MumOfTheMoment = "MumOfTheMoment";
        }
    }
}