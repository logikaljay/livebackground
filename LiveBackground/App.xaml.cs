namespace LiveBackground
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Media.Imaging;
    using LiveBackground.Infrastructure;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static List<DisplayItem> items = new List<DisplayItem>();
        
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            if (e.Args.Length > 0)
            {
                Load();
                Wallpaper.Set(new Uri(System.IO.Path.Combine(System.IO.Path.GetTempPath(), "wallpaper.bmp"), UriKind.Absolute), Wallpaper.Style.Stretched, items);

                Environment.Exit(0);
            }
        }

        public static string Load()
        {
            Store store = new Store(@"C:\temp\livebackground\", typeof(List<DisplayItem>));
            items = store.Get(typeof(List<DisplayItem>)) as List<DisplayItem>;

            Uri uri = new Uri(@"C:\temp\livebackground\background.jpg", UriKind.Absolute);
            System.IO.Stream s = new System.Net.WebClient().OpenRead(uri.ToString());

            System.Drawing.Image img = System.Drawing.Image.FromStream(s);
            string tempPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "wallpaper.bmp");
            Graphics g = Graphics.FromImage(img);

            float paddingTop = 10;
            float left = 170;
            float top = 70;
            foreach (var item in items)
            {
                var height = item.Height;
                RectangleF rectf = new RectangleF(left, top, 1024, height);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                g.DrawString(item.ToString(), new Font("Tahoma", 10), System.Drawing.Brushes.White, rectf);
                top = top + 30 + paddingTop + height;
            }

            g.Flush();

            img.Save(tempPath, System.Drawing.Imaging.ImageFormat.Bmp);

            return tempPath;
        }
    }
}
