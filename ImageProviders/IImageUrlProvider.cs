using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProviders
{
    /// <summary>
    /// An interface that identifies objects capable of providing image urls.
    /// </summary>
    public interface IImageUrlProvider
    {
        Uri GetRandomImageUrl();
    }
}
