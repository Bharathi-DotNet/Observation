using Enquiry.Web.Dtos;
using Enquiry.Web.Models;

namespace Enquiry.Web.Services.Interface
{
    public interface IClient
    {
        Task<(bool Succeeded, string[] Error, IList<ClientListDto> Data)> GetClientsAsync(char chkValue);
        Task<(bool Succeeded, string[] Error)> CreateClientAsync(CreateClientDto dto);
        Task<(bool Succeeded, string[] Error, ClientDetailDto Data)> GetClientDetailAsync(int enquiryId);
        Task<(bool Succeeded, string[] Error, EditClientDto data)> EditClientFormAsync(int enquiryId);
        Task<(bool Succeeded, string[] Error)> EditClientAsync(EditClientDto dto);
        Task<(bool Succeeded, string[] Error)> DeleteClientAsync(int enquiryId);
        Task<(bool Succeeded, string[] Error)> UpdateBDAForClientAsync(int bdaId, int enquiryId);
        Task<(bool Succeeded, string[] Error)> UpdateTechExpertForClientAsync(int techId, int enquiryId);
        Task<(bool Succeeded, string[] Error, IList<ClientActivityDto> Data)> GetClientActivityAsync(int enquiryId, bool isComment);
        Task<(bool Succeeded, string[] Error)> UpdatePaymentRemarksAsync(int enquiryId, string remarks, bool isRegsitered);
        Task AddEnquiryActivity(int enquiryId, int empId, string comment, bool isComment);
        Task<(bool Succeeded, string[] Error)> UpdateAppoinmentDateAsync(DateTime appoinmentDate, int enquiryId);
        Task<(bool Succeeded, string[] Error)> UpdateAsTechAppoinmentAsync(bool ChkValue, int enquiryId);
        Task<(bool Succeeded, string[] Error, Clients Clients)> IsRegisteredAsync(int enquiryId, bool isRegsitered);
        Task<(bool Succeeded, string[] Error)> UpdateBackToEnquiryAsync(int enquiryId);
    }
}
