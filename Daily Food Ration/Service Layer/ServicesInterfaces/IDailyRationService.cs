using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Daily_Food_Ration.Business_Layer.Models;
using Daily_Food_Ration.Data_Access_Layer.RepositoryInterfaces;

namespace Daily_Food_Ration.Service_Layer.ServicesInterfaces
{
    interface IDailyRationService : IDailyRationRepository
    {
        DailyRation CreateDailyRation();

        User CreateUser(decimal weight, decimal height, int age, DailyActivity dailyActivity);
    }
}
