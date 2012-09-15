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
using ImageViewer.ImageProviders;

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
            using (WebClient webClient = new WebClient())
            {
                webClient.Proxy = null; 
                webClient.CachePolicy = new RequestCachePolicy(RequestCacheLevel.Default);
                byte[] imageBytes = null;

                imageBytes = webClient.DownloadData(this._imageLinkProvider.GetRandomImageUrl());

                if (imageBytes == null)
                {
                    NextButton.IsEnabled = true;
                    return;
                }
                MemoryStream imageStream = new MemoryStream(imageBytes);
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
        }
    }
}
