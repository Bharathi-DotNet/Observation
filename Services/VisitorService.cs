using AutoMapper;
using Enquiry.Web.Dtos;
using Enquiry.Web.Models;
using Enquiry.Web.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Enquiry.Web.Services
{
    public class VisitorService : IVisitor
    {
        readonly ApplicationDbContext _context;
        readonly IMapper _mapper;
        private readonly ISession _session;

        public VisitorService(ApplicationDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        { _context = context; _mapper = mapper; _session = httpContextAccessor.HttpContext.Session; }

        public async Task<(bool Succeeded, string[] Error, IList<VisitorsListDto> Data)> GetVisitorAsync()
        {
            try
            {
                var data = await _context.Visitors.Include(x => x.Employees).Include(x => x.Clients).OrderByDescending(x=>x.LoginTime).Take(100).ToListAsync();
                var result = _mapper.Map<IList<VisitorsListDto>>(data);
                return (true, new string[] { }, result);
            }
            catch (Exception ex)
            {
                return (false, new string[] { ex.Message }, null);
            }

        }

    }
}
