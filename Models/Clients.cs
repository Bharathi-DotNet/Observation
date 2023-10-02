using Enquiry.Web.Models.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enquiry.Web.Models;

public class Clients : IAuditableEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
   
    public int EnquiryId { get; set; }
    public string ClientName { get; set; }
    public string EnquiryRef { get; set; }
    public string Contact { get; set; }
    public string Domain { get; set; }
    [ForeignKey("TechEmployee")]
    public int? TechExpert { get; set; }
    [ForeignKey("BDAEmployee")]
    public int? BDA { get; set; }
    public string PaymentRemarks { get; set; }
    public DateTime EnquiryDate { get; set; }
    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime? AppoinmentDate { get; set; }
    public bool IsTechAppoinment { get; set; } = false;
    public int CreatedBy { get; set; }
    public int UpdatedBy { get; set; }
    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime CreatedDate { get; set; }
    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime UpdatedDate { get; set; }
    public bool IsActive { get; set; }
    public bool Registered { get; set; } = false;
    public bool BackToEnquiry { get; set; } = false;
    public int PreviousEnquiryId { get; set; }

    public virtual Employees BDAEmployee { get; set; }
    public virtual Employees TechEmployee { get; set; }
    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
    public virtual ICollection<History> History { get; set; }
    public virtual ICollection<Projects> Projects { get; set; }
}
