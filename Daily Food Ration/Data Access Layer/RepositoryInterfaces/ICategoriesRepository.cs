using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Daily_Food_Ration.Business_Layer.Models;

namespace Daily_Food_Ration.Data_Access_Layer.RepositoryInterfaces
{
    interface ICategoriesRepository
    {
        bool AddCategory(Category category);
        void UpdateCategory(int categoryId, Category category);
        void RemoveCategory(int categoryId);
        IEnumerable<Category> GetAllCategories();
        Category GetCategoryById(int id);
    }
}
