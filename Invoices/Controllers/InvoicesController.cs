using System.Linq;
using System.Threading.Tasks;
using Invoices.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Invoices.Models;
using Invoices.ViewModels;

namespace Invoices.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly IInvoicesRepository _invoicesRepository;

        public InvoicesController(IInvoicesRepository invoicesRepository)
        {
            _invoicesRepository = invoicesRepository;
        }

        // GET: Invoices
        public async Task<IActionResult> Index()
        {
            return View(await _invoicesRepository.ListInvoicesAsync());
        }

        // GET: Invoices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _invoicesRepository.GetInvoiceAsync(id.Value);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // GET: Invoices/Create
        public async Task<IActionResult> Create()
        {
            var suppliers = await _invoicesRepository.ListSuppliersAsync();
            var subscribers = await _invoicesRepository.ListSubscribersAsync();

            var invoiceViewModel = new CreateInvoiceViewModel
            {
                Suppliers = suppliers.Select(s => new SelectListItem(s.Name, s.Id.ToString())).ToList(),
                Subscribers = subscribers.Select(s => new SelectListItem(s.Name, s.Id.ToString())).ToList(),
            };
            return View(invoiceViewModel);
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateInvoiceViewModel invoiceViewModel)
        {
            //var invoice = invoiceViewModel.Invoice;
            //invoice.SupplierId = invoiceViewModel.SupplierId;
            //invoice.SubscriberId = invoiceViewModel.SubscriberId;

            if (ModelState.IsValid)
            {
                var invoice = new Invoice()
                {
                    DateOfIssue = invoiceViewModel.DateOfIssue,
                    DueDate = invoiceViewModel.DueDate,
                    InvoicePayingStatus = InvoicePayingStatus.Unpaid,
                    SubscriberId = invoiceViewModel.SubscriberId,
                    SupplierId = invoiceViewModel.SupplierId
                };

                await _invoicesRepository.AddInvoiceAsync(invoice);
                return RedirectToAction(nameof(Index));
            }

            var suppliers = await _invoicesRepository.ListSuppliersAsync();
            var subscribers = await _invoicesRepository.ListSubscribersAsync();

            //var invoiceViewModel = new InvoiceViewModel
            //{
            //    Invoice = invoice,
            //    Suppliers = suppliers.Select(s => new SelectListItem(s.Name, s.Id.ToString())).ToList(),
            //    Subscribers = subscribers.Select(s => new SelectListItem(s.Name, s.Id.ToString())).ToList(),
            //    SupplierId = invoice.SupplierId,
            //    SubscriberId = invoice.SubscriberId
            //};

            return View(invoiceViewModel);
        }

        // GET: Invoices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _invoicesRepository.GetInvoiceAsync(id.Value);

            if (invoice == null)
            {
                return NotFound();
            }

            var suppliers = await _invoicesRepository.ListSuppliersAsync();
            var subscribers = await _invoicesRepository.ListSubscribersAsync();
            var invoiceViewModel = new EditInvoiceViewModel
            {
                Invoice = invoice,
                Suppliers = suppliers.Select(s => new SelectListItem(s.Name, s.Id.ToString())).ToList(),
                Subscribers = subscribers.Select(s => new SelectListItem(s.Name, s.Id.ToString())).ToList(),
                SupplierId = invoice.SupplierId,
                SubscriberId = invoice.SubscriberId
            };
            return View(invoiceViewModel);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]// [Bind("Id,DateOfIssue,DueDate,InvoicePayingStatus")]
        public async Task<IActionResult> Edit(int id, EditInvoiceViewModel invoiceViewModel)
        {
            if (id != invoiceViewModel.Invoice.Id)
            {
                return NotFound();
            }
            var invoice = invoiceViewModel.Invoice;
            if (ModelState.IsValid)
            {
                try
                {
                    
                    invoice.SupplierId = invoiceViewModel.SupplierId;
                    invoice.SubscriberId = invoiceViewModel.SubscriberId;
                    await _invoicesRepository.UpdateInvoiceAsync(invoice);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _invoicesRepository.InvoiceExistsAsync(invoice.Id))
                    {
                        return NotFound();
                    }

                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(invoiceViewModel);
        }

        // GET: Invoices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _invoicesRepository.GetInvoiceAsync(id.Value);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _invoicesRepository.DeleteInvoiceAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
