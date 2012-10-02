using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProviders
{
    /// <summary>
    /// Abstract factory that produces classes that implement IImageProvider based on a string mapping.
    /// </summary>
    public class ImageProviderFactory
    {
        private static readonly List<string> _providerTypes = new List<string>()
        {
            {"Reddit"},
            {"Pinterest"}
        };

        /// <summary>
        /// Returns a list of potential providers good for databinding and later passing into the CreateProvider method.
        /// </summary>
        public IEnumerable<string> ProviderNames
        {
            get
            {
                return _providerTypes;
            }
        }

        /// <summary>
        /// Returns a class that implements IImageProvider based on the string passed, which should come from the provider types.
        /// </summary>
        /// <param name="providerName">A string that maps to an image provider. Should come from the provider types list.</param>
        /// <returns></returns>
        public IImageUrlProvider CreateProvider(string providerName)
        {
            IImageUrlProvider provider;
            switch (providerName)
            {
                case "Reddit":
                default:
                    provider = new RedditPage(new Uri("http://www.reddit.com/r/aww"));
                    break;
                case "Pinterest":
                    provider = new PinterestBoard(new Uri("http://pinterest.com/all/?category=animals"));
                    break;
            }
            return provider;
        }
    }
}
