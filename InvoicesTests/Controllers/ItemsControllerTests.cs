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
    public class ItemsControllerTests
    {

        [Fact]
        public void Create_ReturnsViewResultWithItemModelForCorrectInvoiceId()
        {
            // Arrange
            var mockRepo = new Mock<IInvoicesRepository>();
            var controller = new ItemsController(mockRepo.Object);

            // Act
            var result = controller.Create(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Item>(viewResult.ViewData.Model);
            Assert.Equal(1, model.InvoiceId);

        }

        [Fact]
        public async Task Create_RedirectsToInvoicesDetails()
        {
            var item = new Item(){Id = 1};
            // Arrange
            var mockRepo = new Mock<IInvoicesRepository>();
            mockRepo.Setup(repo => repo.AddItemAsync(item))
                .ReturnsAsync(item);
            var controller = new ItemsController(mockRepo.Object);

            // Act
            var result = await controller.Create(item);

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Details", redirect.ActionName);
            Assert.Equal("Invoices", redirect.ControllerName);
        }

        [Fact]
        public async Task Index_ReturnsViewResultWithListOfItem()
        {
            // Arrange
            var Item = new List<Item> { new Item() { Id = 1 } };
            var mockRepo = new Mock<IInvoicesRepository>();
            mockRepo.Setup(repo => repo.ListItemsAsync(1)).ReturnsAsync(Item);
            var controller = new ItemsController(mockRepo.Object);

            // Act
            var result = await controller.Index(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Item>>(viewResult.ViewData.Model);
            Assert.Single(model);
        }

        [Fact]
        public async Task Edit_ReturnsViewResultWithItemModel()
        {
            // Arrange
            var Item = new Item() { Id = 1 };
            var mockRepo = new Mock<IInvoicesRepository>();
            mockRepo.Setup(repo => repo.GetItemAsync(Item.Id)).ReturnsAsync(Item);
            var controller = new ItemsController(mockRepo.Object);

            // Act
            var result = await controller.Edit(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Item>(viewResult.ViewData.Model);
            Assert.Equal(Item.Id, model.Id);
        }

        [Fact]
        public async Task Edit_RedirectsToInvoicesDetails()
        {
            var item = new Item() { Id = 1 };
            // Arrange
            var mockRepo = new Mock<IInvoicesRepository>();
            mockRepo.Setup(repo => repo.UpdateItemAsync(item))
                .ReturnsAsync(item);
            var controller = new ItemsController(mockRepo.Object);

            // Act
            var result = await controller.Edit(1, item);

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Details", redirect.ActionName);
            Assert.Equal("Invoices", redirect.ControllerName);
        }

        [Fact]
        public async Task Delete_ReturnsViewResultWithItem()
        {
            var item = new Item() { Id = 1 };
            // Arrange
            var mockRepo = new Mock<IInvoicesRepository>();
            mockRepo.Setup(repo => repo.GetItemAsync(item.Id))
                .ReturnsAsync(item);
            var controller = new ItemsController(mockRepo.Object);

            // Act
            var result = await controller.Delete(item.Id);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Item>(viewResult.ViewData.Model);
            Assert.Equal(item.Id, model.Id);
        }

        [Fact]
        public async Task DeleteConfirmed_Redirects()
        {
            // Arrange
            var mockRepo = new Mock<IInvoicesRepository>();
            var controller = new ItemsController(mockRepo.Object);

            // Act
            var result = await controller.DeleteConfirmed(1);

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }

    }
}