namespace LiveBackground.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Xml.Serialization;

    /// <summary>
    /// Store any object as XML
    /// </summary>
    public class Store
    {

        /// <summary>
        /// returns the path.
        /// </summary>
        /// <value>
        /// The path.
        /// </value>
        public string Path { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Store"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="t">The t.</param>
        public Store(string path, Type t)
        {
            this.Path = string.Format("{0}/{1}.xml", path, t.GetGenericArguments().FirstOrDefault().Name);
        }

        /// <summary>
        /// Gets the specified items.
        /// </summary>
        /// <param name="t">The type of model.</param>
        /// <returns>
        /// object from XML
        /// </returns>
        public object Get(Type t)
        {
            XmlSerializer serializer = new XmlSerializer(t);
            TextReader reader = new StreamReader(this.Path);
            object items = serializer.Deserialize(reader);
            reader.Close();

            return items;
        }

        /// <summary>
        /// Sets the specified t.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <param name="list">The list.</param>
        /// <returns>
        /// true if XML was set to object
        /// </returns>
        public bool Set(Type t, object list)
        {
            try
            {
                if (t.GetGenericArguments().FirstOrDefault().Name != default(String))
                {
                    XmlSerializer serialize = new XmlSerializer(t);
                    TextWriter writer = new StreamWriter(this.Path);
                    serialize.Serialize(writer, list);
                    writer.Close();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Adds the specified t.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <param name="item">The item.</param>
        /// <returns>
        /// Not yet implemented
        /// </returns>
        public bool Add(Type t, object item)
        {
            try
            {
                if (t.GetGenericArguments().FirstOrDefault().Name != default(String))
                {
                    // TODO: Not yet implemented
                    var data = this.Get(t);
                    return false;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
