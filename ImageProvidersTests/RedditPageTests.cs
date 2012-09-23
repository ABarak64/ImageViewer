using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.IO;
using System.Reflection;
using ImageProviders;
using ImageProvidersTests.Properties;

namespace ImageProvidersTests
{
    /// <summary>
    /// A class to test all relevant methods in the RedditPage class.
    /// </summary>
    [TestFixture]
    class RedditPageTests
    {
        /// <summary>
        /// Confirm that we are correctly finding the post urls from a reddit page Json object.
        /// </summary>
        [Test]
        public void PostUrlsTest()
        {
            List<Uri> expected = new List<Uri>
            {
                new Uri("http://i.imgur.com/gzM4d.jpg"),
                new Uri("http://i.imgur.com/RJPpg.jpg"),
                new Uri("http://i.imgur.com/lGlYd.jpg"),
                new Uri("http://imgur.com/eLsOC"),
                new Uri("http://i.imgur.com/ij8aD.jpg?1"),
                new Uri("http://i.imgur.com/chpyk.jpg"),
                new Uri("http://imgur.com/a34aG"),
                new Uri("http://i.imgur.com/bAHVs.jpg"),
                new Uri("http://imgur.com/t1gcK"),
                new Uri("http://imgur.com/ahOMq"),
                new Uri("http://imgur.com/zUtRh"),
                new Uri("http://i.imgur.com/EVsxY.jpg"),
                new Uri("http://i.imgur.com/fDD72.jpg"),
                new Uri("http://i.imgur.com/CTPoN.jpg"),
                new Uri("http://i.imgur.com/CcRhZ.jpg"),
                new Uri("http://i.imgur.com/exLxs.jpg"),
                new Uri("http://www.imgur.com/Uhwxp.jpeg"),
                new Uri("http://imgur.com/R5uRE"),
                new Uri("http://i.imgur.com/Xo9Nv.jpg"),
                new Uri("http://i.imgur.com/BrOi4.jpg"),
                new Uri("http://www.flickr.com/photos/t_funk/7303497392/"),
                new Uri("http://imgur.com/6f1Hs"),
                new Uri("http://imgur.com/nNSpv"),
                new Uri("http://i.imgur.com/sn4DM.jpg"),
                new Uri("http://i.imgur.com/ckfR4.jpg")
            };

            string sampleRedditPageJson = Resources.redditsamplepage;

            var page = new RedditPage(sampleRedditPageJson);
            var actual = page.PostUrls;

            var differenceQuery = expected.Except(actual);
            Assert.AreEqual(differenceQuery.Count(), 0);
        }
    }
}
