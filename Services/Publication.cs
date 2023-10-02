using AutoMapper;
using Enquiry.Web.Dtos;
using Enquiry.Web.Extensions;
using Enquiry.Web.Models;
using Enquiry.Web.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Enquiry.Web.Services
{
    public class Publication: IPublication
    {
        readonly ApplicationDbContext _context;
        readonly IMapper _mapper;
        private readonly ISession _session;

        public Publication(ApplicationDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        { _context = context; _mapper = mapper; _session = httpContextAccessor.HttpContext.Session; }

        public async Task<(bool Succeeded, string[] Error, IList<PublicationListDto> Publication)> GetPublicationListAsync(char chkValue)
        {
            try
            {
                var publications = await _context.Phase.Where(x => x.Status == (int)Enum.WorkStatus.Publication)
                    .Include(x => x.Projects).ThenInclude(x => x.Clients)
                    .Include(x => x.Projects).ThenInclude(x => x.Publication).ToListAsync();


                if (chkValue.Equals('A'))
                {   // ALL

                }
                else if (chkValue.Equals('S'))
                {   // Subnit/Rejected
                    publications = publications.Where(x => x.Status == (int)Enum.JournalStatus.Submitted ||
                                                           x.Status == (int)Enum.JournalStatus.Rejected
                                                            ).ToList();
                }
                else if (chkValue.Equals('M'))
                {   // Major/Minor
                    publications = publications.Where(x => x.Status == (int)Enum.JournalStatus.Submitted ||
                                                           x.Status == (int)Enum.JournalStatus.Rejected ||
                                                           x.Status == (int)Enum.JournalStatus.Major ||
                                                           x.Status == (int)Enum.JournalStatus.Minor
                                                            ).ToList();
                }
                else if (chkValue.Equals('C'))
                {   // Accepted
                    publications = publications.Where(x => x.Status == (int)Enum.JournalStatus.Submitted ||
                                                           x.Status == (int)Enum.JournalStatus.Rejected ||
                                                           x.Status == (int)Enum.JournalStatus.Major ||
                                                           x.Status == (int)Enum.JournalStatus.Minor ||
                                                           x.Status == (int)Enum.JournalStatus.Accepted ).ToList();
                }
                else if (chkValue.Equals('P'))
                {  // Published
                    publications = publications.Where(x => x.Status == (int)Enum.JournalStatus.Submitted ||
                                                           x.Status == (int)Enum.JournalStatus.Rejected ||
                                                           x.Status == (int)Enum.JournalStatus.Major ||
                                                           x.Status == (int)Enum.JournalStatus.Minor ||
                                                           x.Status == (int)Enum.JournalStatus.Accepted ||
                                                           x.Status == (int)Enum.JournalStatus.Published).ToList();
                }


                var map = _mapper.Map<IList<PublicationListDto>>(publications);
                
                return (true, new string[] { }, map.GroupBy(x => x.ProjectId).Select(g=> g.LastOrDefault())
                    .OrderBy(x=>x.RemainderDate).ToList());
            }
            catch (Exception ex)
            {

                return (false, new string[] { ex.Message }, null);
            }

        }

        public async Task<(bool Succeeded, string[] Error, IList<JournalDetailDto> Data)> GetJournalListAsync(int projectId)
        {
            try
            {
                var jrl = await _context.Publication.Where(x => x.ProjectId == projectId).ToListAsync();
                var map = _mapper.Map<IList<JournalDetailDto>>(jrl);
                return (true, new string[] { }, map);
            }
            catch (Exception ex)
            {

                return (false, new string[] { ex.Message }, null);
            }

        }

        public async Task<(bool Succeeded, string[] Error, JournalDetailDto Data)> GetJournalAsync(int publicationid)
        {
            try
            {
                var jrl = await _context.Publication.Where(x => x.PublicationId == publicationid).FirstOrDefaultAsync();
                var map = _mapper.Map<JournalDetailDto>(jrl);
                return (true, new string[] { }, map);
            }
            catch (Exception ex)
            {

                return (false, new string[] { ex.Message }, null);
            }

        }

    }
}
