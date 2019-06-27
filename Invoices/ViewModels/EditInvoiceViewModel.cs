using System.Collections.Generic;
using Invoices.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Invoices.ViewModels
{
    public class EditInvoiceViewModel
    {
        public Invoice Invoice { get; set; }
        public List<SelectListItem> Suppliers { get; set; }
        public List<SelectListItem> Subscribers { get; set; }
        public int SupplierId { get; set; }
        public int SubscriberId { get; set; }
    }
}