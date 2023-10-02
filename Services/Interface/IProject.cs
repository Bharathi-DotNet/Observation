using Enquiry.Web.Dtos;
using Enquiry.Web.Models;

namespace Enquiry.Web.Services.Interface
{
    public interface IProject
    {
        Task<(bool Succeeded, string[] Error, IList<ProjectListDto> Data)> GetPhaseAsync();
        Task<(bool Succeeded, string[] Error, Projects Project)> CreateProjectAsync(CreateProjectDto projectDto);
        Task<(bool Succeeded, string[] Error, Phase Phase)> CreatePhaseAsync(CreatePhaseDto phaseDto, int projectId);
        Task<(bool Succeeded, string[] Error, Payments Phase)> CreatePaymentAsync(CreatePayment payment, int projectId);
        Task<(bool Succeeded, string[] Error, Models.Publication Publication)> CreateJournalAsync(JournalDetailDto journal, int projectId);
        Task<(bool Succeeded, string[] Error, Models.Publication Publication)> UpdateJournalAsync(JournalDetailDto journal);
        Task<(bool Succeeded, string[] Error, RegisteredClientDetailDto Client)> GetRegisteredClientDetailAsync(int enquiryId);
        Task<(bool Succeeded, string[] Error, ProjectDetailDto Project)> GetProjectDetailAsync(int projectId);
        Task<(bool Succeeded, string[] Error, EditPhaseDto Phase)> GetPhaseAsync(int phaseId);
        Task<(bool Succeeded, string[] Error, Phase Phase)> UpdatePhaseAsync(EditPhaseDto phaseDto);
        Task<(bool Succeeded, string[] Error, Phase Phase)> UpdatePhaseOnHoldAsync(EditPhaseOnHoldDto phaseDto, int phaseId);
        Task<(bool Succeeded, string[] Error, EditPhaseOnHoldDto Phase)> EditProjectOnHoldFormAsync(int phaseId);
        Task<(bool Succeeded, string[] Error, Phase Phase)> UpdateProgressAsync(int progressId, int phaseId, int value);
    }
}
