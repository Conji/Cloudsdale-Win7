using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using CloudsdaleWin7.lib.Models;

namespace CloudsdaleWin7.Views.Flyouts
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        private readonly Session Current = App.Connection.SessionController.CurrentSession;
        private readonly Regex UsernameRegex = new Regex(@"\b[a-z]", RegexOptions.IgnoreCase);
        private readonly Regex NameRegex = new Regex(@"\b[a-z0-9_]", RegexOptions.IgnoreCase);

        public Settings()
        {
            InitializeComponent();
            NameBlock.Text = Current.Name;
            UsernameBlock.Text = Current.Username;
            CheckChanges();
            SkypeBlock.Text = Current.SkypeName;
            AvatarImage.Source = new BitmapImage(Current.Avatar.Normal);
        }
        private void CheckChanges()
        {
            if (Current.CanChangeName()) return;
            UsernameBlock.IsReadOnly = true;
        }

        private void ChangeName(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            if (NameRegex.IsMatch(NameBlock.Text)) return;
            NameBlock.Text = Current.Name;
        }
    }
}
