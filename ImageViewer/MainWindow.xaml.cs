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

namespace ImageViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();  
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            var imgeUri = "http://i.imgur.com/NFCxi.jpg";
           // var imgeUri = "http://www.sergojan.com/photos/jenn.jpg";
            var targetFileName = "testimg.jpg";

            NextButton.IsEnabled = false;

            // WORKS PERFECTLY, IS SYNCHRONOUS
            using (WebClient webClient = new WebClient())
            {
                webClient.Proxy = null;  //avoids dynamic proxy discovery delay
                webClient.CachePolicy = new RequestCachePolicy(RequestCacheLevel.Default);
                try
                {
                    byte[] imageBytes = null;

                    imageBytes = webClient.DownloadData(imgeUri);

                    if (imageBytes == null)
                    {
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
                catch (WebException ex)
                {
                    //do something to report the exception
                }
            }



            /*
            // THIS WORKS PERFECTLY BUT IS ASYNC
            var image = new BitmapImage();

            image.BeginInit();
            image.DownloadCompleted += (a, b) =>
            {
                ImageBox.Source = (BitmapImage)a; 
                NextButton.IsEnabled = true;
            };
            image.UriSource = new Uri(imgeUri);
            image.EndInit();*/

        }
    }
}
