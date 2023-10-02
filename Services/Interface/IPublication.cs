using Enquiry.Web.Dtos;

namespace Enquiry.Web.Services.Interface;

public interface IPublication
{
    Task<(bool Succeeded, string[] Error, IList<PublicationListDto> Publication)> GetPublicationListAsync(char chkValue);
    Task<(bool Succeeded, string[] Error, IList<JournalDetailDto> Data)> GetJournalListAsync(int projectId);
    Task<(bool Succeeded, string[] Error, JournalDetailDto Data)> GetJournalAsync(int publicationid);
}
