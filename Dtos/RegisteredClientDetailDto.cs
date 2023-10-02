using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enquiry.Web.Dtos;

public class RegisteredClientDetailDto
{
    public int EnquiryId { get; set; }
    public int ProjectId { get; set; }
    public string ClientName { get; set; }
    public string EnquiryRef { get; set; }
    public string ProjectRef { get; set; }
    public string ProjectName { get; set; }
    public string Contact { get; set; }
    public string Domain { get; set; }
    public string BDAName { get; set; }
    public string TechName { get; set; }
    public bool BackToEnquiry { get; set; }

    public IList<ProjectDetailDto> ProjectDetailDto { get; set; }
}

public class ProjectDetailDto
{
    public int ProjectId { get; set; }
    public string ProjectName { get; set; }
    public int EnquiryId { get; set; }
    public string ProjectRef { get; set; }
    public int TotalPayment { get; set; }
    public int PartPayment { get; set; }
    public int Balance { get; set; }
    public string FeeConfirmation { get; set; }
    public DateTime RegistrationDate { get; set; }
    public IList<PaymentDetailDto> PaymentDetailDto { get; set; }
    public List<PhaseHistoryDto> PhaseHistoryDto { get; set; } = new List<PhaseHistoryDto>();
    public IList<PhaseDetailDto> PhaseDetailDto { get; set; }
    public IList<RegisteredClientDetailDto> RegisteredClientDetailDto { get; set; }
}


public class PaymentDetailDto
{
    public int PaymentId { get; set; }
    public int ProjectId { get; set; }
    public int? PartPayment { get; set; }
    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime? PaymentDate { get; set; }
}

public class PhaseDetailDto
{
    
    public int PhaseId { get; set; }
    public string PhaseName { get; set; }
    public string Programmer { get; set; }
    public string TechExpert { get; set; }
    public string TechExpert1 { get; set; }
    public string TechExpert2 { get; set; }
    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime? DeadLine { get; set; }
    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime? DemoDate { get; set; }
    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime? DemoDate1 { get; set; }
    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime? DemoDate2 { get; set; }
    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime? DemoDate3 { get; set; }
    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
    public string Remarks { get; set; }
    public string Remarks1 { get; set; }
    public string Remarks2 { get; set; }
    public string Remarks3 { get; set; }
    public int Progress { get; set; }
    public int Progress1 { get; set; }
    public int Progress2 { get; set; }
    public int Progress3 { get; set; }
    public string Status { get; set; }
    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime? NextAppoinment { get; set; }
    public string Comment { get; set; }
}
