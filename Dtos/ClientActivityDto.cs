using System.ComponentModel.DataAnnotations;

namespace Enquiry.Web.Dtos;

public class ClientActivityDto
{
    public int Id { get; set; }
    public string EmployeeName { get; set; }
    public string ClientName { get; set; }
    public string Comments { get; set; }
    public DateTime CreatedDate { get; set; }
}
