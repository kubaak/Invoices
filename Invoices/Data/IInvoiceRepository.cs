using System.Collections.Generic;
using System.Threading.Tasks;
using Invoices.Models;

namespace Invoices.Data
{
    public interface IInvoicesRepository
    {
        //Invoice
        Task<Invoice> GetInvoiceAsync(int id);
        Task<IEnumerable<Invoice>> ListInvoicesAsync();
        Task<Invoice> AddInvoiceAsync(Invoice invoice);
        Task<Invoice> UpdateInvoiceAsync(Invoice invoice);
        Task DeleteInvoiceAsync(int id);
        Task<bool> InvoiceExistsAsync(int id);

        //API
        Task<IEnumerable<Invoice>> ListUnpaidInvoicesAsync();
        Task<Invoice> PayInvoiceAsync(int id);
        //Supplier
        Task<Supplier> GetSupplierAsync(int id);
        Task<IEnumerable<Supplier>> ListSuppliersAsync();
        Task<Supplier> AddSupplierAsync(Supplier supplier);
        Task UpdateSupplierAsync(Supplier supplier);
        Task DeleteSupplierAsync(int id);
        Task<bool> SupplierExistsAsync(int id);
        //Subscriber
        Task<Subscriber> GetSubscriberAsync(int id);
        Task<IEnumerable<Subscriber>> ListSubscribersAsync();
        Task<Subscriber> AddSubscriberAsync(Subscriber subscriber);
        Task UpdateSubscriberAsync(Subscriber subscriber);
        Task DeleteSubscriberAsync(int id);
        Task<bool> SubscriberExistsAsync(int id);
        //Item
        Task<Item> GetItemAsync(int invoiceId);
        Task<IEnumerable<Item>> ListItemsAsync(int invoiceId);
        Task<Item> UpdateItemAsync(Item item);
        Task<List<Item>> GetNotExistingItems(List<Item> items);
        Task UpdateItemsOfSingleInvoiceAsync(List<Item> items);
        Task<Item> AddItemAsync(Item item);
        Task<Item> DeleteItemAsync(int id);
        Task<bool> ItemExistsAsync(int id);
    }
}