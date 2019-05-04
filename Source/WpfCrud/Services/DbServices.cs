using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCrud.Services
{
    //This class is here just as a quick way to expose the DBService objects to the code behind the User Controls
    public static class DbServices
    {
        public static EmployeesDbService employeesDbService = new EmployeesDbService();
        public static ProductsDbService productsDbService = new ProductsDbService();
    }
}
