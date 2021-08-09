using System;

namespace EmployeePayRollADO.Net
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Employee Employee payroll Services...");
            EmployeeRepo repo = new EmployeeRepo();
            // repo.RetrieveBasedOnDate();
            // repo.AggregateFunctions();
            //repo.UpdateBasePay();
            // repo.UpdateSalary();
            // repo.GetEmployeData();
            ERRepo er = new ERRepo();
          //  er.RetrieveWihtoutThread();
            er.RetrieveUsingThread();
            //  er.RetrieveRecordsOverAperiod();
            //er.RecordsConuntBasedOnGender();
            // er.AggregateFunctions();
            Transactions transactions = new Transactions();
            // transactions.AddRecord();
          //  transactions.DeleteRecord();
            
        }
        public void PrintDetails()
        {

        }
    }
}
