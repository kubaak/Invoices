using System.Collections.Generic;
using Xunit;
using Invoices.Data;
using System.Threading.Tasks;
using Invoices.Controllers;
using Moq;
using Invoices.Models;
using Microsoft.AspNetCore.Mvc;

namespace InvoicesTests.Controllers
{
    public class SuppliersControllerTests
    {

        [Fact]
        public void Create_ReturnsViewResultWithoutModel()
        {
            // Arrange
            var mockRepo = new Mock<IInvoicesRepository>();
            var controller = new SuppliersController(mockRepo.Object);

            // Act
            var result = controller.Create();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewData.Model);
        }

        [Fact]
        public async Task Create_RedirectsToIndex()
        {
            var supplier = new Supplier(){Id = 1};
            // Arrange
            var mockRepo = new Mock<IInvoicesRepository>();
            mockRepo.Setup(repo => repo.AddSupplierAsync(supplier))
                .ReturnsAsync(supplier);
            var controller = new SuppliersController(mockRepo.Object);

            // Act
            var result = await controller.Create(supplier);

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }

        [Fact]
        public async Task Index_ReturnsViewResultWithListOfSupplier()
        {
            // Arrange
            var supplier = new List<Supplier> { new Supplier() { Id = 1 } };
            var mockRepo = new Mock<IInvoicesRepository>();
            mockRepo.Setup(repo => repo.ListSuppliersAsync()).ReturnsAsync(supplier);
            var controller = new SuppliersController(mockRepo.Object);

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Supplier>>(viewResult.ViewData.Model);
            Assert.Single(model);
        }

        [Fact]
        public async Task Details_ReturnsViewResultWithSupplier()
        {
            // Arrange
            var supplier = new Supplier() { Id = 1 };
            var mockRepo = new Mock<IInvoicesRepository>();
            mockRepo.Setup(repo => repo.GetSupplierAsync(supplier.Id)).ReturnsAsync(supplier);
            var controller = new SuppliersController(mockRepo.Object);

            // Act
            var result = await controller.Details(supplier.Id);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Supplier>(viewResult.ViewData.Model);
            Assert.Equal(supplier.Id, model.Id);
        }

        [Fact]
        public async Task Edit_ReturnsViewResultWithSupplierModel()
        {
            // Arrange
            var supplier = new Supplier() { Id = 1 };
            var mockRepo = new Mock<IInvoicesRepository>();
            mockRepo.Setup(repo => repo.GetSupplierAsync(supplier.Id)).ReturnsAsync(supplier);
            var controller = new SuppliersController(mockRepo.Object);

            // Act
            var result = await controller.Edit(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Supplier>(viewResult.ViewData.Model);
            Assert.Equal(supplier.Id, model.Id);
        }

        [Fact]
        public async Task Edit_Redirects()
        {
            var supplier = new Supplier() { Id = 1 };
            // Arrange
            var mockRepo = new Mock<IInvoicesRepository>();
            mockRepo.Setup(repo => repo.AddSupplierAsync(supplier))
                .ReturnsAsync(supplier);
            var controller = new SuppliersController(mockRepo.Object);

            // Act
            var result = await controller.Edit(1, supplier);

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }

        [Fact]
        public async Task Delete_ReturnsViewResultWithSupplier()
        {
            var supplier = new Supplier() { Id = 1 };
            // Arrange
            var mockRepo = new Mock<IInvoicesRepository>();
            mockRepo.Setup(repo => repo.GetSupplierAsync(supplier.Id))
                .ReturnsAsync(supplier);
            var controller = new SuppliersController(mockRepo.Object);

            // Act
            var result = await controller.Delete(supplier.Id);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Supplier>(viewResult.ViewData.Model);
            Assert.Equal(supplier.Id, model.Id);
        }

        [Fact]
        public async Task DeleteConfirmed_Redirects()
        {
            // Arrange
            var mockRepo = new Mock<IInvoicesRepository>();
            var controller = new SuppliersController(mockRepo.Object);

            // Act
            var result = await controller.DeleteConfirmed(1);

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }

    }
}