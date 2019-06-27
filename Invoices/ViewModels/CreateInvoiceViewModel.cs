using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Invoices.ViewModels
{
    public class CreateInvoiceViewModel
    {
        public DateTime DateOfIssue { get; set; }
        public DateTime DueDate { get; set; }
        public List<SelectListItem> Suppliers { get; set; }
        public List<SelectListItem> Subscribers { get; set; }
        public int SupplierId { get; set; }
        public int SubscriberId { get; set; }
    }
}