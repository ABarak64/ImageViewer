using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Net;
using System.Net.Cache;
using ImageProviders;

namespace ImageViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IImageUrlProvider _imageLinkProvider;

        public MainWindow()
        {
            InitializeComponent();
            _imageLinkProvider = new RedditPage(new Uri("http://www.reddit.com/r/aww"));
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {

            NextButton.IsEnabled = false;

            /* 
             // Async, stops working after a while.
            var image = new BitmapImage();
            image.BeginInit();
            image.DownloadCompleted += (a, b) =>
            {
                ImageBox.Source = (BitmapImage)a;
                NextButton.IsEnabled = true;
            };
            image.UriSource = this._imageLinkProvider.GetRandomImageUrl();
            image.EndInit();*/


            // Stuff to play with later.

            //  var filePathSections = Assembly.GetEntryAssembly().Location.Split('\\');
            // var fileName = String.Join("\\", filePathSections.Take(filePathSections.Count() - 1)) + "\\" + "redditsamplepageNew.txt";
            //  var sampleRedditPageJson = Encoding.UTF8.GetString(File.ReadAllBytes(fileName));

            //  var postUrls = GetPostUrlsFromRedditPage(sampleRedditPageJson);


            //  Synchronously download the image from the given url.

            byte[] rawImage;
            using (var wc = new WebClient())
            {
                rawImage = wc.DownloadData(this._imageLinkProvider.GetRandomImageUrl());
            }
            MemoryStream imageStream = new MemoryStream(rawImage);
            BitmapImage image = new BitmapImage();

            image.BeginInit();
            image.StreamSource = imageStream;
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.EndInit();

            image.Freeze();
            imageStream.Close();

            ImageBox.Source = image;
            NextButton.IsEnabled = true;
        }

        /*
         
         scrape a web page and download all its images with async/await .net 4.5:

private async void button1_Click(object sender, EventArgs e)
{
    await GetPage("http://www.macrumors.com");
}

public async Task GetPage(string pageUri)
{
    byte[] result;
    using (var wc = new WebClient())
    {
        result = await wc.DownloadDataTaskAsync(pageUri);
    }
    var html = Encoding.UTF8.GetString(result);

    var doc = new HtmlAgilityPack.HtmlDocument();
    doc.LoadHtml(html);

    var images = doc.DocumentNode.Descendants("img").Select(e =>
e.GetAttributeValue("src", null))
    .Where(s => !string.IsNullOrWhiteSpace(s) && (s.EndsWith("gif") ||
        s.EndsWith("png") || s.EndsWith("jpg")))
    .Distinct();
    await DownloadImages(images, pageUri);

}

public async Task DownloadImages(IEnumerable<string> images, string pageUri)
{
    foreach (var image in images)
    {
        string urlToGrab;
        if (!image.StartsWith("http"))
        {
            urlToGrab = pageUri + image;
        }
        else
        {
            urlToGrab = image;
        }

        var dotIndex = image.LastIndexOf('.') + 1;
        var extension = image.Substring(dotIndex, image.Length - dotIndex);

        using (var wc = new WebClient())
        {
            await wc.DownloadFileTaskAsync(urlToGrab,
string.Format(@"C:\temp\{0}.{1}", Guid.NewGuid(), extension));

        }
    }
} 
         
         * */
    }
}
