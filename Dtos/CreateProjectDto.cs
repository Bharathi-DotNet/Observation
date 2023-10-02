using System.ComponentModel.DataAnnotations;

namespace Enquiry.Web.Dtos
{
    public class CreateProjectDto
    {
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime RegistrationDate { get; set; }
        [Required]
        public string FeeConfirmation { get; set; }
        [Required]
        public string ProjectName { get; set; }
        [Required]
        public int TotalPayment { get; set; }

        public CreatePhaseDto CreatePhaseDto { get; set; }
        public CreatePayment CreatePayment { get; set; }
        public ClientDetailsDto ClientDetailsDto { get; set; }

    }
    public class CreatePhaseDto
    {
        [Required]
        public string PhaseName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? DeadLine { get; set; }
        public bool IsPublication { get; set; } = false;

    }

    public class CreatePayment
    {
        [Required]
        public int? PartPayment { get; set; }
        [Required]
        public int PaymentType { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? PaymentDate { get; set; }

    }

    public class ClientDetailsDto
    {
        public int EnquiryId { get; set; }
        public string ClientName { get; set; }
        public string Domain { get; set; }
        public string Contact { get; set; }
        public string EnquiryRef { get; set; }
        public int? TechId { get; set; }
    }

    public class EditPhaseDto
    {
        public int PhaseId { get; set; }
        [Required]
        public string PhaseName { get; set; }
        [Required]
        public DateTime? DeadLine { get; set; }
        public int? Programmer { get; set; }
        public DateTime? DemoDate { get; set; } = null;
        public DateTime? DemoDate1 { get; set; }
        public DateTime? DemoDate2 { get; set; }
        public DateTime? DemoDate3 { get; set; }
        public int? TechExpert { get; set; }
        public int? TechExpert1 { get; set; }
        public int? TechExpert2 { get; set; }
        public string Remarks { get; set; }
        public string Remarks1 { get; set; }
        public string Remarks2 { get; set; }
        public string Remarks3 { get; set; }
        public bool IsPublication { get; set; } 
    }
    public class EditPaymentDto
    {
        public int PaymentId { get; set; }
        [Required]
        public int? PartPayment { get; set; }
        [Required]
        public int PaymentType { get; set; }

    }

    public class EditPhaseOnHoldDto
    {
        [Required]
        public string Comment { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? NextAppoinment { get; set; } = DateTime.Now;
               public bool IsCompleted { get; set; } = false;
        public bool IsCompletedphase { get; set; } = false;
        public bool IsCompletedphaseqc { get; set; } = false;

        public bool IsPublication { get; set; } = false;

    }

}
