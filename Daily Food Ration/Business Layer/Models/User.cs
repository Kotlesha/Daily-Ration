using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daily_Food_Ration.Business_Layer.Models
{
    class User
    {
        public decimal Weight { get; private set; }
        public decimal Height { get; private set; }
        public int Age { get; private set; }
        public DailyActivity DailyActivity { get; private set; }
        public decimal DailyCaloriesRate { get; private set; }
        public DailyRation DailyRation { get; private set; }

        public User(decimal weight, decimal height, int age, DailyActivity dailyActivity)
        {
            Weight = weight;
            Height = height;
            Age = age;
            DailyActivity = dailyActivity;
            DailyCaloriesRate = CaloriesData.GetDailyCaloriesRate(dailyActivity, weight, height, age);
            DailyRation = new();
        }

        public void Update(decimal weight, decimal height, int age, DailyActivity dailyActivity)
        {
            Weight = weight;
            Height = height;
            Age = age;
            DailyActivity = dailyActivity;
            DailyCaloriesRate = CaloriesData.GetDailyCaloriesRate(dailyActivity, weight, height, age);
        }
    }
}
