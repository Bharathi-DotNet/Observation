using AutoMapper;
using Enquiry.Web.Dtos;
using Enquiry.Web.Models;
using Enquiry.Web.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Enquiry.Web.Services;

public class Employee : IEmployee
{
    readonly ApplicationDbContext _context;
    readonly IMapper _mapper;

    public Employee(ApplicationDbContext context, IMapper mapper) { _context = context; _mapper = mapper; }

    public async Task<(bool Succeeded, string[] Error, IList<Roles> Data)> GetRolesAsync()
    {
        try
        {
            var data = await _context.Roles.Where(x => x.IsActive == true).ToListAsync();

            return (true, new string[] { }, data);
        }
        catch (Exception ex)
        {

            return (false, new string[] { ex.Message }, null);
        }

    }

    public async Task<(bool Succeeded, string[] Error, IList<Department> Data)> GetDepartmentAsync()
    {
        try
        {
            var data = await _context.Department.Where(x => x.IsActive == true).ToListAsync();

            return (true, new string[] { }, data);
        }
        catch (Exception ex)
        {

            return (false, new string[] { ex.Message }, null);
        }

    }

    public async Task<(bool Succeeded, string[] Error, IList<EmployeeListDto> Data)> GetEmployeeAsync()
    {
        try
        {
            var result = new List<EmployeeListDto>();

            var data = await _context.Employees.Where(x => x.IsActive == true)
                .Include(x => x.Roles).Include(x => x.Department).ToListAsync();

            result = data.Select(x => new EmployeeListDto()
            {
                EmpId = x.EmpId,
                IdentityNo = x.IdentityNo,
                EmployeeName = x.EmployeeName,
                ContactNo = x.ContactNo,
                EmailId = x.EmailId,
                Address = x.Address,
                RoleName = x.Roles.RoleName,
                DeptName = x.Department.DeptName
            }).OrderByDescending(x=>x.EmpId).ToList();

            return (true, new string[] { }, result);
        }
        catch (Exception ex)
        {

            return (false, new string[] { ex.Message }, null);
        }

    }

    public async Task<(bool Succeeded, string[] Error)> CreateEmployeeAsync(CreateEmployeeDto dto)
    {
        try
        {
            var map = _mapper.Map<Employees>(dto);
            await _context.AddAsync(map);
            int result = await _context.SaveChangesAsync().ConfigureAwait(false);
            if (result > 0)
            {
                var id = map.EmpId.ToString().PadLeft(4, '0');
                map.IdentityNo = $"KF" + id;
                _context.Update(map);
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            return (true, new string[] { "Employee successfully created" });
        }
        catch (Exception ex)
        {

            return (false, new string[] { ex.Message });
        }

    }

    public async Task<(bool Succeeded, string[] Error, IList<Employees> Data)> GetBDAAsync()
    {
        try
        {
            var data = await _context.Employees.Where(x => x.IsActive == true && x.DeptId == 1).ToListAsync();

            return (true, new string[] { }, data);
        }
        catch (Exception ex)
        {

            return (false, new string[] { ex.Message }, null);
        }

    }

    public async Task<(bool Succeeded, string[] Error, IList<Employees> Data)> GetTechExpertAsync()
    {
        try
        {
            var data = await _context.Employees.Where(x => x.IsActive == true && x.DeptId == 2).ToListAsync();

            return (true, new string[] { }, data);
        }
        catch (Exception ex)
        {

            return (false, new string[] { ex.Message }, null);
        }

    }

    public async Task<(bool Succeeded, string[] Error, IList<Employees> Data)> GetProgrammerAsync()
    {
        try
        {
            var data = await _context.Employees.Where(x => x.IsActive == true && x.DeptId == 3).ToListAsync();

            return (true, new string[] { }, data);
        }
        catch (Exception ex)
        {

            return (false, new string[] { ex.Message }, null);
        }

    }
}
