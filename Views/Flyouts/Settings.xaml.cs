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
        private readonly Session _current = App.Connection.SessionController.CurrentSession;
        private readonly Regex _nameRegex = new Regex(@"\b\s[a-z]\b", RegexOptions.IgnoreCase);
        private readonly Regex _usernameRegex = new Regex(@"\b[a-z0-9_]\b", RegexOptions.IgnoreCase);

        public Settings()
        {
            InitializeComponent();
            NameBlock.Text = _current.Name;
            UsernameBlock.Text = _current.Username;
            CheckChanges();
            SkypeBlock.Text = _current.SkypeName;
            AvatarImage.Source = new BitmapImage(_current.Avatar.Normal);
            Status.SelectedItem = _current.Status;
        }
        private void CheckChanges()
        {
            if (_current.CanChangeName()) return;
            UsernameBlock.IsReadOnly = true;
        }

        private void ChangeName(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            if (!_nameRegex.IsMatch(NameBlock.Text))
            {
                NameBlock.Text = _current.Name;
                return;
            }
           App.Connection.SessionController.PostData("name", NameBlock.Text);
        }
    }
}
