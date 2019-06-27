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
    public class SubscribersControllerTests
    {

        [Fact]
        public void Create_ReturnsViewResultWithoutModel()
        {
            // Arrange
            var mockRepo = new Mock<IInvoicesRepository>();
            var controller = new SubscribersController(mockRepo.Object);

            // Act
            var result = controller.Create();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewData.Model);
        }

        [Fact]
        public async Task Create_RedirectsToIndex()
        {
            var Subscriber = new Subscriber(){Id = 1};
            // Arrange
            var mockRepo = new Mock<IInvoicesRepository>();
            mockRepo.Setup(repo => repo.AddSubscriberAsync(Subscriber))
                .ReturnsAsync(Subscriber);
            var controller = new SubscribersController(mockRepo.Object);

            // Act
            var result = await controller.Create(Subscriber);

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }

        [Fact]
        public async Task Index_ReturnsViewResultWithListOfSubscriber()
        {
            // Arrange
            var Subscriber = new List<Subscriber> { new Subscriber() { Id = 1 } };
            var mockRepo = new Mock<IInvoicesRepository>();
            mockRepo.Setup(repo => repo.ListSubscribersAsync()).ReturnsAsync(Subscriber);
            var controller = new SubscribersController(mockRepo.Object);

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Subscriber>>(viewResult.ViewData.Model);
            Assert.Single(model);
        }

        [Fact]
        public async Task Details_ReturnsViewResultWithSubscriber()
        {
            // Arrange
            var Subscriber = new Subscriber() { Id = 1 };
            var mockRepo = new Mock<IInvoicesRepository>();
            mockRepo.Setup(repo => repo.GetSubscriberAsync(Subscriber.Id)).ReturnsAsync(Subscriber);
            var controller = new SubscribersController(mockRepo.Object);

            // Act
            var result = await controller.Details(Subscriber.Id);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Subscriber>(viewResult.ViewData.Model);
            Assert.Equal(Subscriber.Id, model.Id);
        }

        [Fact]
        public async Task Edit_ReturnsViewResultWithSubscriberModel()
        {
            // Arrange
            var Subscriber = new Subscriber() { Id = 1 };
            var mockRepo = new Mock<IInvoicesRepository>();
            mockRepo.Setup(repo => repo.GetSubscriberAsync(Subscriber.Id)).ReturnsAsync(Subscriber);
            var controller = new SubscribersController(mockRepo.Object);

            // Act
            var result = await controller.Edit(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Subscriber>(viewResult.ViewData.Model);
            Assert.Equal(Subscriber.Id, model.Id);
        }

        [Fact]
        public async Task Edit_Redirects()
        {
            var Subscriber = new Subscriber() { Id = 1 };
            // Arrange
            var mockRepo = new Mock<IInvoicesRepository>();
            mockRepo.Setup(repo => repo.AddSubscriberAsync(Subscriber))
                .ReturnsAsync(Subscriber);
            var controller = new SubscribersController(mockRepo.Object);

            // Act
            var result = await controller.Edit(1, Subscriber);

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }

        [Fact]
        public async Task Delete_ReturnsViewResultWithSubscriber()
        {
            var Subscriber = new Subscriber() { Id = 1 };
            // Arrange
            var mockRepo = new Mock<IInvoicesRepository>();
            mockRepo.Setup(repo => repo.GetSubscriberAsync(Subscriber.Id))
                .ReturnsAsync(Subscriber);
            var controller = new SubscribersController(mockRepo.Object);

            // Act
            var result = await controller.Delete(Subscriber.Id);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Subscriber>(viewResult.ViewData.Model);
            Assert.Equal(Subscriber.Id, model.Id);
        }

        [Fact]
        public async Task DeleteConfirmed_Redirects()
        {
            // Arrange
            var mockRepo = new Mock<IInvoicesRepository>();
            var controller = new SubscribersController(mockRepo.Object);

            // Act
            var result = await controller.DeleteConfirmed(1);

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }

    }
}