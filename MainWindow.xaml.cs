using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using CloudsdaleWin7.lib;
using CloudsdaleWin7.lib.Helpers;
using CloudsdaleWin7.lib.Models;
using CloudsdaleWin7.Controls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CloudsdaleWin7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {

        public static MainWindow Instance;

        public int MaxCharacters
        {
            get { return (int) GetValue(MaxCharactersProperty); }
            set { SetValue(MaxCharactersProperty, value); }
        }

        public MainWindow()
        {
            Instance = this;
            ClientVersion.Validate();
            InitializeComponent();
            MainFrame.Navigate(new Login());
            App.Connection.MainFrame = MainFrame;
            Testvoid();
        }

        public static DependencyProperty MaxCharactersProperty =
            DependencyProperty.Register("MaxCharacters", typeof (int), typeof (MainWindow),
                                        new FrameworkPropertyMetadata(200));

        private void SaveSettings(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Close();
        }

        private async void Testvoid()
        {

        }
    }
}