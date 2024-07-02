using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Repository_Source
{
    public class XMLSet<TModel> where TModel : class
    {
        private string _filename;
        private ICollection<TModel> _models;

        // Constructor
        public XMLSet(string FileName)
        {
            this._filename = FileName;
        }

        // Property to get or set the collection of objects
        public ICollection<TModel> Data
        {
            get
            {
                try
                {
                    // If _models is null, try to load data from the XML file
                    if (_models == null) Load();
                }
                catch (Exception)
                {
                    // If an exception occurs during loading, initialize _models as an empty list
                    _models = new List<TModel>();
                }
                return _models;
            }
            set
            {
                // When the property is set, update _models and save the changes to the XML file
                _models = value;
                Save();
            }
        }

        // Method to save the collection of objects to the XML file
        public void Save()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<TModel>));
            StreamWriter sw = new StreamWriter(this._filename);
            serializer.Serialize(sw, this._models);
            sw.Close();
            sw.Dispose();
        }

        // Method to load the collection of objects from the XML file
        public void Load()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<TModel>));
            StreamReader sr = new StreamReader(this._filename);
            this._models = serializer.Deserialize(sr) as List<TModel>;
            sr.Close();
            sr.Dispose();
        }
        

    }
}
