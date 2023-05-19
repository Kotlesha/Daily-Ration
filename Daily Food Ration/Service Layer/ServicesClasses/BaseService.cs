using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Daily_Food_Ration.Business_Layer.Models;
using Daily_Food_Ration.Data_Access_Layer;
using Daily_Food_Ration.Service_Layer.ServicesInterfaces;

namespace Daily_Food_Ration.Service_Layer.ServicesClasses
{
    abstract class BaseService
    {
        private static Db db;
        public ICategoriesService CategoriesService { get; } = new CategoriesService();
        public IDailyRationService DailyRationService { get; } = new DailyRationService();
        public IProductsService ProductsService { get; } = new ProductsService();

        public BaseService(string connectionString) => db = new Db(connectionString);

        public void Serialize() => Db.Serialize();
    }
}
