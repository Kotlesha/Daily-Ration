using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maroquio;

namespace Daily_Food_Ration.Business_Layer.Models
{
    class DailyRation
    {
        public IDictionary<MealTime, SortableBindingList<Product>> dailyRation;

        public DailyRation()
        {
            dailyRation = new Dictionary<MealTime, SortableBindingList<Product>>()
            {
                [MealTime.Breakfast] = new(),
                [MealTime.Lunch] = new(),
                [MealTime.Dinner] = new()
            };
        }
    }
}
