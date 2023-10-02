using System.ComponentModel.DataAnnotations;

namespace Enquiry.Web.Models
{
    public class PaymentMode
    {
        [Key]
        public int PaymentId { get; set; }
        public string PaymentType { get; set; }
        public bool IsActive { get; set; }
    }
}
