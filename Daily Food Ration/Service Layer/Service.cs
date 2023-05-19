using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Daily_Food_Ration.Business_Layer.Models;
using Daily_Food_Ration.Data_Access_Layer;
using Daily_Food_Ration.Service_Layer.ServicesClasses;

namespace Daily_Food_Ration.Service_Layer
{
    class Service : BaseService
    {
        public Service(string connectionString) : base(connectionString) { }
    }
}
