﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeePayRollADO.Net
{
    public class ERRepo
    {
        //Establishing connection
        public static string connectionString = @"Server = (localdb)\MSSQLLocalDB; Initial Catalog = payroll_serivce;";

        SqlConnection connection = new SqlConnection(connectionString);

        //Object for EmployeeData class
        EmployeeData data = new EmployeeData();
        public  void RetrieveDetails()
        {
            List<EmployeeData> EmpList = new List<EmployeeData>();
           // EmployeeData data = new EmployeeData();
            using(this.connection)
            {
                string query = @"select CmpId,CompanyName,EmpId,EmployeName,Address,Phone,StartDate,gender,BasicPay,Deductions,TaxablePay,IncomeTax,NetPay,DeptName
                                 FROM Company
                                 INNER JOIN Employee on Company.CmpId = Employee.CompanyId
                                 INNER JOIN Payroll on Payroll.EmpId = Employee.EmployeId
                                 INNER JOIN EmployeDepartment on Employee.EmployeId = EmployeDepartment.EmployeId
                                 INNER JOIN Department on Department.DeptId = EmployeDepartment.DeptId";

                SqlCommand command = new SqlCommand(query, this.connection);

                this.connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        data.ComapnyId = reader.GetInt32(0);
                        data.CompanyName = reader.GetString(1);
                        data.id = reader.GetInt32(2);
                        data.name = reader.GetString(3);
                        data.Address = reader.GetString(4);
                        data.Phone = reader.GetInt64(5);
                        data.startDate = reader.GetDateTime(6);
                        data.gender = reader.GetString(7);
                        data.Basicpay = reader.GetInt64(8);
                        data.Deductions = reader.GetInt32(9);
                        data.TaxablePay = reader.GetInt32(10);
                        data.IncomeTax = reader.GetInt32(11);
                        data.Netpay = reader.GetInt64(12);
                        data.Departmenr = reader.GetString(13);
                        Thread task = new Thread(() =>
                        {
                          //  EmpList.Add(data);
                            Console.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13}", data.ComapnyId, data.CompanyName, data.id, data.name, data.Address, data.Phone, data.startDate, data.gender, data.Basicpay, data.Deductions, data.TaxablePay, data.IncomeTax, data.Netpay, data.Departmenr);
                            Console.WriteLine("\n");
                        });
                        task.Start();
                    }
                }
                else
                {
                    Console.WriteLine("No records exist.....");
                }
                this.connection.Close();
            }
        }

        public void RetrieveWihtoutThread()
        {
            //creating object for stopwatch method
            Stopwatch stopwatch = new Stopwatch();
            //start and stop the stopwatch to get the elapsed time
            stopwatch.Start();
            RetrieveDetails();
            stopwatch.Stop();
            //Printing time taken to retrieve details
            Console.WriteLine("Time taken to retrieve data is :"+stopwatch.ElapsedMilliseconds+" ms");
        }

        public void RetrieveUsingThread()
        {
            //creating object for stopwatch method
            Stopwatch stopwatch = new Stopwatch();
            //start and stop the stopwatch to get the elapsed time
            stopwatch.Start();
            RetrieveDetails();
            stopwatch.Stop();
            //Printing time taken to retrieve details
            Console.WriteLine("Time taken to retrieve data is :" + stopwatch.ElapsedMilliseconds + " ms");
        }

        public void RetrieveRecordsOverAperiod()
        {
            using(this.connection)
            {
                //Query to retrieve records based on certain period
                string query = @"SELECT CmpId CompanyName,EmployeId,EmployeName
                               From Company
                               INNER JOIN Employee on Company.CmpId = Employee.CompanyId where StartDate BETWEEN Cast('2017-03-01' as Date) and cast('2019-11-11' as Date)";

                this.connection.Open();
                SqlCommand command = new SqlCommand(query, this.connection);

                SqlDataReader reader = command.ExecuteReader();

                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        data.ComapnyId = reader.GetInt32(0);
                        data.id = reader.GetInt32(1);
                        data.name = reader.GetString(2);

                        Console.WriteLine("{0},{1},{2},{3} ",data.ComapnyId,data.CompanyName,data.id,data.name);
                    }
                }
                else
                {
                    Console.WriteLine("No records exists under given Condition.......");
                }
                this.connection.Close();

            }
        }

        public void RecordsConuntBasedOnGender()
        {
            using (this.connection)
            {
                //Query to retrieve records based on certain period
                string query = @"select Count(EmpId) as EmployeeCount , gender 
                                 from Employee
                                 INNER JOIN Payroll on Employee.EmployeId = Payroll.EmpId group by gender";
                this.connection.Open();
                SqlCommand command = new SqlCommand(query, this.connection);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        data.count = reader.GetInt32(0);
                        data.gender = reader.GetString(1);

                        //returning gender wise employees
                        Console.WriteLine("{0} : {1}",data.gender,data.count);
                    }
                }
                else
                {
                    Console.WriteLine("No records exists under given Condition.......");
                }
                this.connection.Close();

            }
        }

        public void AggregateFunctions()
        {
            using (this.connection)
            {
                //Query to retrieve salary details among employees
                string query = @"select sum(BasicPay) as TotalPay,Max(BasicPay) as MaximumSalary,Min(Basicpay) as Minimumsalary,Avg(BasicPay)as Averagepay , gender 
                                from Employee
                                INNER JOIN Payroll on Employee.EmployeId = Payroll.EmpId group by gender";

                
                this.connection.Open();
                //initializing command type
                SqlCommand command = new SqlCommand(query, this.connection);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //Displaying details among employees grouped by gender
                        Console.WriteLine("Total Salary = {0}, maximum pay = {1}, Minimumpay = {2}, Averagepay = {3} , {4}",reader[0],reader[1],reader[2],reader[3],reader[4]);
                    }
                }
                else
                {
                    Console.WriteLine("No records exists under given Condition.......");
                }
                this.connection.Close();

            }
        }

    }
}
