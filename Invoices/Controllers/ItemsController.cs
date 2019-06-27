using System.Threading.Tasks;
using Invoices.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Invoices.Models;

namespace Invoices.Controllers
{
    public class ItemsController : Controller
    {
        private readonly IInvoicesRepository _invoicesRepository;

        public ItemsController(IInvoicesRepository invoicesRepository)
        {
            _invoicesRepository = invoicesRepository;
        }


        // GET: Items
        public async Task<IActionResult> Index(int invoiceId)
        {
            return View(await _invoicesRepository.ListItemsAsync(invoiceId));
        }

        // GET: Items/Create
        public IActionResult Create(int id)
        {
            var item = new Item{InvoiceId = id };
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Item item)
        {
            if (ModelState.IsValid)
            {
                var storedItem = await _invoicesRepository.AddItemAsync(item);

                return RedirectToAction("Details", "Invoices",new {
                    id = item.InvoiceId
                });
            }
            return View(item);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _invoicesRepository.GetItemAsync(id.Value);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Item item)
        {
            if (id != item.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                Item updatedItem;
                try
                {
                    updatedItem = await _invoicesRepository.UpdateItemAsync(item);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _invoicesRepository.ItemExistsAsync(item.Id))
                    {
                        return NotFound();
                    }

                    throw;
                }

                if (updatedItem != null)
                    return RedirectToAction("Details", "Invoices", new {id = updatedItem.InvoiceId});
                else
                    return RedirectToAction("Index", "Invoices");
            }
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _invoicesRepository.GetItemAsync(id.Value);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deletedItem =  await _invoicesRepository.DeleteItemAsync(id);
            if (deletedItem != null)
                return RedirectToAction("Details", "Invoices", new { id = deletedItem.InvoiceId });
            else
                return RedirectToAction("Index", "Invoices");
        }
    }
}
