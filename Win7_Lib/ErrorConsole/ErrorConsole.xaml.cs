using System.Windows;

namespace Cloudsdale_Win7.Win7_Lib.ErrorConsole
{
    /// <summary>
    /// Interaction logic for Console.xaml
    /// </summary>
    public partial class ErrorConsole : Window
    {
        public static ErrorConsole Instance;
        public ErrorConsole()
        {
            InitializeComponent();
            Instance = this;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(ConsoleText.Text);
        }
    }
}
