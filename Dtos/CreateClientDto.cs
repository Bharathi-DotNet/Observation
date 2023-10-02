using Enquiry.Web.Extensions;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Enquiry.Web.Dtos
{
    public class CreateClientDto
    {
        public CreateClientDto()
        {
            AppoinmentDate = DatetimeExtension.ToDateTime(DateTimeOffset.Now);
        }
        [DisplayName("Client Name")]
        [Required]
        public string ClientName { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        [MaxLength(25)]
        public string ContactNo { get; set; }
      
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Domain { get; set; }
        [DisplayName("BDA Remarks")]
        public string BDARemarks { get; set; }
        [DisplayName("Payment Remarks")]
        public string PaymentRemarks { get; set; }

        [DisplayName("Appoinment")]
        public DateTime? AppoinmentDate { get; set; }


        public int? TechExpert { get; set; }


        [DisplayName("Enable Tech Appoinment")]
        public bool IsTechAppoinment { get; set; } = false;
    }

    public class EditClientDto
    {
        public int EnquiryId { get; set; }
        [DisplayName("Client Name")]
        [Required]
        public string ClientName { get; set; }
        [Required]
        public string Contact { get; set; }
        [Required]
        public string Domain { get; set; }
        [DisplayName("Payment Remarks")]
        public string PaymentRemarks { get; set; }
        [DisplayName("Appoinment")]
        [Required]
        public DateTime? AppoinmentDate { get; set; }
    }
}
