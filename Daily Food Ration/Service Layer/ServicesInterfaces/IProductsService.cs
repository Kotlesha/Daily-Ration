using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Daily_Food_Ration.Data_Access_Layer.RepositoryInterfaces;
using Daily_Food_Ration.Business_Layer.Models;

namespace Daily_Food_Ration.Service_Layer.ServicesInterfaces
{
    interface IProductsService : IProductsRepository
    {
        Product CreateProduct(string name, int gramms, decimal protein, decimal fats, decimal carbs, decimal calories);
    }
}
