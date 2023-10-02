using Enquiry.Web.Dtos;
using Enquiry.Web.Models;

namespace Enquiry.Web.Services.Interface;

public interface IEmployee
{
    Task<(bool Succeeded, string[] Error, IList<Roles> Data)> GetRolesAsync();
    Task<(bool Succeeded, string[] Error, IList<Department> Data)> GetDepartmentAsync();
    Task<(bool Succeeded, string[] Error, IList<EmployeeListDto> Data)> GetEmployeeAsync();
    Task<(bool Succeeded, string[] Error)> CreateEmployeeAsync(CreateEmployeeDto dto);
    Task<(bool Succeeded, string[] Error, IList<Employees> Data)> GetBDAAsync();
    Task<(bool Succeeded, string[] Error, IList<Employees> Data)> GetTechExpertAsync();
    Task<(bool Succeeded, string[] Error, IList<Employees> Data)> GetProgrammerAsync();
}
