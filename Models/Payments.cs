using Enquiry.Web.Models.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enquiry.Web.Models
{
    public class Payments : IAuditableEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaymentId { get; set; }
        [ForeignKey("Projects")]
        public int ProjectId { get; set; }
        public int? PartPayment { get; set; }
        public DateTime? PaymentDate { get; set; }
        [ForeignKey("PaymentMode")]
        public int PaymentType { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual Projects Projects { get; set; }
        public virtual PaymentMode PaymentMode { get; set; }
    }
}
