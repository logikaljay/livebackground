namespace LiveBackground
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// The type of the display item
    /// </summary>
    public enum DisplayItemType : int
    {
        /// <summary>
        /// The text
        /// </summary>
        Text = 1,

        /// <summary>
        /// The web request
        /// </summary>
        WebRequest = 2,

        /// <summary>
        /// The disk
        /// </summary>
        Disk = 3,

        /// <summary>
        /// The cpu
        /// </summary>
        CPU = 4,

        /// <summary>
        /// The memory
        /// </summary>
        Memory = 5
    }
}
