using System;
using Microsoft.EntityFrameworkCore;

namespace Invoices.Models
{
    public class InvoiceDbContext : DbContext
    {
        public InvoiceDbContext(DbContextOptions<InvoiceDbContext> options) : base(options)
        {
        }

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var subscriber = new Subscriber()
            {
                Id = 1,
                Name = "Test subscriber",
                Address = "Some Address"
            };

            var supplier = new Supplier()
            {
                Id = 1,
                Name = "Test supplier",
                Address = "Some Address",
                BankAccount = "1234",
                Ic = "1234"
            };

            modelBuilder.Entity<Subscriber>().HasData(subscriber);
            modelBuilder.Entity<Supplier>().HasData(supplier);

            var invoice = new
            {
                Id = 1,
                DateOfIssue = DateTime.Now,
                DueDate = DateTime.Now.AddDays(14),
                Total = 99m,
                SubscriberId = subscriber.Id,
                SupplierId = supplier.Id,
                InvoicePayingStatus = InvoicePayingStatus.Unpaid
            };

            modelBuilder.Entity<Invoice>().HasData(invoice);

            var item = new
            {
                Id = 1,
                Quantity = 1m,
                Description = "Some goods",
                UnitPrice = 99m,
                InvoiceId = invoice.Id
            };

            modelBuilder.Entity<Item>().HasData(item);

            base.OnModelCreating(modelBuilder);
        }
    }
}