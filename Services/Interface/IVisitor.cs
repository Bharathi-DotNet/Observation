using Enquiry.Web.Dtos;
using Enquiry.Web.Models;

namespace Enquiry.Web.Services.Interface
{
    public interface IVisitor
    {
        Task<(bool Succeeded, string[] Error, IList<VisitorsListDto> Data)> GetVisitorAsync();
    }
}
