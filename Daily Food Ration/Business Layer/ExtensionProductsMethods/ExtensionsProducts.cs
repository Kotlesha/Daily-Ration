using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Daily_Food_Ration.Business_Layer.Models;
using Maroquio;

namespace Daily_Food_Ration.Business_Layer.ExtensionMethods
{
    static class ExtensionsProducts
    {
        public static bool Contains(this IList<Product> products, string name)
        {
            foreach (var product in products)
            {
                if (product.Name.Equals(name.Trim(), StringComparison.CurrentCultureIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        public static int IndexOf(this IList<Product> products, string name)
        {
            for (int i = 0; i < products.Count; i++)
            {
                if (products[i].Name.Equals(name.Trim(), StringComparison.CurrentCultureIgnoreCase))
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
