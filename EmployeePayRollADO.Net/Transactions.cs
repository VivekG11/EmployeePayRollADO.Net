using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace EmployeePayRollADO.Net
{
    public class Transactions
    {
        //establishing Connection
        public static string connectionString = @"Server = (localdb)\MSSQLLocalDB; Initial Catalog = payroll_serivce;";

        SqlConnection connection = new SqlConnection(connectionString);


        public void AddRecord()
        {
            using(this.connection)
            {
                this.connection.Open();

                SqlTransaction transaction = connection.BeginTransaction();

                SqlCommand command = connection.CreateCommand();
                command.Transaction = transaction;
                try
                {
                    command.CommandText = @"insert into Employee values(1,'Anil',96745,'BPT','2020-04-11','M')";
                    command.ExecuteNonQuery();
                    command.CommandText = @"INSERT INTO Payroll VALUES (24,29000,0,1000,1200,0);";
                    command.ExecuteNonQuery();
                    command.CommandText = @"Update Payroll set Taxablepay = BasicPay-Deductions where EmpId = 24";
                    command.ExecuteNonQuery();
                    command.CommandText = @"update Payroll set Netpay = Taxablepay - Incometax where EmpId = 24";
                    command.ExecuteNonQuery();
                    command.CommandText = @"insert into EmployeDepartment values(3,24)";
                    command.ExecuteNonQuery();

                    transaction.Commit();
                    Console.WriteLine("Record added successfully....");
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transaction.Rollback();
                }

                this.connection.Close();
            }
        }

        public void DeleteRecord()
        {
            using(this.connection)
            {
                //initializing is_active to true
                bool is_active = true;
                //query to delete a record from Employe Table
                string query = @"delete from Employee where EmployeId = 23";
                
                SqlCommand command = new SqlCommand(query, this.connection);
                //opening connection
                this.connection.Open();

                SqlTransaction transaction = connection.BeginTransaction();
                command.Transaction = transaction;

                int res = command.ExecuteNonQuery();
                transaction.Commit();
                //if transaction commits is_active returns to false
                    is_active = false;
                if (is_active == false)
                {
                    Console.WriteLine("Transaction committed successfully..");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Transaction Failed...");
                }
                //closing connection
                this.connection.Close();
                
            }
        }
    }
}
