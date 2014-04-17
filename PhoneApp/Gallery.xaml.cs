using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Data;
using Windows.Storage.Streams;
using System.Windows.Media.Imaging;
using System.IO;
using System.IO.IsolatedStorage;
using PhoneApp.Services;

namespace PhoneApp
{
    // Gallery Class
    public partial class Gallery : PhoneApplicationPage
    {
        // Gallery List
        List<GalleryImage> galleryImages = new List<GalleryImage>();

        // Constructor
        public Gallery()
        {
            InitializeComponent();
            // Load Pictures
            LoadPictures();
            ListBoxPictures.ItemsSource = galleryImages;
        }

        // Load Pictures
        void LoadPictures()
        {
            using (var userStore = IsolatedStorageFile.GetUserStoreForApplication())
            {
                var fileNames = userStore.GetFileNames("*.jpg");
                if (!fileNames.Any())
                {
                    MessageBox.Show("There are no pictures in the gallery!");
                    return;
                }
                foreach (var fileName in fileNames)
                {
                    using (var stream = userStore.OpenFile(fileName, FileMode.Open))
                    {
                        GalleryImage newGalleryImage = new GalleryImage() { ImagePath = stream.Name };
                        galleryImages.Add(newGalleryImage);
                    }
                }
            }
        }
    }
}