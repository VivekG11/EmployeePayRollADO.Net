using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmployeePayRollADO.Net;
using System;

namespace PayrollValidation
{
    [TestClass]
    public class UnitTest1
    {
        EmployeeRepo repo;
        ERRepo erepo;
        EmployeeData data;
        Transactions transaction;
        [TestInitialize]
        public void setup()
        {
            repo = new EmployeeRepo();
            erepo = new ERRepo();
            data = new EmployeeData();
            transaction = new Transactions();
        }
        [TestMethod]
        public void UpdateSalaryUsingStoredProcedurey()
        {

            int expected = 1;
            int actual = repo.UpdateSalary();
            Assert.AreEqual(expected, actual);

        }
        [TestMethod]
        public void RetrieveDataBasedOnDate()
        {

            int expected = 1;
            int actual = repo.RetrieveBasedOnDate();
            Assert.AreEqual(expected, actual);

        }







    }
}
