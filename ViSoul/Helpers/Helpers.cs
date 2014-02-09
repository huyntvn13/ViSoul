using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Text;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web.Configuration;
using System.IO;
using System.Web.Hosting;
using ImageResizer.Util;
using ImageResizer;
using HtmlAgilityPack;
using UmbracoBP.Models;
using umbraco;
using Umbraco.Web.Mvc;
using umbraco.BusinessLogic;
using umbraco.cms.businesslogic.member;
using System.Web.Script.Serialization;


namespace UmbracoBP
{
    public class Helpers
    {
        public static string dettolGmail_mail = "od.rec0179@gmail.com";
        public static string dettolGmail_pass = "odPassWord!";

        public static List<ImageInfo> getImagesOfItem(umbraco.NodeFactory.Node node) {
            List<ImageInfo> images = new List<ImageInfo>();

            string temp = Helpers.getCroppedImageUrl(node.GetProperty<string>("picture01Cropped"), null);
            if (temp != "") {
                images.Add(new ImageInfo { desc = "", img = temp});
                temp = "";
            }

            temp = Helpers.getCroppedImageUrl(node.GetProperty<string>("picture02Cropped"), null);
            if (temp != "")
            {
                images.Add(new ImageInfo { desc = "", img = temp });
                temp = "";
            }

            temp = Helpers.getCroppedImageUrl(node.GetProperty<string>("picture03Cropped"), null);
            if (temp != "")
            {
                images.Add(new ImageInfo { desc = "", img = temp });
                temp = "";
            }

            temp = Helpers.getCroppedImageUrl(node.GetProperty<string>("picture04Cropped"), null);
            if (temp != "")
            {
                images.Add(new ImageInfo { desc = "", img = temp });
                temp = "";
            }

            temp = Helpers.getCroppedImageUrl(node.GetProperty<string>("picture05Cropped"), null);
            if (temp != "")
            {
                images.Add(new ImageInfo { desc = "", img = temp });
                temp = "";
            }

            temp = Helpers.getCroppedImageUrl(node.GetProperty<string>("picture06Cropped"), null);
            if (temp != "")
            {
                images.Add(new ImageInfo { desc = "", img = temp });
                temp = "";
            }

            temp = Helpers.getCroppedImageUrl(node.GetProperty<string>("picture07Cropped"), null);
            if (temp != "")
            {
                images.Add(new ImageInfo { desc = "", img = temp });
                temp = "";
            }

            return images;
        }

        public static string getCroppedImageUrl(string inStr, string cropName) {
            if(String.IsNullOrWhiteSpace(inStr))
                return "";

            HtmlDocument html = new HtmlDocument();
            html.LoadHtml(inStr);

            HtmlNodeCollection crops = html.DocumentNode.SelectNodes ("//crops/crop");
            if (crops.Count == 0) return "";

            if (String.IsNullOrEmpty(cropName))
            {
                // not specify a cropName >>> return the first url found
                return crops[0].GetAttributeValue("url", "");
            }
            else {
                HtmlNode matched = html.DocumentNode.SelectSingleNode("//crops/crop[@name='" + cropName + "']");
                if (matched != null) {
                    return matched.GetAttributeValue("url", "");
                }
                return "";
            } 
        }

        public static HttpCookie newAuthTypeCookie(bool usingSocial, int numDays, int numMins)
        {
            string authMethod = usingSocial ? "social" : "normal";
            var userCookie = new HttpCookie("auth_method", authMethod);
            userCookie.Expires.AddDays(numDays);
            userCookie.Expires.AddMinutes(numMins);
            return userCookie;
        }

        public static string getUniqueID()
        {
            string timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmssffff");
            string guidFragment = Guid.NewGuid().ToString().Substring(0, 4);
            return timestamp + guidFragment;
        }

        public static string getBaseURL()
        {
            string baseURL = getCurrentPageFullUri();
            string abs = getSiteAbsolutePath();
            if (abs == "/")
            {

            }
            else
            {
                //baseURL = baseURL.Replace(abs, "/");
                baseURL = Regex.Replace(baseURL, abs + ".*", "/");
            }

            return baseURL;
        }

        public static string getCurrentPageFullUri()
        {
            return HttpContext.Current.Request.Url.AbsoluteUri;
        }

        public static string getSiteAbsolutePath()
        {
            return HttpContext.Current.Request.Url.AbsolutePath;
        }

        public static string getSiteImage()
        {
            //string siteImage = shortURL + WebConfigurationManager.AppSettings["siteImage"];
            string siteImage = "http://dettol.com.au/img/logo.png";
            return siteImage;
        }

        public static string getSharingSiteHref_Facebook()
        {
            string fbAppId = WebConfigurationManager.AppSettings["fbAppId"];
            string siteName = WebConfigurationManager.AppSettings["siteName"];
            string siteDesc = WebConfigurationManager.AppSettings["siteDesc"];
            string shortURL = Helpers.getBaseURL();
            string siteImage = getSiteImage();

            //string hrefStr = "https://www.facebook.com/dialog/feed?app_id=";
            //string otherParts = fbAppId + "&link=" + shortURL + "&picture=" + siteImage + "&name=" + siteName + "&caption=" + siteName + "&description=" + siteDesc + "&message=" + siteName + "&redirect_uri=https://facebook.com/";
            //string otherParts = fbAppId + "&link=" + shortURL + "&redirect_uri=https://facebook.com/";

            string hrefStr = "http://www.facebook.com/sharer.php?u=";
            string otherParts = shortURL;

            return hrefStr + otherParts;
        }

        public static string getSharingSiteHref_Twitter()
        {
            string siteName = WebConfigurationManager.AppSettings["siteName"];
            string siteDesc = WebConfigurationManager.AppSettings["siteDesc"];
            string shortURL = Helpers.getBaseURL();
            string siteImage = getSiteImage();

            //string hrefStr = "https://twitter.com/intent/tweet?original_referer=";
            //string otherParts = shortURL + "&text=" + siteDesc + " " + shortURL + "&tw_p=tweetbutton&url=" + shortURL;

            string hrefStr = "http://twitter.com/share?text=";
            //string otherParts = siteName + " - " + siteDesc + "&url=" + shortURL + "&hashtags=dettol,missionforhealth";
            //string otherParts = "Dettol are celebrating those little acts of care that help your family stay healthy and happy. Go to http://goo.gl/Yo4ABr";
            string otherParts = "Dettol are celebrating those little acts of care that help your family stay healthy and happy. Go to ";

            return hrefStr + otherParts;
        }

        public static string getSharingSiteHref_GooglePlus()
        {
            string shortURL = Helpers.getBaseURL();

            string hrefStr = "https://plus.google.com/share?url=" + shortURL;
            return hrefStr;
        }

        public static void sendSMTPEmailFromGmail(string toEmail, string subject, string mailContent)
        {
            SmtpClient ss = new SmtpClient("smtp.gmail.com", 587);
            ss.EnableSsl = true;
            ss.Timeout = 10000;
            ss.DeliveryMethod = SmtpDeliveryMethod.Network;
            ss.UseDefaultCredentials = false;
            ss.Credentials = new NetworkCredential(dettolGmail_mail, dettolGmail_pass);

            MailMessage mm = new MailMessage(dettolGmail_mail, toEmail, subject, mailContent);
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            ss.Send(mm);
        }

        public static string gravatarURL(string emailAddress)
        {
            //Get email to lower
            var emailToHash = emailAddress.ToLower();

            // Create a new instance of the MD5CryptoServiceProvider object.  
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.  
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(emailToHash));

            // Create a new Stringbuilder to collect the bytes  
            // and create a string.  
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data  
            // and format each one as a hexadecimal string.  
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            var hashedEmail = sBuilder.ToString();  // Return the hexadecimal string.

            //Return the gravatar URL
            return "http://www.gravatar.com/avatar/" + hashedEmail;
        }

        public static string GetYoutubeEmbedLink(string link)
        {
            string result = "";
            string id = string.Empty;

            var regex = new Regex(@"youtu(?:\.be|be\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)");
            Match youtubeMatch = regex.Match(link);
            if (youtubeMatch.Success)
            {
                id = youtubeMatch.Groups[1].Value;
            }
            if (!String.IsNullOrEmpty(id))
            {
                result = "http://www.youtube.com/embed/" + id;
            }
            return result;
        }

        public static string Truncate(string str, int maxLength, string suffix)
        {
            var words = str.Split(' ');
            if (words.Length > maxLength)
            {
                maxLength = Math.Min(maxLength, words.Length);
                var sb = new StringBuilder();
                for (int i = 0; i < maxLength; i++)
                {
                    sb.Append(words[i]);
                    sb.Append(" ");
                }
                str = sb.ToString().TrimEnd(' ') + suffix;
            }
            return str.Trim();
        }

        public static string RenameImage(string image, string content)
        {
            string oldName = Path.GetFileName(image);
            var serverOldName = HostingEnvironment.MapPath(Constants.Path.Image + oldName);
            string extension = ImageResizer.Util.PathUtils.GetFullExtension(image).ToLower();
            string newName = CreateFileName(content, extension);
            var serverNewName = HostingEnvironment.MapPath(Constants.Path.Image + newName);
            try
            {
                string basePath = ImageResizer.Util.PathUtils.RemoveExtension(serverNewName);
                ImageBuilder.Current.Build(new ImageJob(serverOldName, basePath + Constants.ImageResizeSize.Name.thumbnails_700, Constants.ImageResizeSize.thumbnails_700, false, true));
                ImageBuilder.Current.Build(new ImageJob(serverOldName, basePath + Constants.ImageResizeSize.Name.thumbnails_200, Constants.ImageResizeSize.thumbnails_200, false, true));
                //File.Delete(serverOldName);
                File.Move(serverOldName, serverNewName);
            }
            catch (Exception e)
            {
                return "";
            }
            return Constants.Path.Image + newName;
        }

        public static bool GenerateAvatarThumb(string image)
        {
            string name = Path.GetFileName(image);
            var serverName = HostingEnvironment.MapPath(Constants.Path.Image + name);
            string extension = ImageResizer.Util.PathUtils.GetFullExtension(image).ToLower();
            try
            {
                string basePath = ImageResizer.Util.PathUtils.RemoveExtension(serverName);
                ImageBuilder.Current.Build(new ImageJob(serverName, basePath + Constants.ImageResizeSize.Name.thumbnails_85, Constants.ImageResizeSize.thumbnails_85, false, true));
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public static string CreateFileName(string content, string extension)
        {
            content = Regex.Replace(content, @"[^a-zA-Z0-9\s-]", "");
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                content = content.Replace(c, '-');
            }
            content = Helpers.Truncate(content, 4, "");
            content = content.Replace(" ", "-");
            content = content + "-" + DateTime.UtcNow.Ticks.ToString() + extension;
            return content;
        }

        public static string getImageThumbnail(string name, string type)
        {
            if (name != null)
            {
                if (name.IndexOf("graph.facebook.com") < 0)
                {
                    return PathUtils.RemoveExtension(name) + type + PathUtils.GetExtension(name);
                }
                else
                {

                    return name.Replace("=400", "=85");
                }
            }
            else return string.Empty;
        }

        public static string EncodeTo64(string toEncode)
        {
            byte[] toEncodeAsBytes
                  = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);
            string returnValue
                  = System.Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }

        public static string DecodeFrom64(string encodedData)
        {
            byte[] encodedDataAsBytes
                = System.Convert.FromBase64String(encodedData);
            string returnValue =
               System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);
            return returnValue;
        }

        public static string GenerateOnlineLink(string FirstName, string Link, string Type)
        {
            var key = FirstName + ";#" + Link + ";#" + Type;
            var encryptedKey = EncodeTo64(key);
            var link = getBaseURL() + "Email/ViewOnline?key=" + encryptedKey;
            string result = new WebClient().DownloadString("http://tinyurl.com/api-create.php?url=" + link);
            return result;
        }
    }
}