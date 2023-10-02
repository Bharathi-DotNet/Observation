using System.ComponentModel.DataAnnotations;

namespace Enquiry.Web.Models;

public class Roles
{
    [Key]
    public int RoleId { get; set; }
    public string RoleName { get; set; }
    public bool IsActive { get; set; }
    public virtual ICollection<Employees> Employees { get; set; }
}
