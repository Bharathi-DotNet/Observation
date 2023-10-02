using Enquiry.Web.Models.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enquiry.Web.Models;

public class VisitorsListDto
{
    public int VisitorId { get; set; }
    public string ClientName { get; set; }
    public string EmployeeName { get; set; }
    public bool IsClient { get; set; }
    public string NetworkId { get; set; }
    public DateTime LoginTime { get; set; }

}
