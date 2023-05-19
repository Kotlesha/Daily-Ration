using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Daily_Food_Ration.Business_Layer.Models;

namespace Daily_Food_Ration.Data_Access_Layer.RepositoryInterfaces
{
    interface IDailyRationRepository
    {
        void AddProductToMealTime(User user, Product product, MealTime mealTime, int gramms);
        void RemoveProductFromMealTime(User user, int productId, MealTime mealTime);
        void UpdateProductInMealTime(User user, int productId, MealTime mealTime, int gramms);
        IEnumerable<Product> GetAllProductsInRation(User user);
        IEnumerable<Product> GetAllProductsByMealType(User user, MealTime mealTime);
        Product GetProductInMealTimeById(User user, MealTime mealTime, int productId);
        decimal GetAllCalories(User user);
        void ClearRationMealTime(User user, MealTime mealTime);
        void ClearRation(User user);
    }
}
