using System.ComponentModel.DataAnnotations;

namespace Invoices.Models
{
    public class Subscriber
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        public string Ic { get; set; }
        public string Dic { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}