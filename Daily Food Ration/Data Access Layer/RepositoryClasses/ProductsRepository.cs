using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Daily_Food_Ration.Business_Layer.Models;
using Daily_Food_Ration.Data_Access_Layer.RepositoryInterfaces;
using Daily_Food_Ration.Business_Layer.ExtensionMethods;
using System.ComponentModel;

namespace Daily_Food_Ration.Data_Access_Layer.RepositoryClasses
{
    class ProductsRepository : IProductsRepository
    {
        public bool AddProduct(int categoryId, Product product)
        {
            if (!Db.categories[categoryId].Products.Contains(product.Name))
            {
                Db.categories[categoryId].Products.Add(product);
                return true;
            }

            return false;
        }

        public Product CreateCopyProduct(int categoryId, int productId, int gramms)
        {
            Product product = GetProductById(categoryId, productId);
            var productCopy = product.Clone() as Product;
            productCopy.Update(gramms);
            return productCopy;
        }

        public IEnumerable<Product> GetAllProducts(int categoryId) => Db.categories[categoryId].Products;

        public IEnumerable<Product> GetAllProductsByName(int categoryId, string name)
            => Db.categories[categoryId].Products.Where(product => product.Name.Contains(name, StringComparison.CurrentCultureIgnoreCase));

        public Product GetProductById(int categoryId, int productId) => Db.categories[categoryId].Products[productId];

        public void RemoveProduct(int categoryId, int productId) => Db.categories[categoryId].Products.RemoveAt(productId);

        public void UpdateProduct(int categoryOldId, int categoryNewId, int productId, Product product)
        {
            if (categoryOldId == categoryNewId)
            {
                Db.categories[categoryOldId].Products[productId] = product;
            }
            else
            {
                AddProduct(categoryNewId, product);
                RemoveProduct(categoryOldId, productId);
            }
        }
    }
}
