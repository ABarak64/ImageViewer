using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageViewer.ImageProviders
{
    /// <summary>
    /// An interface that identifies objects capable of providing image urls.
    /// </summary>
    interface IImageUrlProvider
    {
        Uri GetRandomImageUrl();
    }
}
