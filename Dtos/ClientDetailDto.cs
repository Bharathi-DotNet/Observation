using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Enquiry.Web.Dtos;

public class ClientDetailDto
{
    public int EnquiryId { get; set; }
    public string EnquiryRef { get; set; }
    public string ClientName { get; set; }
    public string Contact { get; set; }
    public string Domain { get; set; }
    public string TechName { get; set; }
    public string BDAName { get; set; }
    public int? BDAId { get; set; }
    public int? TechId { get; set; }
    public bool IsTechAppoinment { get; set; }
    public string PaymentRemarks { get; set; }
    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime EnquiryDate { get; set; }
    
    public DateTime? AppoinmentDate { get; set; }
    public string LastComment { get; set; }
    public bool Registered { get; set; }
}
