using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.IO;
using HtmlAgilityPack;

namespace ImageProviders
{
    /// <summary>
    /// A Pinterest Board model that scrapes the Pinterest page.
    /// </summary>
    public class PinterestBoard : IImageUrlProvider 
    {
        private readonly HtmlDocument _pinterestPage = new HtmlDocument();

        /// <summary>
        /// Constructing the object with a Uri downloads the html content of that page in a blocking manner. 
        /// </summary>
        /// <param name="pinterestBoardUrl">The Url of the Pinterest Board to scrape.</param>
        public PinterestBoard(Uri pinterestBoardUrl)
        {
            // Download the page.
            byte[] pinterestHtml;
            using (var wc = new WebClient())
            {
                pinterestHtml = wc.DownloadData(pinterestBoardUrl);
            }
            var html = Encoding.UTF8.GetString(pinterestHtml);

            this._pinterestPage.LoadHtml(html);
        }

        /// <summary>
        /// Construct a PinterestBoard with the html already handy as a string.
        /// </summary>
        /// <param name="pinterestBoardHtml"></param>
        public PinterestBoard(string pinterestBoardHtml)
        {
            this._pinterestPage.LoadHtml(pinterestBoardHtml);
        }

        /// <summary>
        /// Gets a random image url from the Pinterest board.
        /// </summary>
        /// <returns>A Uri containing a link to an image.</returns>
        public Uri GetRandomImageUrl()
        {
            var imgUrls = this.PinImageUrls.Where(url => url.AbsoluteUri.EndsWith(".jpg"));
            if (imgUrls.Any())
            {
                return imgUrls.ElementAt(new Random().Next(0, imgUrls.Count()));
            }
            else
            {
                throw new ApplicationException("No image urls found on Pinterest Board.");
            }
        }

        /// <summary>
        /// Returns the Urls of all the images that are pinned to this Pinterest page.
        /// </summary>
        public IEnumerable<Uri> PinImageUrls
        {
            get
            {
                 return this._pinterestPage.DocumentNode.SelectNodes("//img[@class='PinImageImg']")
                    .Select(n => new Uri(n.GetAttributeValue("src", null).Replace("_b.", "_f.")));  // replace this to link to the actual image and not the thumbnail.
            }
        }
    }
}
