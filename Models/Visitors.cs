using Enquiry.Web.Models.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enquiry.Web.Models;

public class Visitors
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int VisitorId { get; set; }
    [ForeignKey("Clients")]
    public int? ClientId { get; set; }
    [ForeignKey("Employees")]
    public int? EmployeeId { get; set; }
    public bool IsClient { get; set; }
    public string NetworkId { get; set; }
    public DateTime LoginTime { get; set; }

    public virtual Employees Employees { get; set; }
    public virtual Clients Clients { get; set; }
}
