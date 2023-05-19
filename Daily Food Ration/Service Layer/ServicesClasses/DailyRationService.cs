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
    class DailyRationService : IDailyRationService
    {
        private static IDailyRationRepository _dailyRationRepository = new DailyRationRepository();

        public void AddProductToMealTime(User user, Product product, MealTime mealTime, int gramms) => _dailyRationRepository.AddProductToMealTime(user, product, mealTime, gramms);

        public void ClearRation(User user) => _dailyRationRepository.ClearRation(user);

        public void ClearRationMealTime(User user, MealTime mealTime) => _dailyRationRepository.ClearRationMealTime(user, mealTime);

        public DailyRation CreateDailyRation() => new();

        public User CreateUser(decimal weight, decimal height, int age, DailyActivity dailyActivity)
        {
            return new(weight, height, age, dailyActivity);
        }

        public decimal GetAllCalories(User user) => _dailyRationRepository.GetAllCalories(user);

        public IEnumerable<Product> GetAllProductsByMealType(User user, MealTime mealTime) => _dailyRationRepository.GetAllProductsByMealType(user, mealTime);

        public IEnumerable<Product> GetAllProductsInRation(User user) => _dailyRationRepository.GetAllProductsInRation(user);

        public Product GetProductInMealTimeById(User user, MealTime mealTime, int productId) => _dailyRationRepository.GetProductInMealTimeById(user, mealTime, productId);

        public void RemoveProductFromMealTime(User user, int productId, MealTime mealTime) => _dailyRationRepository.RemoveProductFromMealTime(user, productId, mealTime);

        public void UpdateProductInMealTime(User user, int productId, MealTime mealTime, int gramms) => _dailyRationRepository.UpdateProductInMealTime(user, productId, mealTime, gramms);
    }
}
