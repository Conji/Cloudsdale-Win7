using System;
using System.IO;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
    public partial class OwnedCloud
    {

        private Cloud Cloud { get; set; }

        public OwnedCloud(Cloud cloud)
        {
            InitializeComponent();
            Cloud = cloud;
            Shortlink.Text = "/clouds/" + cloud.ShortName;
            CloudAvatar.Source = new BitmapImage(cloud.Avatar.Normal);
            Name.Text = cloud.Name;
            if (cloud.Description != null) DescriptionBox.Text = cloud.Description.Replace("\n", "\r\n");
            if (cloud.Rules != null) RulesBox.Text = cloud.Rules.Replace("\n", "\r\n");
        }

        private void ButtonClick1(object sender, RoutedEventArgs e)
        {
            Main.Instance.HideFlyoutMenu();
        }

        private async void ButtonClick2(object sender, RoutedEventArgs e)
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

            await Cloud.UploadAvatar(new FileStream(dialog.FileName, FileMode.Open), mimeType);
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

        private async void ChangeName(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            await Cloud.UpdateProperty("name", ((TextBox) sender).Text.Trim(), true);
            Main.Instance.Clouds.SelectedItem = Cloud;
        }

        private async void ChangeDescription(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            await Cloud.UpdateProperty("description", ((TextBox) sender).Text.EscapeMessage().Trim(), true);

        }

        private async void ChangeRules(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftShift) && e.Key == Key.Enter)
            {
                ((TextBox) sender).Text += "\r\n";
                ((TextBox) sender).Select(((TextBox)sender).Text.Length, 0);
                return;
            }
            if (e.Key != Key.Enter) return;
            await Cloud.UpdateProperty("rules", ((TextBox)sender).Text.EscapeMessage().Trim(), true);
        }

        private void Subscribe(object sender, RoutedEventArgs e)
        {
            Cloud.IsSubscribed = true;
        }

        private void Unsubscribe(object sender, RoutedEventArgs e)
        {
            Cloud.IsSubscribed = false;
        }
    }
}
