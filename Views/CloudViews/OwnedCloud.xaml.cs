using System;
using System.IO;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using CloudsdaleWin7.lib;
using CloudsdaleWin7.lib.Helpers;
using CloudsdaleWin7.lib.Models;
using WinForms = System.Windows.Forms;

namespace CloudsdaleWin7.Views.CloudViews
{
    /// <summary>
    /// Interaction logic for OwnedCloud.xaml
    /// </summary>
    public partial class OwnedCloud : Page
    {

        private Cloud Cloud { get; set; }

        public OwnedCloud(Cloud cloud)
        {
            InitializeComponent();
            Cloud = cloud;
            Shortlink.Text = "/clouds/" + cloud.ShortName;
            Shortlink.IsReadOnly = cloud.ShortName != cloud.Id;
            CloudAvatar.Source = new BitmapImage(cloud.Avatar.Normal);
            Name.Text = cloud.Name;
            if (cloud.Description != null) DescriptionBox.Text = cloud.Description.Replace("\n", Environment.NewLine);
            if (cloud.Rules != null) RulesBox.Text = cloud.Rules.Replace("\n", Environment.NewLine);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Main.Instance.HideFlyoutMenu();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var dialog = new WinForms.OpenFileDialog
                             {
                                 InitialDirectory = Environment.SpecialFolder.MyPictures.ToString(),
                                 Title = "Upload a new cloud avatar...",
                                 Filter = "Image files |*.png; *.jpg; *.bmp"
                             };
            dialog.ShowDialog();

            if (dialog.SafeFileName == null) return;
            if (String.IsNullOrEmpty(dialog.FileName)) return;

            var mimeType = "image/";
            if (dialog.SafeFileName.EndsWith(".png")) mimeType += "png";
            if (dialog.SafeFileName.EndsWith(".jpg")) mimeType += "jpg";
            if (dialog.SafeFileName.EndsWith(".bmp")) mimeType += "bmp";

            Cloud.UploadAvatar(new FileStream(dialog.FileName, FileMode.Open), mimeType);
        }

        private async void AttemptDelete(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete your cloud? This action can't be undone.", "Confirm", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                return;
            var client = new HttpClient().AcceptsJson();
            client.DefaultRequestHeaders.Add("X-Auth-Token", App.Connection.SessionController.CurrentSession.AuthToken);
            await client.DeleteAsync(Endpoints.Cloud.Replace("[:id]", Cloud.Id));
            App.Connection.SessionController.CurrentSession.Clouds.Remove(Cloud);
            App.Connection.SessionController.RefreshClouds();
            Main.Instance.HideFlyoutMenu();
        }
    }
}
