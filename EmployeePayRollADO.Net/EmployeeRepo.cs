using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePayRollADO.Net
{
    class EmployeeRepo
    {
        //Establishing connection
        public static string connectionString = @"Server = (localdb)\MSSQLLocalDB; Initial Catalog = payroll_serivce;";

        SqlConnection sqlConnection = new SqlConnection(connectionString);

        //Creating method to read data from Database
        public void GetEmployeData()
        {
            EmployeeData data = new EmployeeData();
            using (this.sqlConnection)
            {
                //select id,name,salary,startDate,gender,Phone,Address,Department,Basicpay,Dedcutions,TaxablePay,IncomeTax,NetPay from Employe_Payroll
                string query = @"select * from Employe_Payroll";

                SqlCommand command = new SqlCommand(query, this.sqlConnection);

                this.sqlConnection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if(reader.HasRows)
                {
                    Console.WriteLine("Contacts in database are :");
                    while(reader.Read())
                    {
                        data.id = reader.GetInt32(0);
                        data.name = reader.GetString(1);
                        data.salary = reader.GetDouble(2);
                        data.startDate = reader.GetDateTime(3);
                        data.gender = reader.GetString(4);
                        data.Phone = reader.GetInt64(5);
                        data.Address = reader.GetString(6);
                        data.Departmenr = reader.GetString(7);
                        data.Basicpay = reader.GetInt64(8);
                        data.Deductions = reader.GetInt32(9);
                        data.TaxablePay = reader.GetInt32(10);
                        data.IncomeTax = reader.GetInt32(11);
                        data.Netpay = reader.GetInt64(12);
                        //data.gender,data.Phone,data.Address,data.Departmenr,data.Basicpay,data.Deductions,data.TaxablePay,data.IncomeTax,data.Netpay);
                        Console.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12}",data.id,data.name,data.salary,data.startDate, data.gender, data.Phone, data.Address, data.Departmenr, data.Basicpay, data.Deductions, data.TaxablePay, data.IncomeTax, data.Netpay);
                        Console.WriteLine("\n");

                    }
                }
                else
                {
                    Console.WriteLine("Database is Empty....");
                }
                reader.Close();

                this.sqlConnection.Close();
            }
        }

    }
}
