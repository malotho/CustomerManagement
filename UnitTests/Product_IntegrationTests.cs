using DataAccessLayer.Models;
using LogicLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTests
{
    [TestClass]
    public class Product_IntegrationTests
    {
        DbContextOptions options;
        [TestInitialize]
        public void MyClassInitialize()
        {
            options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer("Data Source=localhost,1433;Initial Catalog=master;Persist Security Info=True;User ID=sa;Password=yourStrong(!)Password")
                .Options;
        }

        [TestMethod]
        public void Given_New_Product_Should_Generate_ProductCode()
        {
            using (var con = new AppDbContext(options))
            {
                string expectedCODE = $"{DateTime.Today:yyyyMM}";
                var currentSequenceValue = con.GetCurrentSequenceValue();
                var service = new ProductService(con);
                var code = service.GenerateCode();
                Assert.AreEqual($"{expectedCODE}-{(currentSequenceValue + 1).ToString().PadLeft(3, '0')}",
                    code, "Expected product code to be next sequence");
            }
        }
    }
}
