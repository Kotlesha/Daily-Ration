using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Daily_Food_Ration.Business_Layer.Models;
using Maroquio;

namespace Daily_Food_Ration.Data_Access_Layer.Xml
{
    static class XmlParser
    {
        public static SortableBindingList<Category> ReadData(string path)
        {
            SortableBindingList<Category> categories = new();
            XDocument document = XDocument.Load(path, LoadOptions.PreserveWhitespace);

            foreach (XElement el in document.Root.Elements("Category"))
            {
                SortableBindingList<Product> categoryProducts = new();

                foreach (XElement element in el.Elements("Product"))
                {
                    string productName = element.Element("Name").Value;
                    int productGramms = Convert.ToInt32(element.Element("Gramms").Value);
                    decimal productProtein = Convert.ToDecimal(element.Element("Protein").Value);
                    decimal productFats = Convert.ToDecimal(element.Element("Fats").Value);
                    decimal productCarbs = Convert.ToDecimal(element.Element("Carbs").Value);
                    decimal productCalories = Convert.ToDecimal(element.Element("Calories").Value);

                    Product product = new(productName, productGramms, productProtein, productFats, productCarbs, productCalories);
                    categoryProducts.Add(product);
                }

                string categoryName = el.Attribute("name").Value;
                string categoryDescription = el.Attribute("description").Value;

                Category category = new(categoryName, categoryDescription, categoryProducts);
                categories.Add(category);
            }

            return categories;
        }

        public static void WriteData(SortableBindingList<Category> categories)
        {
            XmlDocument xmlDoc = new();
            XmlNode Db = xmlDoc.CreateElement("Db");
            xmlDoc.AppendChild(Db);
            
            foreach (var category in categories)
            {
                XmlNode categoryNode = xmlDoc.CreateElement("Category");
                XmlAttribute name = xmlDoc.CreateAttribute("name");
                name.Value = $"{category.Name}";
                categoryNode.Attributes.Append(name);
                XmlAttribute description = xmlDoc.CreateAttribute("description");
                description.Value = $"{category.Description}";
                categoryNode.Attributes.Append(description);
                Db.AppendChild(categoryNode);

                foreach (var product in category.Products)
                {
                    XmlNode productNode = xmlDoc.CreateElement("Product");
                    categoryNode.AppendChild(productNode);
                    XmlNode productName = xmlDoc.CreateElement("Name");
                    productName.InnerText = product.Name;
                    productNode.AppendChild(productName);
                    XmlNode productGramms = xmlDoc.CreateElement("Gramms");
                    productGramms.InnerText = product.Gramms.ToString();
                    productNode.AppendChild(productGramms);
                    XmlNode productProtein = xmlDoc.CreateElement("Protein");
                    productProtein.InnerText = product.Protein.ToString().Replace(".", ",");
                    productNode.AppendChild(productProtein);
                    XmlNode productFats = xmlDoc.CreateElement("Fats");
                    productFats.InnerText = product.Fats.ToString().Replace(".", ",");
                    productNode.AppendChild(productFats);
                    XmlNode productCarbs = xmlDoc.CreateElement("Carbs");
                    productCarbs.InnerText = product.Carbs.ToString().Replace(".", ",");
                    productNode.AppendChild(productCarbs);
                    XmlNode productCalories = xmlDoc.CreateElement("Calories");
                    productCalories.InnerText = product.Calories.ToString().Replace(".", ",");
                    productNode.AppendChild(productCalories);
                }
            }
            

            xmlDoc.Save("FoodProducts.xml");
        }
    }
}
