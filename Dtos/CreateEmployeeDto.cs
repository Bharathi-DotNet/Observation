using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Enquiry.Web.Dtos;

public class CreateEmployeeDto
{
    [DisplayName("Employee Name")]
    [Required]
    public string EmployeeName { get; set; }
    [DisplayName("Contact")]
    [Required]
    public string ContactNo { get; set; }
    [DisplayName("Email")]
    [Required]
    public string EmailId { get; set; }
    [Required]
    public string Address { get; set; }
    [Required]
    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
    public DateTime DOB { get; set; }
    [Required]
    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
    public DateTime DOJ { get; set; }
    [DisplayName("Department")]
    [Required]
    public int DeptId { get; set; }
    [DisplayName("Role")]
    [Required]
    public int RoleId { get; set; }
}
