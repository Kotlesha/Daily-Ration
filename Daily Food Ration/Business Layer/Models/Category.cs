using Maroquio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Daily_Food_Ration.Business_Layer.Models
{
    public class Category
    {
        [DisplayName("Название категории")]
        public string Name { get; private set; }

        [DisplayName("Описание категории")]
        public string Description { get; private set; }

        public SortableBindingList<Product> Products { get; private set; }

        public Category(string name, string description, SortableBindingList<Product> products)
        {
            Name = name;
            Description = description;
            Products = products;
        }

        public override string ToString() => $"{Name}";
    }
}
