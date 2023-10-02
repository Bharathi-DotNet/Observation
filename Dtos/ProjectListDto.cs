using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace Enquiry.Web.Dtos;

public class ProjectListDto
{
    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime? NextAppoinment { get; set; }
    public string status { get; set; }
    public int EnquiryId { get; set; }
    public int PhaseId { get; set; }
    public int ProjectId { get; set; }
    public string ClientName { get; set; }
    public string ProjectName { get; set; }
    public string PhaseName { get; set; }
    public string EnquiryRef { get; set; }
    public string Contact { get; set; }
    public string Domain { get; set; }
    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime? DeadLine { get; set; }
    public string Status { get; set; }
    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime? DemoDate { get; set; }
    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime? DemoDate3 { get; set; }
    public string AssignedTech { get; set; }
    public string AssignedTech1 { get; set; }
    public string AssignedTech2 { get; set; }
    public string AssignedTech3 { get; set; }

    public int? TechExpert { get; set; }
    public int? TechExpert1 { get; set; }
    public int? TechExpert2 { get; set; }
    public int? Programmer { get; set; }
    public string Comment { get; set; }
    

}

