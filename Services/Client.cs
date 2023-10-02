using AutoMapper;
using Enquiry.Web.Dtos;
using Enquiry.Web.Extensions;
using Enquiry.Web.Models;
using Enquiry.Web.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Enquiry.Web.Services;

public class Client : IClient
{
    readonly ApplicationDbContext _context;
    readonly IMapper _mapper;
    private readonly ISession _session;
    public Client(ApplicationDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _mapper = mapper;
        _session = httpContextAccessor.HttpContext.Session;
    }
    public async Task<(bool Succeeded, string[] Error, IList<ClientListDto> Data)> GetClientsAsync(char chkValue)
    {
        try
        {
            var data = new List<ClientListDto>();
            var result = new List<Clients>();
            if (_session.GetString("Role") != Enum.Roles.Executive.ToString())
            {
                
                  if ( _session.GetString("Dept") == Enum.Department.BDA.ToString())
                  {
                        result = await _context.Clients.Where(x => x.IsActive.Equals(true) && x.Registered.Equals(false)).
                         Include(x => x.History).ThenInclude(x => x.Employees)
                       .Include(x => x.BDAEmployee).Include(x => x.TechEmployee).ToListAsync();
                  }
                  if (_session.GetString("Dept") == Enum.Department.Tech.ToString())
                  {
                        if (_session.GetString("Role") == Enum.Roles.Manager.ToString())
                        {
                            result = await _context.Clients.Where(x => x.IsActive.Equals(true) && x.Registered.Equals(false)).
                           Include(x => x.History).ThenInclude(x => x.Employees)
                           .Include(x => x.BDAEmployee).Include(x => x.TechEmployee).ToListAsync();
                        }
                        else 
                        {
                            result = await _context.Clients
                        .Where(x => x.IsActive.Equals(true) && x.Registered.Equals(false)
                        && (x.BDA == _session.GetInt32("CurrentUserId") || x.TechExpert == _session.GetInt32("CurrentUserId"))).Include(x => x.History)
                        .ThenInclude(x => x.Employees)
                        .Include(x => x.BDAEmployee).Include(x => x.TechEmployee).ToListAsync();
                        }

                }

            }
            
            else
            {
                result = await _context.Clients
                    .Where(x => x.IsActive.Equals(true) && x.Registered.Equals(false)
                    && (x.BDA == _session.GetInt32("CurrentUserId") || x.TechExpert == _session.GetInt32("CurrentUserId"))).Include(x => x.History)
                    .ThenInclude(x => x.Employees)
                    .Include(x => x.BDAEmployee).Include(x => x.TechEmployee).ToListAsync();
            }

            if (result == null) return (true, new string[] { }, data);

            DateTime date = DateTime.Now.AddDays(1);

            data = result.Select(x => new ClientListDto()
            {
                
                EnquiryId = x.EnquiryId,
                ClientName = x.ClientName,
                Contact = x.Contact,
                Domain = x.Domain,
                TechRemarks = x.History != null
                && x.History.Any(y => y.Employees.DeptId == (int)Enum.Department.Tech && y.IsComment == true)
                ? x.History.LastOrDefault(z => z.Employees.DeptId == (int)Enum.Department.Tech && z.IsComment == true).UpdatedDate.Date.ToString("dd/MM/yyyy") + Environment.NewLine +
                    x.History.LastOrDefault(z => z.Employees.DeptId == (int)Enum.Department.Tech && z.IsComment == true).Comments
         
                : " ",
                BDARemarks = x.History != null
                && x.History.Any(y => y.Employees.DeptId == (int)Enum.Department.BDA && y.IsComment == true)
                ? x.History.LastOrDefault(z => z.Employees.DeptId == (int)Enum.Department.BDA && z.IsComment == true).UpdatedDate.Date.ToString("dd/MM/yyyy") + Environment.NewLine +
                    x.History.LastOrDefault(z => z.Employees.DeptId == (int)Enum.Department.BDA && z.IsComment == true).Comments
                : " ",
                PaymentRemarks = x.PaymentRemarks,
                EnquiryDate = x.EnquiryDate,
                AppoinmentDate = x.AppoinmentDate,
                EnquiryRef = x.EnquiryRef,
                IsTechAppoinment = x.IsTechAppoinment,
                TechExperts = x.TechEmployee != null ? x.TechEmployee.EmployeeName: "",
                BDA = x.BDAEmployee != null ? x.BDAEmployee.EmployeeName : "",
                Registered = x.Registered
                            }).OrderBy(x => x.EnquiryDate).ToList();

            DateTime dtIST = DatetimeExtension.IndiaStandardTimeNow();
            if (chkValue.Equals('F'))
            {   //Future
                DateTime dt = (new DateTime(dtIST.Year, dtIST.Month, dtIST.Day, 10, 30, 0)).AddDays(1);
                data = data.Where(x => x.AppoinmentDate >= dt && x.IsTechAppoinment == false).OrderBy(x => x.AppoinmentDate).ToList();
            }
            else if (chkValue.Equals('P'))
            {   //Past
                DateTime dtTo = (new DateTime(dtIST.Year, dtIST.Month, dtIST.Day, 17, 30, 0)).AddDays(-1);
                dtTo = dtTo.AddDays(-7);
                data = data.Where(x => x.AppoinmentDate < dtTo && x.IsTechAppoinment == false).OrderBy(x => x.AppoinmentDate).ToList();
            }
            else if (chkValue.Equals('L'))
            {   //Last week
                DateTime dtTo = (new DateTime(dtIST.Year, dtIST.Month, dtIST.Day, 17, 30, 0)).AddDays(-1);
                DateTime dtFrom = dtTo.AddDays(-7);
                data = data.Where(x => (x.AppoinmentDate >= dtFrom && x.AppoinmentDate <= dtTo) && x.IsTechAppoinment == false).OrderBy(x => x.AppoinmentDate).ToList();
            }
            else if (chkValue.Equals('T'))
            {   //Today
                DateTime dtFrom = (new DateTime(dtIST.Year, dtIST.Month, dtIST.Day, 17, 30, 0)).AddDays(-1);
                DateTime dtTo = (new DateTime(dtIST.Year, dtIST.Month, dtIST.Day, 10, 30, 0)).AddDays(1);
                data = data.Where(x => (x.AppoinmentDate >= dtFrom && x.AppoinmentDate <= dtTo) && x.IsTechAppoinment == false).OrderBy(x => x.AppoinmentDate).ToList();
            }
            else if (chkValue.Equals('E'))
            {
                data = data.Where(x => x.IsTechAppoinment == true).OrderBy(x => x.AppoinmentDate).ToList();
            }

            //Show Tech enabled under ALL
            if (!chkValue.Equals('A') && !chkValue.Equals('E'))
            {
                data = data.Where(x => x.IsTechAppoinment == false).OrderBy(x => x.AppoinmentDate).ToList();
            }

            return (true, new string[] { }, data);
        }
        catch (Exception ex)
        {

            return (false, new string[] { ex.Message }, null);
        }

    }

    public async Task<(bool Succeeded, string[] Error, ClientDetailDto Data)> GetClientDetailAsync(int enquiryId)
    {
        try
        {
            var data = new ClientDetailDto();
            var result = await _context.Clients.Where(x => x.EnquiryId.Equals(enquiryId) && x.IsActive.Equals(true))
                .Include(x => x.BDAEmployee).Include(x => x.TechEmployee)
                .Include(x => x.History).ThenInclude(x => x.Employees).ThenInclude(x => x.Department).FirstOrDefaultAsync();

            if (result == null) return (true, new string[] { }, data);

            data.EnquiryId = result.EnquiryId;
            data.ClientName = result.ClientName;
            data.Contact = result.Contact;
            data.Domain = result.Domain;
            data.TechName = result.TechEmployee != null ? result.TechEmployee.EmployeeName :"" ;
            data.BDAName = result.BDAEmployee != null ? result.BDAEmployee.EmployeeName :"" ;
            data.TechId = result.TechEmployee != null ? result.TechEmployee.EmpId : null;
            data.BDAId = result.BDAEmployee != null ? result.BDAEmployee.EmpId : null;
            data.PaymentRemarks = result.PaymentRemarks;
            data.EnquiryDate = result.EnquiryDate;
            data.AppoinmentDate = result.AppoinmentDate;
            data.IsTechAppoinment = result.IsTechAppoinment;
            data.EnquiryRef = result.EnquiryRef;
            data.LastComment = result.History != null
                && result.History.Any(y => y.Employees.Department.DeptName == _session.GetString("Dept") && y.IsComment == true && y.EnquiryId == result.EnquiryId)
                ? result.History.LastOrDefault(z => z.Employees.Department.DeptName == _session.GetString("Dept") && z.IsComment == true && z.EnquiryId == result.EnquiryId).Comments
                :"" ;

            return (true, new string[] { }, data);
        }
        catch (Exception ex)
        {

            return (false, new string[] { ex.Message }, null);
        }

    }

    public async Task<(bool Succeeded, string[] Error)> CreateClientAsync(CreateClientDto dto)
    {
        try
        {
            var contacts = _context.Clients.Select(x => x.Contact.Split(null)).ToList();
            var clientExists = new List<Clients>();
            foreach (var item in contacts)
            {
                var client = await _context.Clients.Where(x => item.Contains(dto.Email) || item.Contains(dto.ContactNo)).FirstOrDefaultAsync();
                if (client != null)
                    clientExists.Add(client);
            }

            if (clientExists.Count() > 0)
            {
                string message = "Client already exists. Please contact the Manager";
                return (false, new string[] { message });
            }

            var map = _mapper.Map<Clients>(dto);
            map.AppoinmentDate = null;
            map.BDA = _session.GetInt32("CurrentUserId");
            map.IsTechAppoinment = dto.IsTechAppoinment;
            await _context.AddAsync(map);
            int result = await _context.SaveChangesAsync().ConfigureAwait(false);
            if (result > 0)
            {
                if (dto.AppoinmentDate.HasValue)
                {
                    await UpdateAppoinmentDateAsync(dto.AppoinmentDate.Value, map.EnquiryId);
                }
                if (!string.IsNullOrEmpty(dto.BDARemarks))
                    await AddEnquiryActivity(map.EnquiryId, (int)_session.GetInt32("CurrentUserId"), dto.BDARemarks, true);
            }
            return (true, new string[] { "Client successfully created" });
        }
        catch (Exception ex)
        {

            return (false, new string[] { ex.Message });
        }

    }

    public async Task<(bool Succeeded, string[] Error, EditClientDto data)> EditClientFormAsync(int enquiryId)
    {
        try
        {
            var result = await _context.Clients.FindAsync(enquiryId);
            var map = _mapper.Map<EditClientDto>(result);
            return (true, new string[] { }, map);
        }
        catch (Exception ex)
        {

            return (false, new string[] { ex.Message }, null);
        }

    }

    public async Task<(bool Succeeded, string[] Error)> EditClientAsync(EditClientDto dto)
    {
        try
        {
            var client = await _context.Clients.FindAsync(dto.EnquiryId);
            DateTime? oldAppoinment = client.AppoinmentDate;
            var map = _mapper.Map<EditClientDto, Clients>(dto, client);
            map.AppoinmentDate = oldAppoinment;
            _context.Update(map);
            int result = await _context.SaveChangesAsync().ConfigureAwait(false);
            if (result > 0)
            {
                if (dto.AppoinmentDate.HasValue
                    || (client.AppoinmentDate.HasValue && (DateTime.Compare(dto.AppoinmentDate.Value, client.AppoinmentDate.Value) > 0)))
                {
                    await UpdateAppoinmentDateAsync(dto.AppoinmentDate.Value, map.EnquiryId);
                }
            }
            return (true, new string[] { "Client successfully updated" });
        }
        catch (Exception ex)
        {

            return (false, new string[] { ex.Message });
        }

    }

    public async Task<(bool Succeeded, string[] Error)> DeleteClientAsync(int enquiryId)
    {
        try
        {
            var client = await _context.Clients.FindAsync(enquiryId);
            _context.Remove(client);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return (true, new string[] { "Client successfully removed" });
        }
        catch (Exception ex)
        {

            return (false, new string[] { ex.Message });
        }

    }

    public async Task<(bool Succeeded, string[] Error)> UpdateBDAForClientAsync(int bdaId, int enquiryId)
    {
        try
        {
            var result = await _context.Clients.FindAsync(enquiryId);
            result.BDA = bdaId;
            _context.Update(result);
            int val = await _context.SaveChangesAsync().ConfigureAwait(false);
            if (val > 0)
            {
                string message = "Assigned BDA";
                await AddEnquiryActivity(result.EnquiryId, bdaId, message);
            }
            return (true, new string[] { });
        }
        catch (Exception ex)
        {

            return (false, new string[] { ex.Message });
        }

    }

    public async Task<(bool Succeeded, string[] Error)> UpdateTechExpertForClientAsync(int techId, int enquiryId)
    {
        try
        {
            var result = await _context.Clients.FindAsync(enquiryId);
            result.TechExpert = techId;
            _context.Update(result);
            int val = await _context.SaveChangesAsync().ConfigureAwait(false);
            if (val > 0)
            {
                string message = "Assigned Technician";
                await AddEnquiryActivity(result.EnquiryId, techId, message);
            }
            return (true, new string[] { });
        }
        catch (Exception ex)
        {

            return (false, new string[] { ex.Message });
        }

    }

    public async Task<(bool Succeeded, string[] Error, IList<ClientActivityDto> Data)> GetClientActivityAsync(int enquiryId, bool isComment)
    {
        try
        {
            var data = new List<ClientActivityDto>();
            var result = await _context.History.Where(x => x.EnquiryId.Equals(enquiryId))
                .Include(x => x.Employees).Include(x => x.Clients).ToListAsync();

            if (result == null) return (true, new string[] { }, data);
            if (result == null && !isComment) return (true, new string[] { }, data);
            data = result.Where(t => t.IsComment == isComment).Select(x => new ClientActivityDto()
            {
                Id = x.HistoryId,
                ClientName = x.Clients.ClientName,
                EmployeeName = x.Employees.EmployeeName,
                Comments = x.Comments,
                CreatedDate = x.CreatedDate
            }).OrderByDescending(t => t.CreatedDate).ThenByDescending(t => t.Id).ToList();

            return (true, new string[] { }, data);
        }
        catch (Exception ex)
        {

            return (false, new string[] { ex.Message }, null);
        }

    }

    public async Task<(bool Succeeded, string[] Error)> UpdatePaymentRemarksAsync(int enquiryId, string remarks, bool isRegsitered)
    {
        try
        {
            var result = await _context.Clients.FindAsync(enquiryId);
            result.PaymentRemarks = remarks;
            result.Registered = isRegsitered;
            _context.Update(result);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return (true, new string[] { });
        }
        catch (Exception ex)
        {

            return (false, new string[] { ex.Message });
        }

    }

    public async Task AddEnquiryActivity(int enquiryId, int empId, string comment, bool isComment = false)
    {
        var history = new History()
        {
            EnquiryId = enquiryId,
            EmpId = empId,
            Comments = comment,
            IsComment = isComment
        };
        await _context.AddAsync(history);
        await _context.SaveChangesAsync().ConfigureAwait(false);
    }

    public async Task<(bool Succeeded, string[] Error)> UpdateAppoinmentDateAsync(DateTime appoinmentDate, int enquiryId)
    {
        try
        {
            string message = String.Empty;
            var result = await _context.Clients.FindAsync(enquiryId);

            if (result.AppoinmentDate == null) message = $"Appoinment dated on {appoinmentDate}";
            else message = $"Changed appoinment date from {result.AppoinmentDate} to {appoinmentDate}";

            result.AppoinmentDate = DatetimeExtension.ToDateTime(appoinmentDate);
            _context.Update(result);
            int val = await _context.SaveChangesAsync().ConfigureAwait(false);
            if (val > 0)
            {
                await AddEnquiryActivity(result.EnquiryId, (int)_session.GetInt32("CurrentUserId"), message);
            }
            return (true, new string[] { });
        }
        catch (Exception ex)
        {

            return (false, new string[] { ex.Message });
        }
    }

    public async Task<(bool Succeeded, string[] Error)> UpdateAsTechAppoinmentAsync(bool ChkValue, int enquiryId)
    {
        try
        {
            string message = String.Empty;
            var result = await _context.Clients.Where(x => x.EnquiryId == enquiryId)
                .Include(x => x.TechEmployee).Include(x => x.BDAEmployee).FirstOrDefaultAsync();
            DateTime dt = DatetimeExtension.IndiaStandardTimeNow();
            result.IsTechAppoinment = ChkValue;
            result.AppoinmentDate = dt;
            _context.Update(result);
            int val = await _context.SaveChangesAsync().ConfigureAwait(false);
            if (val > 0)
            {
                if (ChkValue) message = $"Appoinment assigned to the technical person {result.TechEmployee.EmployeeName}";
                else message = $"Appoinment assigned to the bda {result.BDAEmployee.EmployeeName}";
                await AddEnquiryActivity(result.EnquiryId, (int)_session.GetInt32("CurrentUserId"), message);
            }
            return (true, new string[] { });
        }
        catch (Exception ex)
        {

            return (false, new string[] { ex.Message });
        }
    }

    public async Task<(bool Succeeded, string[] Error, Clients Clients)> IsRegisteredAsync(int enquiryId, bool isRegsitered)
    {
        try
        {
            var result = await _context.Clients.FindAsync(enquiryId);
            result.Registered = isRegsitered;
            _context.Update(result);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return (true, new string[] { }, result);
        }
        catch (Exception ex)
        {

            return (false, new string[] { ex.Message }, null);
        }

    }

    public async Task<(bool Succeeded, string[] Error)> UpdateBackToEnquiryAsync(int enquiryId)
    {
        try
        {
            var client = await _context.Clients.FindAsync(enquiryId);
            client.BackToEnquiry = true;
            _context.Update(client);
            int result = await _context.SaveChangesAsync().ConfigureAwait(false);
            if (result > 0)
            {
                var map = _mapper.Map<Clients>(client);
                map.EnquiryId = 0;
                map.PreviousEnquiryId = client.EnquiryId;
                map.IsTechAppoinment = false;
                map.Registered = false;
                map.BackToEnquiry = false;
                map.AppoinmentDate = DateTime.Now;
                await _context.AddAsync(map);
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            return (true, new string[] { });
        }
        catch (Exception ex)
        {

            return (false, new string[] { ex.Message });
        }
    }
}
