using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Daily_Food_Ration.Data_Access_Layer.RepositoryInterfaces;
using Daily_Food_Ration.Business_Layer.Models;
using System.ComponentModel;
using Maroquio;

namespace Daily_Food_Ration.Service_Layer.ServicesInterfaces
{
    interface ICategoriesService : ICategoriesRepository
    {
        Category CreateCategory(string name, string description, SortableBindingList<Product> products);
    }
}
