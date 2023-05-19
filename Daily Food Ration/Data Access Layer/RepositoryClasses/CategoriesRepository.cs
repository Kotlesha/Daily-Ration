using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Daily_Food_Ration.Business_Layer.Models;
using Daily_Food_Ration.Business_Layer.ExtensionMethods;
using Daily_Food_Ration.Data_Access_Layer.RepositoryInterfaces;
using Maroquio;

namespace Daily_Food_Ration.Data_Access_Layer.RepositoryClasses
{
    class CategoriesRepository : ICategoriesRepository
    {
        public bool AddCategory(Category category)
        {
            if (!Db.categories.Contains(category.Name))
            {
                Db.categories.Add(category);
                return true;
            }

            return false;
        }

        public IEnumerable<Category> GetAllCategories() => Db.categories;

        public Category GetCategoryById(int id) => Db.categories[id];

        public void RemoveCategory(int categoryId) => Db.categories.RemoveAt(categoryId);

        public void UpdateCategory(int categoryId, Category category)
        {
            SortableBindingList<Product> products = Db.categories[categoryId].Products;

            if (category.Products.Count == 0)
            {
                category = new Category(category.Name, category.Description, products);
            }

            Db.categories[categoryId] = category;
        }
    }
}
