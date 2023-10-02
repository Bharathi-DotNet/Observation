using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enquiry.Web.Models;

public class Phase
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PhaseId { get; set; }
    public string PhaseName { get; set; }
    [ForeignKey("Projects")]
    public int ProjectId { get; set; }
    [ForeignKey("Employees")]
    public int? TechExpert { get; set; }
    [ForeignKey("Employees1")]
    public int? TechExpert1 { get; set; }
    [ForeignKey("Employees2")]
    public int? TechExpert2 { get; set; }
    [ForeignKey("Employees3")]
    public int? Programmer { get; set; }
    public DateTime? DemoDate { get; set; }
    public DateTime? DemoDate1 { get; set; }
    public DateTime? DemoDate2 { get; set; }
    public DateTime? DemoDate3 { get; set; }
    public string Remarks { get; set; }
    public string Remarks1 { get; set; }
    public string Remarks2 { get; set; }
    public string Remarks3 { get; set; }
    public int Progress { get; set; }
    public int Progress1 { get; set; }
    public int Progress2 { get; set; }
    public int Progress3 { get; set; }
    public DateTime? DeadLine { get; set; }
    [ForeignKey("WorkStatus")]
    public int Status { get; set; }
    public DateTime? NextAppoinment { get; set; }
    public string Comment { get; set; }

    public virtual Projects Projects { get; set; }
    public virtual Employees Employees { get; set; }
    public virtual Employees Employees1 { get; set; }
    public virtual Employees Employees2 { get; set; }
    public virtual Employees Employees3 { get; set; }
    public virtual WorkStatus WorkStatus { get; set; }
    public virtual ICollection<PhaseHistory> PhaseHistory { get; set; }
}

