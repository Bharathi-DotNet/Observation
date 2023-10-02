namespace Enquiry.Web.Models.Interface;

public interface IAuditableEntity
{
    int CreatedBy { get; set; }
    int UpdatedBy { get; set; }
    DateTime CreatedDate { get; set; }
    DateTime UpdatedDate { get; set; }
}
