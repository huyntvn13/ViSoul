using System;
using System.Linq;
using System.Collections;
using System.Text.RegularExpressions;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Umbraco.Web.Mvc;
using umbraco.BusinessLogic;
using umbraco.cms.businesslogic.member;
using System.Web.Script.Serialization;
using umbraco;
using System.IO;
using Newtonsoft.Json;
using UmbracoBP.Models;
using System.Collections.Generic;

namespace UmbracoBP.Controllers.SurfaceControllers
{
    public class ApiSurfaceController : SurfaceController
    {
        public ActionResult LoadPosts(string number, string offset, string get_action, string itemid, string cat)
        {
            if (get_action == "pexeto_get_portfolio_content")
            {
                return RedirectToAction("PostDetails", "ApiSurface", new
                {
                    itemid = itemid
                });
            }

            int outNum;
            int numberInt = int.TryParse(number, out outNum) ? outNum : 10;
            int offsetInt = int.TryParse(offset, out outNum) ? outNum : 0;
            
            //return Content("{\"items\":[{\"id\":1582,\"title\":\"Rocker\u2019s Girlfriend\",\"image\":\"http://pexetothemes.com/demos/photolux_wp/wp-content/themes/photolux/lib/utils/timthumb.php?src=http://pexetothemes.com/demos/photolux_wp/wp-content/uploads/2011/11/1322051499rockers-girlfriend.jpg&h=&w=290&zc=1&q=100&a=t\",\"cat\":\"People / Portrait\",\"link\":\"permalink\"},{\"id\":1675,\"title\":\"Girl With Sunglasses\",\"image\":\"http://pexetothemes.com/demos/photolux_wp/wp-content/themes/photolux/lib/utils/timthumb.php?src=http://pexetothemes.com/demos/photolux_wp/wp-content/uploads/2011/11/1322078133portrait-of-pretty-young-female-in-sunglasses.jpg&h=&w=290&zc=1&q=100&a=c\",\"cat\":\"Fashion / Lifestyle\",\"link\":\"permalink\"},{\"id\":1669,\"title\":\"Retro Women\",\"image\":\"http://pexetothemes.com/demos/photolux_wp/wp-content/themes/photolux/lib/utils/timthumb.php?src=http://pexetothemes.com/demos/photolux_wp/wp-content/uploads/2011/11/hat-and-dress.jpg&h=&w=290&zc=1&q=100&a=c\",\"cat\":\"Fashion / Lifestyle\",\"link\":\"permalink\"},{\"id\":1651,\"title\":\"Fashion Model Posing\",\"image\":\"http://pexetothemes.com/demos/photolux_wp/wp-content/themes/photolux/lib/utils/timthumb.php?src=http://pexetothemes.com/demos/photolux_wp/wp-content/uploads/2011/11/1322078062sexy-young-female-fashion-model-posing.jpg&h=&w=290&zc=1&q=100&a=c\",\"cat\":\"Fashion / Portrait\",\"link\":\"permalink\"},{\"id\":1631,\"title\":\"Hair Style\",\"image\":\"http://pexetothemes.com/demos/photolux_wp/wp-content/themes/photolux/lib/utils/timthumb.php?src=http://pexetothemes.com/demos/photolux_wp/wp-content/uploads/2011/11/hot-beauty.jpg&h=&w=290&zc=1&q=100&a=c\",\"cat\":\"Fashion / Portrait\",\"link\":\"permalink\"},{\"id\":1686,\"title\":\"Street Life\",\"image\":\"http://pexetothemes.com/demos/photolux_wp/wp-content/themes/photolux/lib/utils/timthumb.php?src=http://pexetothemes.com/demos/photolux_wp/wp-content/uploads/2011/11/1321987121closeup-of-a-beautiful-young-girl-looking-away.jpg&h=&w=290&zc=1&q=100&a=c\",\"cat\":\"Lifestyle / People\",\"link\":\"permalink\"},{\"id\":1662,\"title\":\"Beautiful Brunette \",\"image\":\"http://pexetothemes.com/demos/photolux_wp/wp-content/themes/photolux/lib/utils/timthumb.php?src=http://pexetothemes.com/demos/photolux_wp/wp-content/uploads/2011/11/beautiful-brunette2.jpg&h=&w=290&zc=1&q=100&a=c\",\"cat\":\"Black&amp;white / Portrait\",\"link\":\"permalink\"},{\"id\":1837,\"title\":\"Girl in Veil\",\"image\":\"http://pexetothemes.com/demos/photolux_wp/wp-content/themes/photolux/lib/utils/timthumb.php?src=http://pexetothemes.com/demos/photolux_wp/wp-content/uploads/2011/11/beautiful-blonde-in-vei2l.jpg&h=&w=290&zc=1&q=100&a=c\",\"cat\":\"Fashion / People\",\"link\":\"permalink\"},{\"id\":1819,\"title\":\"Adipiscing Elit\",\"image\":\"http://pexetothemes.com/demos/photolux_wp/wp-content/themes/photolux/lib/utils/timthumb.php?src=http://pexetothemes.com/demos/photolux_wp/wp-content/uploads/2011/11/retro-woman-(1).jpg&h=&w=290&zc=1&q=100&a=c\",\"cat\":\"Fashion / Portrait\",\"link\":\"permalink\"},{\"id\":1830,\"title\":\"Model With Sunglasses\",\"image\":\"http://pexetothemes.com/demos/photolux_wp/wp-content/themes/photolux/lib/utils/timthumb.php?src=http://pexetothemes.com/demos/photolux_wp/wp-content/uploads/2011/11/portait-of-glamorous-young-female-model2.jpg&h=&w=290&zc=1&q=100&a=c\",\"cat\":\"Fashion / Portrait\",\"link\":\"permalink\"}],\"more\":true}");
            //return Json(new { status = false, returnUrl = "", message = "Login failed." });

            // query example
            // var items = uQuery.GetNodesByType("item").Where(x => x.GetProperty<string>("status") == Constant.PostStatus.Approved);
            var items = uQuery.GetNodesByType("item");
            if (cat.ToLower() == "mostpeeked") {
                items = items.OrderByDescending(r => r.GetProperty<int>("peeked")).ThenByDescending(r => r.UpdateDate).Skip(offsetInt).Take(numberInt);
            }
            else if (cat.ToLower() == "mostpeeked")
            {
                items = items.OrderByDescending(r => r.GetProperty<int>("viewed")).ThenByDescending(r => r.UpdateDate).Skip(offsetInt).Take(numberInt);
            }else {
                // cat == null || cat == "" || cat == "viewAll"
                items = items.OrderByDescending(r => r.CreateDate).Skip(offsetInt).Take(numberInt);
            }
            
            List<ItemOverviewDocType> itemList;
            itemList = new List<ItemOverviewDocType>();
            if (items.Count() == 0)
            {
                return Json(new { 
                    items = itemList,
                    more = false
                }, JsonRequestBehavior.AllowGet);
            }

            bool hasMore = true;
            if (items.Count() < numberInt)
            {
                hasMore = false;
            }
            else {
                var moreItems = uQuery.GetNodesByType("item").OrderByDescending(r => r.UpdateDate).Skip(offsetInt + numberInt);
                if (moreItems == null)
                    hasMore = false;
                else
                    if (moreItems.Count() == 0)
                        hasMore = false;
            }

            var allFilteredItems = items.Select(obj => new ItemOverviewDocType()
            {
                id = obj.Id,
                title = obj.Name,
                cat = obj.GetProperty<string>("category"),
                created = obj.CreateDate.ToLocalTime().ToString(),
                tags = obj.GetProperty<string>("tags").Split(',').ToList(),
                lightbox = true,
                //link = "permalink",
                link = obj.GetProperty<string>("image"),
                url = obj.Url,
                image = Helpers.getCroppedImageUrl(obj.GetProperty<string>("imageCropped"), null),
                imageOrig = obj.GetProperty<string>("image")
                
                //WisdomId = obj.Id,
                //Content = obj.GetProperty<string>("content"),
                //OurLittleThing = new Member(obj.GetProperty<int>("author")).IsInGroup("Moderators"),
                //CommentCount = obj.GetChildNodesByType("comment").Where(x => x.GetProperty<bool>("active") == true).Count(),
            });

            foreach(ItemOverviewDocType filteredItem in allFilteredItems){
                itemList.Add(filteredItem);
            }

            return Json(new
            {
                items = itemList,
                more = hasMore
            }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult PostDetails(string itemid)
        {
            int outNum;
            int idInt = int.TryParse(itemid, out outNum) ? outNum : 0;

            // query example
            // var items = uQuery.GetNodesByType("item").Where(x => x.GetProperty<string>("status") == Constant.PostStatus.Approved);
            var item = uQuery.GetNode(idInt);

            if (item == null)
            {
                return Json(new { 
                
                }, JsonRequestBehavior.AllowGet);
            }

            var jsonObj = new ItemDocType{
                id = item.Id,
                title = item.Name,
                content = item.GetProperty<string>("content"),
                cat = item.GetProperty<string>("category"),
                show_content = true,
                url = item.Url,
                created = item.CreateDate.ToLocalTime().ToString(),
                tags = item.GetProperty<string>("tags").Split(',').ToList(),
                images = Helpers.getImagesOfItem(item)
            };

            // update stats
            string numPeeked = item.GetProperty<string>("peeked");
            int newPeekValue = 0;
            if (int.TryParse(numPeeked, out outNum))
            {
                newPeekValue = int.Parse(numPeeked) + 1;
                item.SetProperty("peeked", int.Parse(numPeeked) + 1);
            }
            else
            {
                item.SetProperty("peeked", 1);
            }
            item.Publish(true);

            //image = Helpers.getCroppedImageUrl(obj.GetProperty<string>("imageCropped"), null),
            //imageOrig = obj.GetProperty<string>("image")

            return Json(jsonObj, JsonRequestBehavior.AllowGet);
        }
    }
}