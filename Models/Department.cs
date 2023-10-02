using System.ComponentModel.DataAnnotations;

namespace Enquiry.Web.Models;

public class Department
{
    [Key]
    public int DeptId { get; set; }
    public string DeptName { get; set; }
    public bool IsActive { get; set; }
    public virtual ICollection<Employees> Employees { get; set; }
}
