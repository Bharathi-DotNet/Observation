using System.ComponentModel.DataAnnotations;

namespace Enquiry.Web.Dtos
{
    public class ClientListDto
    {
        public int EnquiryId { get; set; }
        public string ClientName { get; set; }
        public string EnquiryRef { get; set; }
        public string Contact { get; set; }
        public string Domain { get; set; }
        public string TechRemarks { get; set; }
        
        public string BDARemarks { get; set; }
        public string PaymentRemarks { get; set; }
        public string TechExperts { get; set; }
        public string BDA { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? EnquiryDate { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy hh:mm tt}")]
        public DateTime? AppoinmentDate { get; set; }
        public bool IsTechAppoinment { get; set; }
        public bool Registered { get; set; }
    }
}
