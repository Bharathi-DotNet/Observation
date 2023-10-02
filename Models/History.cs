using Enquiry.Web.Models.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enquiry.Web.Models
{
    public class History : IAuditableEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HistoryId { get; set; }
        [ForeignKey("Employees")]
        public int EmpId { get; set; }
        [ForeignKey("Clients")]
        public int EnquiryId { get; set; }
        public string Comments { get; set; }
        public bool IsComment { get; set; } = false;
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual Employees Employees { get; set; }
        public virtual Clients Clients { get; set; }
    }
}
