using System.Collections.Generic;
using System.Threading.Tasks;
using Invoices.ApiControllers;
using Invoices.Data;
using Invoices.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace InvoicesTests.ApiControllers
{
    public class InvoicesControllerTests
    {
        [Fact]
        public async Task ListUnpaidInvoices_ReturnsOkAndAllInvoices()
        {
            var invoices = new List<Invoice>(){new Invoice(){Id = 1}};
            // Arrange
            var mockRepo = new Mock<IInvoicesRepository>();
            mockRepo.Setup(repo => repo.ListUnpaidInvoicesAsync()).ReturnsAsync(invoices);
            var controller = new InvoicesController(mockRepo.Object);

            // Act
            var result = await controller.ListUnpaidInvoices();

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var returnedInvoices = Assert.IsType<List<Invoice>>(okObjectResult.Value);
            Assert.Equal(invoices.Count, returnedInvoices.Count);
        }
        [Fact]
        public async Task ListUnpaidInvoices_ReturnsNotFound()
        {
            var invoices = new List<Invoice>();
            // Arrange
            var mockRepo = new Mock<IInvoicesRepository>();
            mockRepo.Setup(repo => repo.ListUnpaidInvoicesAsync()).ReturnsAsync(invoices);
            var controller = new InvoicesController(mockRepo.Object);

            // Act
            var result = await controller.ListUnpaidInvoices();

            // Assert
            var okObjectResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task EditInvoice_ReturnsOk()
        {
            var invoice = new Invoice() { Id = 1 };
            // Arrange
            var mockRepo = new Mock<IInvoicesRepository>();
            mockRepo.Setup(repo => repo.UpdateInvoiceAsync(invoice)).ReturnsAsync(invoice);
            var controller = new InvoicesController(mockRepo.Object);

            // Act
            var result = await controller.EditInvoice(invoice);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var returnedInvoice = Assert.IsType<Invoice>(okObjectResult.Value);
            Assert.Equal(invoice.Id, returnedInvoice.Id);
        }

        [Fact]
        public async Task EditInvoice_ReturnsNotFound()
        {
            var invoice = new Invoice() { Id = 1 };
            // Arrange
            var mockRepo = new Mock<IInvoicesRepository>();
            var controller = new InvoicesController(mockRepo.Object);

            // Act
            var result = await controller.EditInvoice(invoice);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task PayInvoice_ReturnsOk()
        {
            var invoice = new Invoice() { Id = 1 };
            // Arrange
            var mockRepo = new Mock<IInvoicesRepository>();
            mockRepo.Setup(repo => repo.PayInvoiceAsync(invoice.Id)).ReturnsAsync(invoice);
            var controller = new InvoicesController(mockRepo.Object);

            // Act
            var result = await controller.PayInvoice(invoice.Id);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var returnedInvoice = Assert.IsType<Invoice>(okObjectResult.Value);
            Assert.Equal(invoice.Id, returnedInvoice.Id);
        }

        [Fact]
        public async Task PayInvoice_ReturnsNotFound()
        {
            var invoice = new Invoice() { Id = 1 };
            // Arrange
            var mockRepo = new Mock<IInvoicesRepository>();
            var controller = new InvoicesController(mockRepo.Object);

            // Act
            var result = await controller.PayInvoice(invoice.Id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task EditSingleInvoiceItems_ReturnsOk()
        {
            var invoices = new List<Item>
            {
                new Item() {Id = 1, InvoiceId = 1},
                new Item() {Id = 2, InvoiceId = 1}
            };
            // Arrange
            var mockRepo = new Mock<IInvoicesRepository>();
            mockRepo.Setup(repo => repo.GetNotExistingItems(invoices)).ReturnsAsync(new List<Item>());
            var controller = new InvoicesController(mockRepo.Object);

            // Act
            var result = await controller.EditSingleInvoiceItems(invoices);

            // Assert
            var okObjectResult = Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task EditSingleInvoiceItems_ReturnsNotFoundResult()
        {
            var invoices = new List<Item>
            {
                new Item() {Id = 1, InvoiceId = 1},
                new Item() {Id = 2, InvoiceId = 1}
            };
            // Arrange
            var mockRepo = new Mock<IInvoicesRepository>();
            mockRepo.Setup(repo => repo.GetNotExistingItems(invoices)).ReturnsAsync(invoices);
            var controller = new InvoicesController(mockRepo.Object);

            // Act
            var result = await controller.EditSingleInvoiceItems(invoices);

            // Assert
            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(result);
            var returnedItems = Assert.IsType<List<Item>>(notFoundObjectResult.Value);
            Assert.Equal(invoices.Count, returnedItems.Count);
        }

        [Fact]
        public async Task EditSingleInvoiceItems_ReturnsBadRequestResult()
        {
            var invoices = new List<Item>
            {
                new Item() {Id = 1, InvoiceId = 1},
                new Item() {Id = 2, InvoiceId = 2}
            };
            // Arrange
            var mockRepo = new Mock<IInvoicesRepository>();
            var controller = new InvoicesController(mockRepo.Object);

            // Act
            var result = await controller.EditSingleInvoiceItems(invoices);

            // Assert
            var notFoundObjectResult = Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}