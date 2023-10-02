using AutoMapper;
using Enquiry.Web.Dtos;
using Enquiry.Web.Models;
using Enquiry.Web.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Enquiry.Web.Services
{
    public class Project : IProject
    {
        readonly ApplicationDbContext _context;
        readonly IMapper _mapper;
        private readonly ISession _session;

        public Project(ApplicationDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        { _context = context; _mapper = mapper; _session = httpContextAccessor.HttpContext.Session; }

        public async Task<(bool Succeeded, string[] Error, IList<ProjectListDto> Data)> GetPhaseAsync()
        {
            try
            {
                IEnumerable<Phase> phase;

                var project = await _context.Projects
                    .Include(x => x.Phase).ThenInclude(x => x.Employees)
                    .Include(x => x.Phase).ThenInclude(x => x.Employees1)
                    .Include(x => x.Phase).ThenInclude(x => x.Employees2)
                    .Include(x => x.Phase).ThenInclude(x => x.WorkStatus)
                    .Include(x => x.Phase).ThenInclude(x => x.Employees3)
                    .Include(x => x.Clients).ThenInclude(x => x.BDAEmployee)
                    .Include(x => x.Clients).ThenInclude(x => x.TechEmployee).ToListAsync();

                if (_session.GetString("Role") == Enum.Roles.Manager.ToString())
                {
                    phase = project.Where(x => x.Phase.Count() > 0).Select(x => x.Phase.OrderByDescending(x => x.ProjectId).LastOrDefault());
                }
                else if (_session.GetString("Dept") == Enum.Department.BDA.ToString())
                {
                    phase = project.Where(x => x.Phase.Count() > 0).Select(x => x.Phase.OrderByDescending(x => x.ProjectId).LastOrDefault());
                }
                else if (_session.GetString("Dept") == Enum.Department.Programming.ToString())
                {
                    phase = project.Where(x => x.Phase.Count() > 0 && x.Phase != null && x.Phase.Where(y => y.Employees3 is not null).Select(z => z.Employees3.EmpId).ToList()
                                .Contains(Int32.Parse(_session.GetInt32("CurrentUserId").ToString()))).Select(x => x.Phase.OrderByDescending(x => x.ProjectId).LastOrDefault());

                    phase = phase.Where(phasetmp =>
                          phasetmp.TechExpert == Int32.Parse(_session.GetInt32("CurrentUserId").ToString()) ||
                          phasetmp.TechExpert1 == Int32.Parse(_session.GetInt32("CurrentUserId").ToString()) ||
                          phasetmp.TechExpert2 == Int32.Parse(_session.GetInt32("CurrentUserId").ToString()) ||
                          phasetmp.Programmer == Int32.Parse(_session.GetInt32("CurrentUserId").ToString())
                      );
                }
                /*else if (_session.GetString("Dept") == Enum.Department.Tech.ToString() &&
                    _session.GetString("Role") == Enum.Roles.Manager.ToString())
                {
                    phase = project.Where(x => x.Phase.Count() > 0).Select(x => x.Phase.OrderByDescending(x => x.ProjectId).LastOrDefault());
                }*/
                else
                {
                    phase = project.Where(x => x.Phase.Count() > 0 &&
                            (/*(x.Clients.BDAEmployee != null && x.Clients.BDAEmployee.EmpId == Int32.Parse(_session.GetInt32("CurrentUserId").ToString())) ||
                                (x.Clients.TechEmployee != null && x.Clients.TechEmployee.EmpId == Int32.Parse(_session.GetInt32("CurrentUserId").ToString()))
                                ||*/ ((x.Phase != null && x.Phase.Where(y => y.Employees is not null).Select(z => z.Employees.EmpId).ToList()
                                .Contains(Int32.Parse(_session.GetInt32("CurrentUserId").ToString()))) || (x.Phase.LastOrDefault() != null && x.Phase.Where(y => y.Employees1 is not null).Select(z => z.Employees1.EmpId).ToList()
                                .Contains(Int32.Parse(_session.GetInt32("CurrentUserId").ToString()))) || (x.Phase.LastOrDefault() != null && x.Phase.Where(y => y.Employees2 is not null).Select(z => z.Employees2.EmpId).ToList()
                                .Contains(Int32.Parse(_session.GetInt32("CurrentUserId").ToString()))))
                                )).Select(x => x.Phase.OrderByDescending(x => x.ProjectId).LastOrDefault());


                    phase = phase.Where(phasetmp =>
                            phasetmp.TechExpert  == Int32.Parse(_session.GetInt32("CurrentUserId").ToString()) ||
                            phasetmp.TechExpert1 == Int32.Parse(_session.GetInt32("CurrentUserId").ToString()) ||
                            phasetmp.TechExpert2 == Int32.Parse(_session.GetInt32("CurrentUserId").ToString()) ||
                            phasetmp.Programmer  == Int32.Parse(_session.GetInt32("CurrentUserId").ToString())
                        );

                }


                List<String> lstCustomSort = new List<String>() { "Publication", "Completed","Published"};


                var result = phase.Select(t => new ProjectListDto()
                {
                    EnquiryId = t.Projects.Clients.EnquiryId,
                    PhaseId = t.PhaseId,
                    ProjectId = t.ProjectId,
                    ClientName = t.Projects.Clients.ClientName,
                    ProjectName = t.Projects.ProjectName,
                    PhaseName = t.PhaseName,
                    EnquiryRef = t.Projects.ProjectRef,
                    Contact = t.Projects.Clients.Contact,
                    Domain = t.Projects.Clients.Domain,
                    DeadLine = t.DeadLine,
                    NextAppoinment =t.NextAppoinment,
                   // DemoDate =  t.DemoDate,
                    //DemoDate = t.NextAppoinment <  t.DemoDate  ? t.NextAppoinment : t.DemoDate,
                    // DemoDate = t.NextAppoinment != null ? t.NextAppoinment : t.DemoDate,
                    DemoDate = t.Status == (int)Enum.WorkStatus.Hold  ? t.NextAppoinment : t.DemoDate,
                    DemoDate3 = t.Status == (int)Enum.WorkStatus.Hold ? t.NextAppoinment : t.DemoDate3,
                    Status = t.WorkStatus.WorkStatusName,
                    AssignedTech = t.Employees == null ? "" : t.Employees.EmployeeName,
                    AssignedTech1 = t.Employees1 == null ? "" : t.Employees1.EmployeeName,
                    AssignedTech2 = t.Employees2 == null ? "" : t.Employees2.EmployeeName,
                    AssignedTech3 = t.Employees3 == null ? "" : t.Employees3.EmployeeName,
                    Comment = t.Comment,


                }).OrderBy(x => x.DeadLine).ToList()
                .OrderBy(x => x.DemoDate3).ToList()
                .OrderBy(x => x.DemoDate).ToList()
                .OrderBy(x => lstCustomSort.Contains(x.Status)? 2: 1).ToList();
                
                
                    

                return (true, new string[] { }, result);
            }
            catch (Exception ex)
            {

                return (false, new string[] { ex.Message }, null);
            }

        }
        public async Task<(bool Succeeded, string[] Error, Projects Project)> CreateProjectAsync(CreateProjectDto projectDto)
        {
            try
            {
                var map = _mapper.Map<Projects>(projectDto);
                map.EnquiryId = projectDto.ClientDetailsDto.EnquiryId;
                await _context.AddAsync(map);
                int result = await _context.SaveChangesAsync().ConfigureAwait(false);
                if (result > 0)
                {
                    var client = await _context.Clients.Where(x => x.EnquiryRef == projectDto.ClientDetailsDto.EnquiryRef &&
                                                                x.EnquiryId == projectDto.ClientDetailsDto.EnquiryId)
                        .Include(x => x.Projects).ToListAsync();
                    map.ProjectRef = $"{client[0].EnquiryRef}-{client[0].Projects.Count()}";
                    _context.Update(map);
                    await _context.SaveChangesAsync().ConfigureAwait(false);
                }
                return (true, new string[] { "Project added sucessfully" }, map);
            }
            catch (Exception ex)
            {

                return (false, new string[] { ex.Message }, null);
            }

        }

        public async Task<(bool Succeeded, string[] Error, Phase Phase)> CreatePhaseAsync(CreatePhaseDto phaseDto, int projectId)
        {
            try
            {
                var map = _mapper.Map<Phase>(phaseDto);
                map.ProjectId = projectId;
                if (phaseDto.IsPublication) {
                    /*var pub = new List<Models.Publication>();*/
                    map.Status = (int)Enum.WorkStatus.Publication;
                    /*var subPub = new Models.Publication();
                    {
                        ProjectId = projectId;
                    }
                    pub.Add(subPub);*/

                    /*for (int i = 0; i <= 30; i++)
                    {
                        var subPub = new Models.Publication()
                        {
                            ProjectId = projectId
                        };
                        pub.Add(subPub);
                    }*/
                   /* await _context.Publication.AddRangeAsync(pub);
                    await _context.SaveChangesAsync();*/
                } 
                await _context.AddAsync(map);
                int result = await _context.SaveChangesAsync().ConfigureAwait(false);
                return (true, new string[] { "Project added sucessfully" }, map);
            }
            catch (Exception ex)
            {

                return (false, new string[] { ex.Message }, null);
            }

        }

        public async Task<(bool Succeeded, string[] Error, Phase Phase)> UpdatePhaseAsync(EditPhaseDto phaseDto)
        {
            try
            {
                var phase = await _context.Phase.FindAsync(phaseDto.PhaseId);
                var map = _mapper.Map<EditPhaseDto, Phase>(phaseDto, phase);
                if (map.DemoDate.HasValue) map.Status = (int)Enum.WorkStatus.Progress;
                else if (map.DemoDate3.HasValue) map.Status = (int)Enum.WorkStatus.Progress;
                else map.Status = (int)Enum.WorkStatus.Assigned;
                _context.Update(map);
                int result = await _context.SaveChangesAsync().ConfigureAwait(false);
                return (true, new string[] { "Project updated sucessfully" }, map);
            }
            catch (Exception ex)
            {

                return (false, new string[] { ex.Message }, null);
            }

        }

        public async Task<(bool Succeeded, string[] Error, Payments Phase)> CreatePaymentAsync(CreatePayment payment, int projectId)
        {
            try
            {
                var map = _mapper.Map<Payments>(payment);
                map.ProjectId = projectId;
                await _context.AddAsync(map);
                int result = await _context.SaveChangesAsync().ConfigureAwait(false);
                return (true, new string[] { "Payment added sucessfully" }, map);
            }
            catch (Exception ex)
            {

                return (false, new string[] { ex.Message }, null);
            }

        }

        public async Task<(bool Succeeded, string[] Error, Models.Publication Publication)> CreateJournalAsync(JournalDetailDto journal, int projectId)
        {
            try
            {
                var map = _mapper.Map<Models.Publication>(journal);
                map.ProjectId = projectId;

                if (_session.GetInt32("CurrentUserId") is not null)
                {
                    map.CreatedBy = (int)_session.GetInt32("CurrentUserId");
                    map.UpdatedBy = (int)_session.GetInt32("CurrentUserId");
                }

                map.CreatedDate= DateTime.Now;
                map.UpdatedDate= DateTime.Now;


                await _context.AddAsync(map);
                int result = await _context.SaveChangesAsync().ConfigureAwait(false);
                return (true, new string[] { "Journal added sucessfully" }, map);
            }
            catch (Exception ex)
            {

                return (false, new string[] { ex.Message }, null);
            }

        }

        public async Task<(bool Succeeded, string[] Error, Models.Publication Publication)> UpdateJournalAsync(JournalDetailDto dto)
        {
            try
            {
                var jrl = await _context.Publication.FindAsync(dto.PublicationId);
                jrl.RemainderDate = dto.RemainderDate;
                jrl.JournelName = dto.JournelName;
                jrl.Link = dto.Link;
                jrl.Publisher = dto.Publisher;
                jrl.Reason = dto.Reason;
                jrl.DateOfSubmission = dto.DateOfSubmission;
                jrl.Status = (int)dto.Status;                                
                jrl.UserId = dto.UserId;
                jrl.Password = dto.Password;

                if (_session.GetInt32("CurrentUserId") is not null)
                {
                    jrl.UpdatedBy = (int)_session.GetInt32("CurrentUserId");
                }

                jrl.UpdatedDate = DateTime.Now;

                _context.Update(jrl);
                int result = await _context.SaveChangesAsync().ConfigureAwait(false);
                return (true, new string[] { "Journal updated sucessfully" }, jrl);
            }
            catch (Exception ex)
            {

                return (false, new string[] { ex.Message }, null);
            }

        }

        public async Task<(bool Succeeded, string[] Error, RegisteredClientDetailDto Client)> GetRegisteredClientDetailAsync(int enquiryId)
        {
            try
            {
                var project = await _context.Clients.Where(x => x.EnquiryId == enquiryId && x.IsActive == true)
                    
                    .Include(x => x.BDAEmployee).Include(x => x.TechEmployee)
                    .Include(x => x.Projects).ThenInclude(x => x.Payments)
                   

                    .Include(x => x.Projects).ThenInclude(x => x.Phase).FirstOrDefaultAsync();

                var map = _mapper.Map<RegisteredClientDetailDto>(project);
                map.BDAName = project.BDAEmployee.EmployeeName;
                map.TechName = project.TechEmployee.EmployeeName;
     
                

                return (true, new string[] { }, map);
            }
            catch (Exception ex)
            {

                return (false, new string[] { ex.Message }, null);
            }

        }

        public async Task<(bool Succeeded, string[] Error, ProjectDetailDto Project)> GetProjectDetailAsync(int projectId)
        {
            try
            {
                var project = await _context.Projects.Where(x => x.ProjectId == projectId).Include(x => x.Payments)
                    .Include(x => x.Phase).ThenInclude(x => x.Employees)
                    .Include(x => x.Phase).ThenInclude(x => x.Employees1)
                    .Include(x => x.Phase).ThenInclude(x => x.Employees2)
                    .Include(x => x.Phase).ThenInclude(x => x.Employees3)
                    .Include(x => x.Phase).ThenInclude(x => x.WorkStatus)
                    .Include(x=>x.Phase).ThenInclude(x=>x.PhaseHistory)
                    .Include(x => x.Phase).ThenInclude(x => x.PhaseHistory).ThenInclude(x=>x.Employees).FirstOrDefaultAsync();

                var map = _mapper.Map<ProjectDetailDto>(project);
                foreach (var item in map.PhaseDetailDto)
                {
                    var pHistory = await _context.PhaseHistory.Where(x => x.PhaseId == item.PhaseId).ToListAsync();
                    if (pHistory.Any())
                    {
                        var pHisList = new List<PhaseHistoryDto>();
                        
                        foreach (var item1 in pHistory)
                        {
                            var pHis = new PhaseHistoryDto()
                            {
                                Comments = item1.Comments,
                                EmpName = item1.Employees.EmployeeName,
                                CreatedDate = item1.CreatedDate
                            };
                            pHisList.Add(pHis);
                        };
                        map.PhaseHistoryDto.AddRange(pHisList);
                    };
                }

                return (true, new string[] { }, map);
            }
            catch (Exception ex)
            {

                return (false, new string[] { ex.Message }, null);
            }

        }

        public async Task<(bool Succeeded, string[] Error, EditPhaseDto Phase)> GetPhaseAsync(int phaseId)
        {
            try
            {
                var result = await _context.Phase.FindAsync(phaseId);
                var map = _mapper.Map<EditPhaseDto>(result);
                return (true, new string[] { }, map);
            }
            catch (Exception ex)
            {

                return (false, new string[] { ex.Message }, null);
            }

        }

        public async Task<(bool Succeeded, string[] Error, Phase Phase)> UpdatePhaseOnHoldAsync(EditPhaseOnHoldDto phaseDto, int phaseId)
        {
            try
            {
                var phase = await _context.Phase.FindAsync(phaseId);
                var phaseCommnet = new PhaseHistory()
                {
                    PhaseId = phaseId,
                    Comments = phaseDto.Comment,
                    EmpId = (int)_session.GetInt32("CurrentUserId")
                };
                await _context.AddAsync(phaseCommnet);
                phase.Comment = $"{phaseDto.Comment} *{ _session.GetString("EmployeeName")}* ";
                phase.NextAppoinment = phaseDto.NextAppoinment;
                //phase.Status = (int)Enum.WorkStatus.Hold;
                
                if (phaseDto.IsCompleted)
                {
                    phase.Status = (int)Enum.WorkStatus.Completed;
                }
                if (phaseDto.IsCompletedphaseqc)
                {
                    phase.Status = (int)Enum.WorkStatus.Assigned;
                    
                }
                if (phaseDto.IsCompletedphase)
                {
                    phase.Status = (int)Enum.WorkStatus.Hold;

                }
                _context.Update(phase);
                int result = await _context.SaveChangesAsync().ConfigureAwait(false);
                return (true, new string[] { "Project updated sucessfully" }, phase);
            }
            catch (Exception ex)
            {

                return (false, new string[] { ex.Message }, null);
            }

        }

        public async Task<(bool Succeeded, string[] Error, EditPhaseOnHoldDto Phase)> EditProjectOnHoldFormAsync(int phaseId)
        {
            try
            {
                var phase = await _context.Phase.Where(x => x.PhaseId == phaseId).Select(x => new EditPhaseOnHoldDto()
                {
                    Comment = x.Comment,
                    NextAppoinment = x.NextAppoinment
                }).FirstOrDefaultAsync();

                return (true, new string[] { }, phase);
            }
            catch (Exception ex)
            {

                return (false, new string[] { ex.Message }, null);
            }

        }

        public async Task<(bool Succeeded, string[] Error, Phase Phase)> UpdateProgressAsync(int progressId, int phaseId, int value)
        {
            try
            {
                var phase = await _context.Phase.FindAsync(phaseId);
                if (progressId == 1) phase.Progress = (int)Math.Round((decimal)value / 5, MidpointRounding.AwayFromZero) * 5;
                if (progressId == 2) phase.Progress1 = (int)Math.Round((decimal)value / 5, MidpointRounding.AwayFromZero) * 5;
                if (progressId == 3) phase.Progress2 = (int)Math.Round((decimal)value / 5, MidpointRounding.AwayFromZero) * 5;
                if (progressId == 4) phase.Progress3 = (int)Math.Round((decimal)value / 5, MidpointRounding.AwayFromZero) * 5;
                _context.Update(phase);
                int result = await _context.SaveChangesAsync().ConfigureAwait(false);
                return (true, new string[] { "Project updated sucessfully" }, phase);
            }
            catch (Exception ex)
            {

                return (false, new string[] { ex.Message }, null);
            }

        }

    }
}
