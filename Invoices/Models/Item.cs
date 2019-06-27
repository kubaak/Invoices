using System.ComponentModel.DataAnnotations;

namespace Invoices.Models
{
    public class Item
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
        [Required]
        public string Description { get; set; }
        public decimal Quantity { get; set; }
        [DataType(DataType.Currency)]
        public decimal UnitPrice { get; set; }
    }
}