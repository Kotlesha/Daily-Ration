using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daily_Food_Ration.Business_Layer.Models
{
    static class CaloriesData
    {
        public static decimal GetARM(DailyActivity dailyActivity) => dailyActivity switch
        {
            DailyActivity.Low => 1.2m,
            DailyActivity.Normal => 1.375m,
            DailyActivity.Average => 1.55m,
            DailyActivity.High => 1.725m,
            _ => 0.0m
        };

        public static decimal GetBMR(decimal weight, decimal height, int age) =>
            447.593m + 9.247m * weight + 3.098m * height - 4.330m * age;

        public static decimal GetDailyCaloriesRate(DailyActivity dailyActivity, decimal weight, decimal height, int age)
            => GetARM(dailyActivity) + GetBMR(weight, height, age);
    }
}
