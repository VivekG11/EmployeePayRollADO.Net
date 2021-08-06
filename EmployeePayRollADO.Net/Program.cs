using System;

namespace EmployeePayRollADO.Net
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Employee Employee payroll Services...");
            EmployeeRepo repo = new EmployeeRepo();
            repo.RetrieveBasedOnDate();
            //repo.UpdateBasePay();
           // repo.UpdateSalary();
           // repo.GetEmployeData();
        }
    }
}
