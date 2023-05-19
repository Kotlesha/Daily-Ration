using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Daily_Food_Ration.Business_Layer.Models
{
    public class Product : ICloneable
    {
        [DisplayName("Название")]
        public string Name { get; private set; }

        [DisplayName("Граммы")]
        public int Gramms { get; private set; }

        [DisplayName("Белки")]
        public decimal Protein { get; private set; }

        [DisplayName("Жиры")]
        public decimal Fats { get; private set; }

        [DisplayName("Углеводы")]
        public decimal Carbs { get; private set; }

        [DisplayName("Калории")]
        public decimal Calories { get; private set; }

        public Product(string name, int gramms, decimal protein, decimal fats, decimal carbs, decimal calories)
        {
            Name = name;
            Gramms = gramms;
            Protein = protein;
            Fats = fats;
            Carbs = carbs;
            Calories = calories;
        }


        public void Update(int gramms)
        {
            Gramms = gramms;
            Protein = Protein * gramms / 100;
            Fats = Fats * gramms / 100;
            Carbs = Carbs * gramms / 100;
            Calories = Calories * gramms / 100;
        }


        public object Clone() => MemberwiseClone();

        public static Product operator+(Product product1, Product product2)
        {
            if (!product1.Name.Equals(product2.Name, StringComparison.CurrentCultureIgnoreCase))
            {
                return new Product(string.Empty, 0, 0.0m, 0.0m, 0.0m, 0.0m);
            }

            int gramms = product1.Gramms + product2.Gramms;
            decimal protein = product1.Protein + product2.Protein, fats = product1.Fats + product2.Fats;
            decimal carbs = product1.Carbs + product2.Carbs, calories = product1.Calories + product2.Calories;

            return new Product(product1.Name, gramms, protein, fats, carbs, calories);
        }

        public override string ToString() => $"{Name}";
    }
}
