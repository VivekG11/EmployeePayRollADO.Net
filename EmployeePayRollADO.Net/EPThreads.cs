using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Text;

namespace EmployeePayRollADO.Net
{
    public class EPThreads
    {
        public static string connectionString = @"Server = (localdb)\MSSQLLocalDB; Initial Catalog = payroll_serivce;";

        SqlConnection connection = new SqlConnection(connectionString);

        public void RetrieveData()
        {
            string query = @"select CmpId,CompanyName,EmpId,EmployeName,Address,Phone,StartDate,gender,BasicPay,Deductions,TaxablePay,IncomeTax,NetPay,DeptName
                            FROM Company
                            INNER JOIN Employee on Company.CmpId = Employee.CompanyId
                            INNER JOIN Payroll on Payroll.EmpId = Employee.EmployeId
                            INNER JOIN EmployeDepartment on Employee.EmployeId = EmployeDepartment.EmployeId
                            INNER JOIN Department on Department.DeptId = EmployeDepartment.DeptId;";

            SqlCommand command = new SqlCommand(query, this.connection);
            this.connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            if(reader.HasRows)
            {
                while(reader.Read())
                {

                }
            }

        }
    }
}
