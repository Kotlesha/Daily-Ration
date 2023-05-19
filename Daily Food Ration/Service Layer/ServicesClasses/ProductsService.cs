using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Daily_Food_Ration.Business_Layer.Models;
using Daily_Food_Ration.Data_Access_Layer.RepositoryClasses;
using Daily_Food_Ration.Data_Access_Layer.RepositoryInterfaces;
using Daily_Food_Ration.Service_Layer.ServicesInterfaces;


namespace Daily_Food_Ration.Service_Layer.ServicesClasses
{
    class ProductsService : IProductsService
    {
        private static IProductsRepository _productsRepository = new ProductsRepository();

        public bool AddProduct(int categoryId, Product product) => _productsRepository.AddProduct(categoryId, product);

        public Product CreateCopyProduct(int categoryId, int productId, int gramms) => _productsRepository.CreateCopyProduct(categoryId, productId, gramms);

        public Product CreateProduct(string name, int gramms, decimal protein, decimal fats, decimal carbs, decimal calories) => new(name, gramms, protein, fats, carbs, calories);

        public IEnumerable<Product> GetAllProducts(int categoryId) => _productsRepository.GetAllProducts(categoryId);

        public IEnumerable<Product> GetAllProductsByName(int categoryId, string name) => _productsRepository.GetAllProductsByName(categoryId, name);

        public Product GetProductById(int categoryId, int productId) => _productsRepository.GetProductById(categoryId, productId);

        public void RemoveProduct(int categoryId, int productId) => _productsRepository.RemoveProduct(categoryId, productId);

        public void UpdateProduct(int categoryOldId, int categoryNewId, int productId, Product product) => _productsRepository.UpdateProduct(categoryOldId, categoryNewId, productId, product);
    }
}
