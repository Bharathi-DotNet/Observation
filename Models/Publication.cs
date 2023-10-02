using Enquiry.Web.Models.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enquiry.Web.Models;

public class Publication : IAuditableEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PublicationId { get; set; }
    [ForeignKey("Projects")]
    public int ProjectId { get; set; }
    public DateTime? RemainderDate { get; set; }
    public string JournelName { get; set; }
    public string Link { get; set; }
    public string UserId { get; set; }
    public string Password { get; set; }
    public string Publisher { get; set; }
    public DateTime? DateOfSubmission { get; set; }
    [ForeignKey("JournalStatus")]
    public int Status { get; set; }
    public string Reason { get; set; }
    public int CreatedBy { get; set; }
    public int UpdatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public virtual Projects Projects { get; set; }
    public virtual JournalStatus JournalStatus { get; set; }
}
