using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace CloudsdaleWin7.Controls
{
    public class Hyperlink : Span
    {
        private InlineUIContainer marker;
        private static DateTime lastTriggered = DateTime.Now;
        private static Browser _browser = new Browser();

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(Hyperlink),
                                        new PropertyMetadata(default(string), TextChanged));

        private static void TextChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            var hyperlink = (Hyperlink)dependencyObject;
            hyperlink.Inlines.Clear();

            var link = new Hyperlink
            {
                Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x63, 0xA0, 0xD0)),
                Text = args.NewValue.ToString(),
                FontSize = hyperlink.FontSize
            };
            
            //hyperlink.marker = new InlineUIContainer { Child = link };
            hyperlink.Inlines.Add(hyperlink.marker);
        }

        public async void TriggerLink(bool directOpen)
        {
            if (lastTriggered > DateTime.Now.AddMilliseconds(-500))
            {
                return;
            }
            lastTriggered = DateTime.Now;

            Uri uri;
            if (Uri.IsWellFormedUriString(Target, UriKind.Absolute))
            {
                uri = new Uri(Target);
            }
            else if (Uri.IsWellFormedUriString("http://" + Target, UriKind.Absolute))
            {
                uri = new Uri("http://" + Target);
            }
            else
            {
                return;
            }

            if (directOpen)
            {
                _browser.NavigateTo(uri.AbsolutePath);
            }

            // ReSharper restore ObjectCreationAsStatement
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TargetProperty =
            DependencyProperty.Register("Target", typeof(string), typeof(Hyperlink),
                                        new PropertyMetadata(default(string), TextChanged));

        public string Target
        {
            get { return (string)GetValue(TargetProperty); }
            set { SetValue(TargetProperty, value); }
        }
    }
}
