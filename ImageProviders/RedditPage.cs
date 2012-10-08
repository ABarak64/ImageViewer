using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.IO;
using System.Reflection;
using System.Net;
using System.Net.Cache;

namespace ImageProviders
{
    /// <summary>
    /// A Reddit page model that consumes the Reddit page Json API.
    /// </summary>
    public class RedditPage : IImageUrlProvider
    {
        private readonly dynamic _redditPage;

        /// <summary>
        /// Constructing the object with a Uri downloads the Json content of that page in a blocking manner.
        /// </summary>
        /// <param name="redditPageUrl">The url to a page of a Reddit board.</param>
        public RedditPage(Uri redditPageUrl)
        {
            // If the passed url doesn't have .json appended, then append it so we can grab the Json for the page.
            if (!redditPageUrl.AbsoluteUri.EndsWith(".json"))
            {
                redditPageUrl = new Uri(redditPageUrl.AbsoluteUri + ".json");
            }
            // Download the page Json.
            byte[] redditJson;
            using (var wc = new WebClient())
            {
                redditJson = wc.DownloadData(redditPageUrl);
            }
            var jss = new JavaScriptSerializer();
            _redditPage = jss.Deserialize<Dictionary<string, dynamic>>(Encoding.UTF8.GetString(redditJson));
        }

        /// <summary>
        /// Construcing a Reddit page when the Json string is already available.
        /// </summary>
        /// <param name="redditPageJson"></param>
        public RedditPage(string redditPageJson)
        {
            var jss = new JavaScriptSerializer();
            _redditPage = jss.Deserialize<Dictionary<string, dynamic>>(redditPageJson);
        }

        /// <summary>
        /// Gets a random image url from the Reddit page.
        /// </summary>
        /// <returns>A Uri containing a link to an image.</returns>
        public Uri GetRandomImageUrl()
        {
            var posturls = this.PostUrls;
            var imgUrls = PostUrls.Where(url => url.AbsoluteUri.EndsWith(".jpg"));
            if (imgUrls.Any())
            {
                return imgUrls.ElementAt(new Random().Next(0, imgUrls.Count()));
            }
            else
            {
                throw new ApplicationException("No image urls found on Reddit page.");
            }
        }

        /// <summary>
        /// Returns the urls of all the posts on this Reddit page.
        /// </summary>
        public IEnumerable<Uri> PostUrls
        {
            get
            {
                IEnumerable posts = _redditPage["data"]["children"]; 
                return posts.Cast<dynamic>().Select(post => new Uri(post["data"]["url"]));
            }
        }
    }
}
