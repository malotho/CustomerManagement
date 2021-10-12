using DataAccessLayer.Models;
using LogicLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class ProductTests
    {
        public TestContext TestContext { get; set; }

        private static TestContext _testContext;

        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
            _testContext = context;
            var options = new DbContextOptionsBuilder<AppDbContext>()
                    .UseInMemoryDatabase(databaseName: "master")
                    .Options;
            _testContext.Properties.Add("options", options);
            using (var con = new AppDbContext(options))
            {

                con.Categories.Add(new Category { ID = 1, CategoryName = "Beverage", CategoryCode = "BEV123", IsActive = true });
                con.Categories.Add(new Category { ID = 2, CategoryName = "Cosmentics", CategoryCode = "COS365", IsActive = true });
                con.Categories.Add(new Category { ID = 3, CategoryName = "Fruits", CategoryCode = "FRT745", IsActive = true });

                con.SaveChanges();

                con.Products.Add(new Product
                {
                    ID = 1,
                    ProductName = "Coffee",
                    Price = 2,
                    ProductCode = "202109-001",
                    Category = con.Categories.Single(p => p.ID == 1)
                });
                con.Products.Add(new Product
                {
                    ID = 2,
                    ProductName = "Milk",
                    Price = 36,
                    ProductCode = "202109-002",
                    Category = con.Categories.Single(p => p.ID == 1)
                });
                con.Products.Add(new Product
                {
                    ID = 3,
                    ProductName = "Roll-On",
                    Price = 45,
                    ProductCode = "202109-008",
                    Category = con.Categories.Single(p => p.ID == 2)
                });
                con.Products.Add(new Product
                {
                    ID = 4,
                    ProductName = "Banana",
                    Price = 10,
                    ProductCode = "202109-020",
                    Category = con.Categories.Single(p => p.ID == 3)
                });
                con.Products.Add(new Product
                {
                    ID = 5,
                    ProductName = "Apple",
                    Price = 5,
                    ProductCode = "202109-078",
                    Category = con.Categories.Single(p => p.ID == 3)
                });
                con.SaveChanges();
            }
        }

        [TestMethod]
        public void GetProducts_Should_Return_All_Products()
        {
            var options = _testContext.Properties["options"] as DbContextOptions;
            using (var con = new AppDbContext(options))
            {
                var service = new ProductService(con);
                var products = service.GetProducts().ToList();
                Assert.IsTrue(products.Count > 0);
            }
        }


        [TestMethod]
        public void Edit_Product_Should_Update_Product_Name()
        {
            var options = _testContext.Properties["options"] as DbContextOptions;
            using (var con = new AppDbContext(options))
            {
                var service = new ProductService(con);
                var coffee = con.Products.AsNoTracking()
                    .Include(c => c.Category)
                    .Single(p => p.ID == 1);
                Assert.AreEqual("Coffee", coffee.ProductName);

                coffee.ProductName = "Tea";
                var updated = service.UpdateProduct(1, coffee).GetAwaiter().GetResult();
                Assert.IsTrue(updated);
                var tea = service.Get(1).GetAwaiter().GetResult();
                Assert.AreEqual("Tea", tea.ProductName);
                Assert.IsFalse(con.Products.Any(P => P.ProductName == "Coffee"));
            }
        }

        [TestMethod]
        public void Delete_Product_Should_Remive_Product()
        {
            var options = _testContext.Properties["options"] as DbContextOptions;
            using (var con = new AppDbContext(options))
            {
                var service = new ProductService(con);
                var prods = service.GetProducts();

                var countBefore = prods.Count();

                var deleted = service.Remove(4).GetAwaiter().GetResult();
                Assert.IsTrue(deleted);
                prods = service.GetProducts();

                Assert.AreEqual(4, prods.Count());

            }
        }
    }
}
