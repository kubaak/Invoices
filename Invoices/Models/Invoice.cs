using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Invoices.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfIssue { get; set; }
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public int SubscriberId { get; set; }
        public Subscriber Subscriber { get; set; }
        public List<Item> Items { get; set; }
        [DataType(DataType.Currency)]
        public decimal Total { get; set; }
        public InvoicePayingStatus InvoicePayingStatus { get; set; }

    }

    public enum InvoicePayingStatus
    {
        Unpaid,
        Paid
    }
}