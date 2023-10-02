using System.ComponentModel.DataAnnotations;

namespace Enquiry.Web.Models
{
    public class WorkStatus
    {
        [Key]
        public int WorkStatusId { get; set; }
        public string WorkStatusName { get; set; }
        public bool IsActive { get; set; }
    }
}
