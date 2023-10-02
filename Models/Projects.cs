using Enquiry.Web.Models.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enquiry.Web.Models;

public class Projects : IAuditableEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProjectId { get; set; }
    public string ProjectName { get; set; }
    public string ProjectRef { get; set; }
    [ForeignKey("Clients")]
    public int EnquiryId { get; set; }
    public string FeeConfirmation { get; set; }
    public int TotalPayment { get; set; }
    public DateTime RegistrationDate { get; set; }
    public int CreatedBy { get; set; }
    public int UpdatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public bool IsActive { get; set; }
    public virtual Clients Clients { get; set; }
    public virtual ICollection<Phase> Phase { get; set; }
    public virtual ICollection<Payments> Payments { get; set; }
    public virtual ICollection<ProjectHistory> ProjectHistory { get; set; }
    public virtual ICollection<Publication> Publication { get; set; }
}
