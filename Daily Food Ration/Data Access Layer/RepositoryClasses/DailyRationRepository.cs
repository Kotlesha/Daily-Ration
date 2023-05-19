using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Daily_Food_Ration.Business_Layer.Models;
using Daily_Food_Ration.Business_Layer.ExtensionMethods;
using Daily_Food_Ration.Data_Access_Layer.RepositoryInterfaces;

namespace Daily_Food_Ration.Data_Access_Layer.RepositoryClasses
{
    class DailyRationRepository : IDailyRationRepository
    {
        public void AddProductToMealTime(User user, Product product, MealTime mealTime, int gramms)
        {
            var copyProduct = product.Clone() as Product;
            copyProduct.Update(gramms);

            if (!user.DailyRation.dailyRation[mealTime].Contains(copyProduct.Name))
            {
                user.DailyRation.dailyRation[mealTime].Add(copyProduct);
                return;
            }

            int index = user.DailyRation.dailyRation[mealTime].IndexOf(product.Name);
            Product resultProduct = copyProduct + user.DailyRation.dailyRation[mealTime][index];
            user.DailyRation.dailyRation[mealTime][index] = resultProduct;
        }

        public void ClearRation(User user)
        {
            foreach (var element in user.DailyRation.dailyRation)
            {
                element.Value.Clear();
            }
        }

        public void ClearRationMealTime(User user, MealTime mealTime) => user.DailyRation.dailyRation[mealTime].Clear();

        public decimal GetAllCalories(User user)
        {
            decimal totalValue = 0.0m;

            foreach (var products in user.DailyRation.dailyRation)
            {
                foreach (var product in products.Value)
                {
                    totalValue += product.Calories;
                }
            }

            return totalValue;
        }

        public IEnumerable<Product> GetAllProductsByMealType(User user, MealTime mealTime) => user.DailyRation.dailyRation[mealTime];

        public IEnumerable<Product> GetAllProductsInRation(User user) => from products in user.DailyRation.dailyRation.Values
                                                                         from product in products
                                                                         select product;

        public Product GetProductInMealTimeById(User user, MealTime mealTime, int productId) =>
            user.DailyRation.dailyRation[mealTime][productId];

        public void RemoveProductFromMealTime(User user, int productId, MealTime mealTime) => user.DailyRation.dailyRation[mealTime].RemoveAt(productId);

        public void UpdateProductInMealTime(User user, int productId, MealTime mealTime, int gramms)
        {
            var product = user.DailyRation.dailyRation[mealTime][productId];
            var copyProduct = product.Clone() as Product;
            copyProduct.Update(gramms);
            user.DailyRation.dailyRation[mealTime][productId] = copyProduct;
        }
    }
}
