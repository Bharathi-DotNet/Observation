using System.ComponentModel.DataAnnotations;

namespace Enquiry.Web.Models
{
    public class JournalStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive
        {
            get; set;
        }
    }
}
