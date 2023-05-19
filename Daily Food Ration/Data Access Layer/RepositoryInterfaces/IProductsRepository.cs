using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Daily_Food_Ration.Business_Layer.Models;

namespace Daily_Food_Ration.Data_Access_Layer.RepositoryInterfaces
{
    interface IProductsRepository
    {
        bool AddProduct(int categoryId, Product product);
        void UpdateProduct(int categoryOldId, int categoryNewId, int productId, Product product);
        void RemoveProduct(int categoryId, int productId);
        IEnumerable<Product> GetAllProducts(int categoryId);
        IEnumerable<Product> GetAllProductsByName(int categoryId, string name);
        Product GetProductById(int categoryId, int productId);
        Product CreateCopyProduct(int categoryId, int productId, int gramms);
    }
}
