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

        public void UpdateSalary()
        {
            using(this.sqlConnection)
            {
                //Updating salary of an employee using Sql Query 
                string query = @"update Employe_Payroll set salary = 30000 where id = 6";

                SqlCommand command = new SqlCommand(query,this.sqlConnection);

                this.sqlConnection.Open();

                int result = command.ExecuteNonQuery();
                if(result == 0)
                {
                    Console.WriteLine("Query Not Executed..");
                }
                else
                {
                    Console.WriteLine("Query Executed successfully...");
                }
                this.sqlConnection.Close();
            }
        }
        public void UpdateBasePay()
        {
            EmployeeData data = new EmployeeData();
            using (this.sqlConnection)
            {
                SqlCommand command = new SqlCommand("UpdateBasePay", this.sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", data.id);
               // command.Parameters.AddWithValue("@name ", data.name);
                command.Parameters.AddWithValue("@salary", data.salary);
                this.sqlConnection.Open();
                int res = command.ExecuteNonQuery();
                if (res == 0)
                {
                    Console.WriteLine("Query NOt executed...");
                }
                else
                {
                    Console.WriteLine("Query executed successfully...");
                }
                this.sqlConnection.Close();
            }
        }

        public  void RetrieveBasedOnDate()
        {
            EmployeeData data = new EmployeeData();
            using (this.sqlConnection)
            {
                //Query to retrieve data from table
                string query = @"select * from Employe_Payroll  where startDate between cast('2018-01-01' as date) and  getdate()";
                SqlCommand command = new SqlCommand(query, this.sqlConnection);
                this.sqlConnection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    Console.WriteLine("Contacts in database are :");
                    while (reader.Read())
                    {
                        //using employeeDadta class to print records under given condition
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
                        Console.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12}", data.id, data.name, data.salary, data.startDate, data.gender, data.Phone, data.Address, data.Departmenr, data.Basicpay, data.Deductions, data.TaxablePay, data.IncomeTax, data.Netpay);
                        Console.WriteLine("\n");

                    }
                }
                else
                {
                    Console.WriteLine("No records exists under given Condition....");
                }
            }
        }
        public void AggregateFunctions()
        {
            using(this.sqlConnection)
            {
                //Query for retrieving agregate functions..
                string query = @"select sum(BasicPay) as Totalsalary, avg(BasicPay) as Average, min(BasicPay) as MinimumSalary,max(BasicPay) as MaximumSalary,Count(*) as Count from Employe_Payroll group by gender";
                SqlCommand command = new SqlCommand(query, this.sqlConnection);

                this.sqlConnection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        Console.WriteLine("Total salary of records is :"+reader[0]);
                        Console.WriteLine("Average salary of records is :" + reader[1]);
                        Console.WriteLine("Minimjm salary among records is :" + reader[2]);
                        Console.WriteLine("Maximum salary among records is :" + reader[3]);
                        Console.WriteLine("Total number  of records are :" + reader[4]);

                    }
                }
                else
                {
                    Console.WriteLine("No records exists in database under the given condition ....");
                }
                this.sqlConnection.Close();
            }
        }

    }
}
