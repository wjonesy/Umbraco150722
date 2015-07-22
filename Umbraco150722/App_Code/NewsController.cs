using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core;
using Umbraco.Web;
using Umbraco.Core.Models;
using Umbraco.Web.WebApi;
using System.ServiceModel.Syndication;
using Umbraco.Web.BaseRest;
using Umbraco150722.App_Code;

namespace SilverBackWebsite.App_Code
{
    public class NewsController : UmbracoApiController
    {
        ///umbraco/Api/news/getallnews?parent=1053
        public IEnumerable<NewsItem> GetAllNews(int parent,int page)
        {
            UmbracoHelper help = new UmbracoHelper(UmbracoContext);
            int numberInPage = 2;
            if(page == 0) { page = numberInPage; }
            int count = help.TypedContent(parent).Children().Where("Visible").Count();
            return help.TypedContent(parent).Children().Where("Visible").Skip((numberInPage - page) + numberInPage).Select(obj => new NewsItem()
            {
                Name = obj.Name,
                Id = obj.Id
                //BodyText = obj.GetPropertyValue(bodyText)
            });
        }
        ///umbraco/Api/news/getnews?id=1054
        public NewsItem GetNews(int id)
        {
            UmbracoHelper help = new UmbracoHelper(UmbracoContext);
            var content = help.TypedContent(id);
            return new NewsItem
            {
                Name = content.Name,
                Id = content.Id
            };
        }
    }
}