using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Daily_Food_Ration.Business_Layer.Models;
using Daily_Food_Ration.Data_Access_Layer.RepositoryClasses;
using Daily_Food_Ration.Data_Access_Layer.RepositoryInterfaces;
using Daily_Food_Ration.Service_Layer.ServicesInterfaces;
using Maroquio;

namespace Daily_Food_Ration.Service_Layer.ServicesClasses
{
    class CategoriesService : ICategoriesService
    {
        private static ICategoriesRepository _categoriesRepository = new CategoriesRepository();

        public bool AddCategory(Category category) => _categoriesRepository.AddCategory(category);

        public Category CreateCategory(string name, string description, SortableBindingList<Product> products) => new(name, description, products);

        public IEnumerable<Category> GetAllCategories() => _categoriesRepository.GetAllCategories();

        public Category GetCategoryById(int id) => _categoriesRepository.GetCategoryById(id);

        public void RemoveCategory(int categoryId) => _categoriesRepository.RemoveCategory(categoryId);

        public void UpdateCategory(int categoryId, Category category) => _categoriesRepository.UpdateCategory(categoryId, category);
    }
}
