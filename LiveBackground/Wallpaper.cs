namespace LiveBackground
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading.Tasks;
    using System.Drawing;
    using Microsoft.Win32;

    /// <summary>
    /// Wallpaper class for switching the desktop background
    /// </summary>
    public sealed class Wallpaper
    {
        Wallpaper() { }

        const int SPI_SETDESKWALLPAPER = 20;
        const int SPIF_UPDATEINIFILE = 0x01;
        const int SPIF_SENDWININICHANGE = 0x02;

        /// <summary>
        /// Systems the parameters information.
        /// </summary>
        /// <param name="uAction">The u action.</param>
        /// <param name="uParam">The u parameter.</param>
        /// <param name="lpvParam">The LPV parameter.</param>
        /// <param name="fuWinIni">The fu win ini.</param>
        /// <returns>
        /// int of result
        /// </returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        /// <summary>
        /// Style of wallpaper
        /// </summary>
        public enum Style : int
        {
            Tiled,
            Centered,
            Stretched
        }

        /// <summary>
        /// Sets the specified URI as a desktop wallpaper.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="style">The style.</param>
        /// <param name="items">The items.</param>
        public static void Set(Uri uri, Style style, List<DisplayItem> items)
        {
            System.IO.Stream s = new System.Net.WebClient().OpenRead(uri.ToString());

            System.Drawing.Image img = System.Drawing.Image.FromStream(s);
            string tempPath = Path.Combine(Path.GetTempPath(), "wallpaper.bmp");
            
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
            if (style == Style.Stretched)
            {
                key.SetValue(@"WallpaperStyle", 2.ToString());
                key.SetValue(@"TileWallpaper", 0.ToString());
            }

            if (style == Style.Centered)
            {
                key.SetValue(@"WallpaperStyle", 1.ToString());
                key.SetValue(@"TileWallpaper", 0.ToString());
            }

            if (style == Style.Tiled)
            {
                key.SetValue(@"WallpaperStyke", 1.ToString());
                key.SetValue(@"TileWallpaper", 1.ToString());
            }

            // Change the wallpaper
            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, tempPath, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
        }
    }
}