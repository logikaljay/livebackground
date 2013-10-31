namespace LiveBackground
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Management;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Display item class for Live Background
    /// </summary>
    public class DisplayItem
    {
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public DisplayItemType DisplayItemType { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public float Width { get; set; }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        public float Height { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public string ToString()
        {
            // action the display item and then return the result
            switch (this.DisplayItemType)
            {
                case DisplayItemType.Text:
                    return this.Text;

                case DisplayItemType.WebRequest:
                    return new WebClient().DownloadString(this.Text);

                case DisplayItemType.Disk:
                    foreach (ManagementObject volume in new ManagementObjectSearcher("Select * from Win32_Volume").Get())
                    {
                        if (volume["FreeSpace"] != null && volume["Name"].ToString().ToLower() == this.Text.ToLower())
                        {
                            ulong freeSpace = ulong.Parse(volume["FreeSpace"].ToString()) / 1024 / 1024 / 1024;
                            ulong totalSpace = ulong.Parse(volume["Capacity"].ToString()) / 1024 / 1024 / 1024;
                            return string.Format("{0} {1}/{2} GB", volume["Name"], freeSpace.ToString("#,##0"), totalSpace.ToString("#,##0"));
                        }
                    }

                    return string.Format("{0} Not found or Empty", this.Text);

                case DisplayItemType.CPU:
                    int coreCount = 0;
                    string clockSpeed = string.Empty;
                    foreach (var item in new ManagementObjectSearcher("Select * from Win32_Processor").Get())
                    {
                        coreCount += int.Parse(item["NumberOfCores"].ToString());
                        clockSpeed = float.Parse(item["MaxClockSpeed"].ToString()).ToString("#,###0");
                    }

                    return string.Format("{0} cores running at {1} Mhz", coreCount, clockSpeed);

                case DisplayItemType.Memory:
                    UInt64 capacity = 0;
                    foreach(var item in new ManagementObjectSearcher("SELECT Capacity FROM Win32_PhysicalMemory").Get())
                    {
                        capacity += UInt64.Parse(item["Capacity"].ToString()) / 1024 / 1024;
                    }

                    return string.Format("{0} MB", capacity.ToString("#,###0"));

                default:
                    return string.Empty;
            }
        }
    }
}
