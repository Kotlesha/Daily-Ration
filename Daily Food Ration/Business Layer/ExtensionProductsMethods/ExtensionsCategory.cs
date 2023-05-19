using Daily_Food_Ration.Business_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daily_Food_Ration.Business_Layer.ExtensionMethods
{
    static class ExtensionsCategory
    {
        public static bool Contains(this IList<Category> categories, string name)
        {
            foreach (var category in categories)
            {
                if (category.Name.Equals(name.Trim(), StringComparison.CurrentCultureIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
