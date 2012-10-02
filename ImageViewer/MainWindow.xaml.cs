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
using System.Net;
using System.Net.Cache;
using ImageProviders;
using System.Reflection;
using System.IO;

namespace ImageViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IImageUrlProvider _imageLinkProvider;

        public MainWindow()
        {
            InitializeComponent();

          var providerFactory = new ImageProviderFactory();
          var defaultProvider = providerFactory.ProviderNames.First();
          ImageSourceDropDown.ItemsSource = providerFactory.ProviderNames;
          ImageSourceDropDown.SelectedValue = defaultProvider;
        }

        /// <summary>
        /// User has clicked the 'Next' button, indicating they want to see a new image. Gets the new Image
        /// Url, downloads the image and puts it on the GUI.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void NextButton_Click(object sender, RoutedEventArgs e)
        {
            NextButton.IsEnabled = false;

            //download the image from the given url.
            byte[] rawImage;
            using (var wc = new WebClient())
            {
                rawImage = await wc.DownloadDataTaskAsync(this._imageLinkProvider.GetRandomImageUrl());
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

        /// <summary>
        /// User has changed their desired image source via changing the dropdown list on the GUI. Creates a new provider
        /// based on the value of the dropdown that they selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ImageSourceDropDown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NextButton.IsEnabled = false;
            var providerName = e.AddedItems[0].ToString();
            var providerFactory = new ImageProviderFactory();
            // set the new imageprovider asynchronously since it requires downloading a page.
            await Task.Run(() => { this._imageLinkProvider = providerFactory.CreateProvider(providerName); });
            NextButton.IsEnabled = true;
        }
    }
}
