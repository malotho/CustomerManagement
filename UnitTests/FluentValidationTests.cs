using CustomerManagementPortal.Validation;
using FluentValidation.TestHelper;
using LogicLayer.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class FluentValidationTests
    {
        private CategoryValidation validator;

        [TestInitialize]
        public void Setup()
        {
            validator = new CategoryValidation();
        }


        [TestMethod]
        public void Given_Empty_Category_Should_Retun_Error()
        {
            var model = new CategoryModel();
            var result = validator.TestValidate(model);
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public void Given_Invalid_Category_Parttern_Should_Retun_Error()
        {
            var expectedErrorCount = 1;
            var expectedErrorErrorMessage = "Category Code Should be in the format: ABC123";

            var model = new CategoryModel
            {
                CategoryCode = "SomeInvalidCode123"
            };
            var result = validator.TestValidate(model);
            var actualtErrorCount = result.Errors.Count();
            Assert.AreEqual(expectedErrorCount, result.Errors.Count(), $"Expected to find {expectedErrorCount} error(s), but actially found {actualtErrorCount}");
            Assert.AreEqual(expectedErrorErrorMessage, result.Errors.Single().ErrorMessage);
            Assert.IsTrue(result.Errors.Single().PropertyName == "CategoryCode");
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public void Given_Valid_Category_Parttern_Should_Accept()
        {
            var expectedErrorCount = 0;

            var model = new CategoryModel
            {
                CategoryCode = "DND985"
            };
            var result = validator.TestValidate(model);
            var actualtErrorCount = result.Errors.Count();
            Assert.AreEqual(expectedErrorCount, result.Errors.Count(), $"Expected to find {expectedErrorCount} error(s), but actially found {actualtErrorCount}");
            Assert.IsTrue(result.IsValid);
        }
    }
}
