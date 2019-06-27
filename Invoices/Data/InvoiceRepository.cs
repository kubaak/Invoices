using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeGuard.dotNetCore;
using Invoices.Models;
using Microsoft.EntityFrameworkCore;

namespace Invoices.Data
{
    public class InvoicesRepository : IInvoicesRepository
    {
        private readonly InvoiceDbContext _invoiceDbContext;

        public InvoicesRepository(InvoiceDbContext invoiceDbContext)
        {
            _invoiceDbContext = invoiceDbContext;
        }
        //Invoice
        public async Task<Invoice> GetInvoiceAsync(int id)
        {
            return await _invoiceDbContext.Invoices.AsNoTracking()
                .Include(i => i.Items)
                .FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<IEnumerable<Invoice>> ListInvoicesAsync()
        {
            return await _invoiceDbContext.Invoices.AsNoTracking()
                .Include(i => i.Supplier)
                .Include(i=>i.Subscriber)
                .ToListAsync();
        }
        public async Task<Invoice> AddInvoiceAsync(Invoice invoice)
        {
            var entity = new Invoice
            {
                DateOfIssue = invoice.DateOfIssue,
                DueDate = invoice.DueDate,
                SubscriberId = invoice.SubscriberId,
                SupplierId = invoice.SupplierId,
                Total = invoice.Total
            };
            await _invoiceDbContext.Invoices.AddAsync(entity);

            await _invoiceDbContext.SaveChangesAsync();

            return entity;
        }
        public async Task<Invoice> UpdateInvoiceAsync(Invoice invoice)
        {
            var entity = await _invoiceDbContext.Invoices.FindAsync(invoice.Id);
            if (entity == null)
                return null;
            entity.DateOfIssue = invoice.DateOfIssue;
            entity.DueDate = invoice.DueDate;
            entity.SubscriberId = invoice.SubscriberId;
            entity.SupplierId = invoice.SupplierId;
            entity.InvoicePayingStatus = invoice.InvoicePayingStatus;
            await _invoiceDbContext.SaveChangesAsync();

            return entity;
        }
        public async Task DeleteInvoiceAsync(int id)
        {
            var entity = await _invoiceDbContext.Invoices.FindAsync(id);
            if (entity != null)
            {
                _invoiceDbContext.Invoices.Remove(entity);
                await _invoiceDbContext.SaveChangesAsync();
            }
        }

        public async Task<bool> InvoiceExistsAsync(int id)
        {
            var exists = await _invoiceDbContext.Invoices.AnyAsync(i => i.Id == id);
            return exists;
        }
        //Api
        public async Task<IEnumerable<Invoice>> ListUnpaidInvoicesAsync()
        {
            return await _invoiceDbContext.Invoices.AsNoTracking()
                .Where(i => i.InvoicePayingStatus == InvoicePayingStatus.Unpaid).ToListAsync();
        }
        public async Task<Invoice> PayInvoiceAsync(int id)
        {
            var entity = await _invoiceDbContext.Invoices.FindAsync(id);
            if (entity == null)
                return null;
            entity.InvoicePayingStatus = InvoicePayingStatus.Paid;
            await _invoiceDbContext.SaveChangesAsync();

            return  entity;
        }
        //Suplier
        public async Task<Supplier> GetSupplierAsync(int id)
        {
            var entity = await _invoiceDbContext.Suppliers.AsNoTracking().FirstOrDefaultAsync(s => s.Id==id);
            return entity;
        }
        public async Task<IEnumerable<Supplier>> ListSuppliersAsync()
        {
            return await _invoiceDbContext.Suppliers.AsNoTracking().ToListAsync();
        }
        public async Task<Supplier> AddSupplierAsync(Supplier supplier)
        {
            var entity = new Supplier
            {
                Address = supplier.Address,
                Name = supplier.Name,
                BankAccount = supplier.BankAccount,
                Dic = supplier.Dic,
                Ic = supplier.Ic,
                Email = supplier.Email,
                Phone = supplier.Phone
            };
            await _invoiceDbContext.Suppliers.AddAsync(entity);

            await _invoiceDbContext.SaveChangesAsync();

            return entity;
        }
        public async Task UpdateSupplierAsync(Supplier supplier)
        {
            var entity =  await _invoiceDbContext.Suppliers.FindAsync(supplier.Id);

            if (entity != null)
            {
                entity.Name = supplier.Name;
                entity.Ic = supplier.Ic;
                entity.Dic = supplier.Dic;
                entity.Address = supplier.Address;
                entity.BankAccount = supplier.BankAccount;
                entity.Email = supplier.Email;
                entity.Phone = supplier.Phone;
           
                _invoiceDbContext.Suppliers.Update(entity);
                await _invoiceDbContext.SaveChangesAsync();
            }
        }
        public async Task DeleteSupplierAsync(int id)
        {
            var entity = await _invoiceDbContext.Suppliers.FindAsync(id);
            if (entity != null)
            {
                _invoiceDbContext.Suppliers.Remove(entity);
                await _invoiceDbContext.SaveChangesAsync();
            }
        }
        public async Task<bool> SupplierExistsAsync(int id)
        {
            var exists = await _invoiceDbContext.Suppliers.AnyAsync(i => i.Id == id);
            return exists;
        }
        //Subscriber
        public async Task<Subscriber> GetSubscriberAsync(int id)
        {
            var entity = await _invoiceDbContext.Subscribers.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
            return entity;
        }
        public async Task<IEnumerable<Subscriber>> ListSubscribersAsync()
        {
            return await _invoiceDbContext.Subscribers.AsNoTracking().ToListAsync();
        }
        public async Task<Subscriber> AddSubscriberAsync(Subscriber subscriber)
        {
            var entity = new Subscriber
            {
                Address = subscriber.Address,
                Name = subscriber.Name,
                Dic = subscriber.Dic,
                Ic = subscriber.Ic,
                Email = subscriber.Email,
                Phone = subscriber.Phone
            };
            await _invoiceDbContext.Subscribers.AddAsync(entity);

            await _invoiceDbContext.SaveChangesAsync();

            return entity;
        }
        public async Task UpdateSubscriberAsync(Subscriber subscriber)
        {
            var entity = await _invoiceDbContext.Subscribers.FindAsync(subscriber.Id);

            if (entity != null)
            {
                entity.Name = subscriber.Name;
                entity.Ic = subscriber.Ic;
                entity.Dic = subscriber.Dic;
                entity.Address = subscriber.Address;
                entity.Email = subscriber.Email;
                entity.Phone = subscriber.Phone;

                _invoiceDbContext.Subscribers.Update(entity);
                await _invoiceDbContext.SaveChangesAsync();
            }
        }
        public async Task DeleteSubscriberAsync(int id)
        {
            var entity = await _invoiceDbContext.Subscribers.FindAsync(id);
            if (entity != null)
            {
                _invoiceDbContext.Subscribers.Remove(entity);
                await _invoiceDbContext.SaveChangesAsync();
            }
        }
        public async Task<bool> SubscriberExistsAsync(int id)
        {
            var exists = await _invoiceDbContext.Subscribers.AnyAsync(i => i.Id == id);
            return exists;
        }
        //Item
        public async Task<Item> GetItemAsync(int invoiceId)
        {
            var entity = await _invoiceDbContext.Items.AsNoTracking().FirstOrDefaultAsync(s => s.Id == invoiceId);
            return entity;
        }
        public async Task<IEnumerable<Item>> ListItemsAsync(int invoiceId)
        {
            return await _invoiceDbContext.Items.AsNoTracking()
                .Where(i => i.Invoice.Id == invoiceId).ToListAsync();
        }
        public async Task<Item> UpdateItemAsync(Item item)
        {
            var entity = await _invoiceDbContext.Items.FindAsync(item.Id);
            if (entity != null)
            {
                entity.Description = item.Description;
                entity.Quantity = item.Quantity;
                entity.UnitPrice = item.UnitPrice;

                _invoiceDbContext.Items.Update(entity);
                await _invoiceDbContext.SaveChangesAsync();
                var invoiceEntity = await _invoiceDbContext.Invoices.FindAsync(entity.InvoiceId);
                if (invoiceEntity != null)
                {
                    var sum = await _invoiceDbContext.Items.AsNoTracking().Where(i => i.InvoiceId == entity.InvoiceId)
                        .Select(i => new { Amount = i.Quantity * i.UnitPrice }).SumAsync(a => a.Amount);

                    invoiceEntity.Total = sum;
                    _invoiceDbContext.Invoices.Update(invoiceEntity);
                    await _invoiceDbContext.SaveChangesAsync();
                }              
            }

            return entity;
        }

        public async Task<List<Item>> GetNotExistingItems(List<Item> items)
        {
            var foundList = await _invoiceDbContext.Items
                .Where(i => items.Contains(i))
                .Select(i=> i.Id)
                .ToListAsync();
            var notFoundList = items.FindAll(i => !foundList.Contains(i.Id));
            return notFoundList;
        }

        public async Task UpdateItemsOfSingleInvoiceAsync(List<Item> items)
        {
            var distinctCount = items.Select(i => i.InvoiceId).Distinct().Count();
            Guard.That(distinctCount == 1);

            foreach (var item in items)
            {
                var entity = await _invoiceDbContext.Items.FindAsync(item.Id);

                entity.Description = item.Description;
                entity.Quantity = item.Quantity;
                entity.UnitPrice = item.UnitPrice;

                _invoiceDbContext.Items.Update(entity);
                await _invoiceDbContext.SaveChangesAsync();
            }

            var invoiceId = items.First().InvoiceId;
            var invoiceEntity = await _invoiceDbContext.Invoices.FindAsync(invoiceId);
            if (invoiceEntity == null)
                return;

            var sum = await _invoiceDbContext.Items.AsNoTracking().Where(i => i.InvoiceId == invoiceId)
                .Select(i => new { Amount = i.Quantity * i.UnitPrice }).SumAsync(a => a.Amount);

            invoiceEntity.Total = sum;
            _invoiceDbContext.Invoices.Update(invoiceEntity);
            await _invoiceDbContext.SaveChangesAsync();
        }

        public async Task<Item> AddItemAsync(Item item)
        {
            var entity = new Item
            {
                InvoiceId = item.InvoiceId,
                Description = item.Description,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice
            };
            await _invoiceDbContext.Items.AddAsync(entity);

            var invoiceEntity = await _invoiceDbContext.Invoices.FindAsync(item.InvoiceId);
            if (invoiceEntity != null)
            {
                var sum = await _invoiceDbContext.Items.AsNoTracking().Where(i => i.InvoiceId == item.InvoiceId)
                    .Select(i => new { Amount = i.Quantity * i.UnitPrice }).SumAsync(a => a.Amount);

                invoiceEntity.Total = sum + (item.Quantity * item.UnitPrice);
            }
            await _invoiceDbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<Item> DeleteItemAsync(int id)
        {
            var entity = await _invoiceDbContext.Items.FindAsync(id);
            if (entity != null)
            {
                _invoiceDbContext.Items.Remove(entity);
                var invoiceEntity = await _invoiceDbContext.Invoices.FindAsync(entity.InvoiceId);
                if (invoiceEntity != null)
                {
                    invoiceEntity.Total = invoiceEntity.Total - entity.Quantity * entity.UnitPrice;
                }
                _invoiceDbContext.SaveChanges();
            }

            return entity;
        }
        public async Task<bool> ItemExistsAsync(int id)
        {
            var exists = await _invoiceDbContext.Items.AnyAsync(i => i.Id == id);
            return exists;
        }
    }
}