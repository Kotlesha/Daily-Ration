using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Daily_Food_Ration.Business_Layer.Models;
using Daily_Food_Ration.Data_Access_Layer.Xml;
using System.ComponentModel;
using Maroquio;

namespace Daily_Food_Ration.Data_Access_Layer
{
    [Serializable]
    public class Db
    {
        public static SortableBindingList<Category> categories;

        public Db(string connectionString)
        {
            categories = File.Exists(connectionString) ? XmlParser.ReadData(connectionString) : new SortableBindingList<Category>();
        }

        public static void Serialize() => XmlParser.WriteData(categories);
    }
}
