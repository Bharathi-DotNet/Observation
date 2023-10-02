using Enquiry.Web.Models.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enquiry.Web.Models;

public class Employees : IAuditableEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int EmpId { get; set; }
    public string IdentityNo { get; set; }
    public string EmployeeName { get; set; }
    public string ContactNo { get; set; }
    public string Password { get; set; }
    public string EmailId { get; set; }
    public string Address { get; set; }
    public DateTime DOB { get; set; }
    public DateTime DOJ { get; set; }
    [ForeignKey("Department")]
    public int DeptId { get; set; }
    [ForeignKey("Roles")]
    public int RoleId { get; set; }
    public int CreatedBy { get; set; }
    public int UpdatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public bool IsActive { get; set; }
    public virtual Roles Roles { get; set; }
    public virtual Department Department { get; set; }
}
