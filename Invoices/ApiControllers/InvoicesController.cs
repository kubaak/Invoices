using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Invoices.Data;
using Invoices.Models;
using Microsoft.AspNetCore.Mvc;

namespace Invoices.ApiControllers
{
    [Route("api/[controller]/[action]/{id?}")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly IInvoicesRepository _invoicesRepository;

        public InvoicesController(IInvoicesRepository invoicesRepository)
        {
            _invoicesRepository = invoicesRepository;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> ListUnpaidInvoices()
        {
            var invoices = await _invoicesRepository.ListUnpaidInvoicesAsync();

            if (!invoices.Any())
                return NotFound();

            return Ok(invoices);
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> ListInvoiceItems(int id)
        {
            var items = await _invoicesRepository.ListItemsAsync(id);

            if (!items.Any())
                return NotFound();

            return Ok(items);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PayInvoice(int id)
        {
            var invoices = await _invoicesRepository.PayInvoiceAsync(id);

            if (invoices == null)
                return NotFound();

            return Ok(invoices);
        }

        [HttpPatch]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> EditInvoice([FromBody]Invoice invoice)
        {
            var invoices = await _invoicesRepository.UpdateInvoiceAsync(invoice);

            if (invoices == null)
                return NotFound();

            return Ok(invoices);
        }

        [HttpPatch]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> EditSingleInvoiceItems([FromBody] IEnumerable<Item> items)
        {
            var itemsList = items.ToList();
            var distinctCount = itemsList.Select(i=>i.InvoiceId).Distinct().Count();
            if (distinctCount != 1)
                return BadRequest("Items of multiple invoices received.");

            var notExistingItems = await _invoicesRepository.GetNotExistingItems(itemsList);
            if (notExistingItems.Any())
                return NotFound(notExistingItems);

            await _invoicesRepository.UpdateItemsOfSingleInvoiceAsync(itemsList);
            return Ok();
        }
    }
}