using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.BulkAccess;
using Windows.Storage.FileProperties;
using Windows.Storage.Search;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

/*
 * Reference: "Windows 8 Apps: Creating Picture Library Viewer using XAML GridView and C#"
 * http://www.devcurry.com/2013/02/windows-8-apps-creating-picture-library.html
 */

// The Namespace
namespace StoreApp
{
    // Gallery Page Class
    public sealed partial class GalleryPage : Page
    {
        // Constructor
        public GalleryPage()
        {
            this.InitializeComponent();    
        }

        // Go Back Button Click
        private void GoBack(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        // Page Loaded
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Define the query to iterate thriugh all the subfolders 
            var pictureQueryOptions = new QueryOptions();

            // Read only Picture Library folder 
            pictureQueryOptions.FolderDepth = FolderDepth.Shallow;

            // Read through all the subfolders
            // pictureQueryOptions.FolderDepth = FolderDepth.Deep;

            // Apply the query on the PicturesLibrary 
            var pictureQuery = KnownFolders.PicturesLibrary.CreateFileQueryWithOptions(pictureQueryOptions);            
            var picturesInformation = new FileInformationFactory(pictureQuery, ThumbnailMode.PicturesView);
            picturesSource.Source = picturesInformation.GetVirtualizedFilesVector();
        }
    }

    // The converter class used to display images
    public class ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            if (value != null)
            {
                var img = (IRandomAccessStream)value;
                var picture = new BitmapImage();
                picture.SetSource(img);
                return picture;
            }
            return DependencyProperty.UnsetValue;
        }
        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }
    }
}
