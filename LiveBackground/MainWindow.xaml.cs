namespace LiveBackground
{
    using LiveBackground.Infrastructure;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Management;
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
    using System.Xml.Serialization;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// The items
        /// </summary>
        List<DisplayItem> items = new List<DisplayItem>();

        string tempPath = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Click event of the Button control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Load_Click(object sender, RoutedEventArgs e)
        {
            tempPath = App.Load();

            BitmapImage preview = new BitmapImage();
            preview.BeginInit();
            preview.UriSource = new Uri(System.IO.Path.Combine(System.IO.Path.GetTempPath(), "wallpaper.bmp"), UriKind.Absolute);
            preview.EndInit();
            Preview.Stretch = Stretch.Uniform;
            Preview.Source = preview;
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            Wallpaper.Set(new Uri(System.IO.Path.Combine(System.IO.Path.GetTempPath(), "wallpaper.bmp"), UriKind.Absolute), Wallpaper.Style.Stretched, items);
        }
    }
}
