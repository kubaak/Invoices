using System.Collections.Generic;
using System.Linq;
using Xunit;
using Invoices.Data;
using System.Threading.Tasks;
using Invoices.Controllers;
using Moq;
using Invoices.ViewModels;
using Invoices.Models;
using Microsoft.AspNetCore.Mvc;

namespace InvoicesTests.Controllers
{
    public class InvoicesControllerTests
    {

        [Fact]
        public async Task Create_ReturnsViewResultWithCreateInvoiceViewModel()
        {
            var suppliers = new List<Supplier>{new Supplier(){Id = 1}};
            var subscribers = new List<Subscriber> { new Subscriber() { Id = 1 }};
            // Arrange
            var mockRepo = new Mock<IInvoicesRepository>();
            mockRepo.Setup(repo => repo.ListSuppliersAsync()).ReturnsAsync(suppliers);
            mockRepo.Setup(repo => repo.ListSubscribersAsync()).ReturnsAsync(subscribers);
            var controller = new InvoicesController(mockRepo.Object);

            // Act
            var result = await controller.Create();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<CreateInvoiceViewModel>(viewResult.ViewData.Model);
            Assert.Equal(suppliers.First().Id, int.Parse(model.Suppliers.First().Value));
            Assert.Equal(subscribers.First().Id, int.Parse(model.Subscribers.First().Value));
        }

        [Fact]
        public async Task Create_Redirects()
        {
            var invoice = new Invoice();
            // Arrange
            var mockRepo = new Mock<IInvoicesRepository>();
            mockRepo.Setup(repo => repo.AddInvoiceAsync(invoice))
                .ReturnsAsync(invoice);
            var controller = new InvoicesController(mockRepo.Object);

            // Act
            var invoiceViewModel = new CreateInvoiceViewModel();
            var result = await controller.Create(invoiceViewModel);

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName );
        }

        [Fact]
        public async Task Index_ReturnsViewResultWithListOfInvoices()
        {
            // Arrange
            var invoices = new List<Invoice> {new Invoice() {Id = 1}};
            var mockRepo = new Mock<IInvoicesRepository>();
            mockRepo.Setup(repo => repo.ListInvoicesAsync()).ReturnsAsync(invoices);
            var controller = new InvoicesController(mockRepo.Object);

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Invoice>>(viewResult.ViewData.Model);
            Assert.Single(model);
        }

        [Fact]
        public async Task Details_ReturnsViewResultWithInvoice()
        {
            // Arrange
            var invoice = new Invoice() { Id = 1 } ;
            var mockRepo = new Mock<IInvoicesRepository>();
            mockRepo.Setup(repo => repo.GetInvoiceAsync(invoice.Id)).ReturnsAsync(invoice);
            var controller = new InvoicesController(mockRepo.Object);

            // Act
            var result = await controller.Details(invoice.Id);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Invoice>(viewResult.ViewData.Model);
            Assert.Equal(invoice.Id, model.Id);
        }

        [Fact]
        public async Task Edit_ReturnsViewResultWithEditInvoiceViewModel()
        {
            // Arrange
            var invoice = new Invoice(){Id = 1};
            var suppliers = new List<Supplier> { new Supplier() { Id = 1 } };
            var subscribers = new List<Subscriber> { new Subscriber() { Id = 1 } };
            var mockRepo = new Mock<IInvoicesRepository>();
            mockRepo.Setup(repo => repo.GetInvoiceAsync(invoice.Id)).ReturnsAsync(invoice);
            mockRepo.Setup(repo => repo.ListSuppliersAsync()).ReturnsAsync(suppliers);
            mockRepo.Setup(repo => repo.ListSubscribersAsync()).ReturnsAsync(subscribers);
            var controller = new InvoicesController(mockRepo.Object);

            // Act
            var result = await controller.Edit(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<EditInvoiceViewModel>(viewResult.ViewData.Model);
            Assert.Equal(suppliers.First().Id, int.Parse(model.Suppliers.First().Value));
            Assert.Equal(subscribers.First().Id, int.Parse(model.Subscribers.First().Value));
        }

        [Fact]
        public async Task Edit_Redirects()
        {
            var invoice = new Invoice();
            // Arrange
            var mockRepo = new Mock<IInvoicesRepository>();
            mockRepo.Setup(repo => repo.AddInvoiceAsync(invoice))
                .ReturnsAsync(invoice);
            var controller = new InvoicesController(mockRepo.Object);

            // Act
            var invoiceViewModel = new EditInvoiceViewModel()
            {
                Invoice = new Invoice(){Id = 1}
            };
            var result = await controller.Edit(1,invoiceViewModel);

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }

        [Fact]
        public async Task Delete_ReturnsViewResultWithInvoice()
        {
            var invoice = new Invoice(){Id = 1};
            // Arrange
            var mockRepo = new Mock<IInvoicesRepository>();
            mockRepo.Setup(repo => repo.GetInvoiceAsync(invoice.Id))
                .ReturnsAsync(invoice);
            var controller = new InvoicesController(mockRepo.Object);

            // Act
            var result = await controller.Delete(invoice.Id);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Invoice>(viewResult.ViewData.Model);
            Assert.Equal(invoice.Id, model.Id);
        }

        [Fact]
        public async Task DeleteConfirmed_Redirects()
        {
            // Arrange
            var mockRepo = new Mock<IInvoicesRepository>();
            var controller = new InvoicesController(mockRepo.Object);

            // Act
            var result = await controller.DeleteConfirmed(1);

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }

    }
}