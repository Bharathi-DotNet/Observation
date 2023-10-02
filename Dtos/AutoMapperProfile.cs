using AutoMapper;
using Enquiry.Web.Extensions;
using Enquiry.Web.Models;

namespace Enquiry.Web.Dtos;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<EditClientDto, Clients>().ReverseMap();

        CreateMap<CreateClientDto, Clients>()
                .ForMember(d => d.IsActive, map => map.MapFrom(src => true))
                .ForMember(d => d.EnquiryDate, map => map.MapFrom(src => DatetimeExtension.ToDateTime(DateTimeOffset.Now)))
                .ForMember(d => d.EnquiryRef, map => map.MapFrom(src => string.Format("KR{0}", new Random().Next(10000, 99999))))
                .ForMember(d => d.Contact, map => map.MapFrom(src => $"{src.ContactNo} {src.Email}"))
                .ForMember(d => d.IsTechAppoinment, map => map.MapFrom(src => false))
                //.ForMember(d => d.Registered, map => map.MapFrom(src => false))
                .ReverseMap();

        CreateMap<CreateEmployeeDto, Employees>()
                .ForMember(d => d.IsActive, map => map.MapFrom(src => true))
                .ForMember(d => d.Password, map => map.MapFrom(src => src.ContactNo))
                .ReverseMap();

        CreateMap<ClientDetailsDto, ClientDetailDto>().ReverseMap();

        CreateMap<CreateProjectDto, Projects>().ReverseMap();

        CreateMap<CreatePhaseDto, Phase>()
            .ForMember(d => d.Status, map => map.MapFrom(src => (int)Enum.WorkStatus.Assigned)).ReverseMap();

        CreateMap<CreatePayment, Payments>().ReverseMap();

        CreateMap<Clients, RegisteredClientDetailDto>()
            .ForMember(src => src.ProjectDetailDto, dest => dest.MapFrom(po => po.Projects)).ReverseMap();
        CreateMap<Projects, ProjectDetailDto>()
            .ForMember(src => src.PaymentDetailDto, dest => dest.MapFrom(po => po.Payments))
            .ForMember(src => src.PhaseDetailDto, dest => dest.MapFrom(po => po.Phase))
            .ReverseMap();
        CreateMap<PaymentDetailDto, Payments>().ReverseMap();
        CreateMap<Phase, PhaseDetailDto>()
            .ForMember(src => src.TechExpert, dest => dest.MapFrom(po => po.Employees.EmployeeName))
            .ForMember(src => src.TechExpert1, dest => dest.MapFrom(po => po.Employees1.EmployeeName))
            .ForMember(src => src.TechExpert2, dest => dest.MapFrom(po => po.Employees2.EmployeeName))
            .ForMember(src => src.Programmer, dest => dest.MapFrom(po => po.Employees3.EmployeeName))
            .ForMember(src => src.Status, dest => dest.MapFrom(po => po.WorkStatus.WorkStatusName))
            .ReverseMap();

        CreateMap<EditPhaseDto, Phase>().ReverseMap();

        CreateMap<Phase, PublicationListDto>()
            .ForMember(src => src.RemainderDate, dest => dest.MapFrom(po => po.DeadLine))
            .ForMember(src => src.EnquiryRef, dest => dest.MapFrom(po => po.Projects.ProjectRef))
            .ForMember(src => src.ClientName, dest => dest.MapFrom(po => po.Projects.Clients.ClientName))
            .ForMember(src => src.Contact, dest => dest.MapFrom(po => po.Projects.Clients.Contact))
            .ForMember(src => src.Domain, dest => dest.MapFrom(po => po.Projects.Clients.Domain))
            .ForMember(src => src.ProjectName, dest => dest.MapFrom(po => po.Projects.ProjectName))
            .ForMember(src => src.ProjectId, dest => dest.MapFrom(po => po.Projects.ProjectId))
            .ForMember(src => src.EnquiryId, dest => dest.MapFrom(po => po.Projects.Clients.EnquiryId))
            .ForMember(src => src.PublishCount, dest => dest.MapFrom(po => po.Projects.Publication.Count(x=>x.Status == (int)Enum.JournalStatus.Published)))
            .ForMember(src => src.AcceptCount, dest => dest.MapFrom(po => po.Projects.Publication.Count(x => x.Status == (int)Enum.JournalStatus.Accepted)))
            .ForMember(src => src.MajorCount, dest => dest.MapFrom(po => po.Projects.Publication.Count(x => x.Status == (int)Enum.JournalStatus.Major)))
            .ForMember(src => src.MinorCount, dest => dest.MapFrom(po => po.Projects.Publication.Count(x => x.Status == (int)Enum.JournalStatus.Minor)))
            .ForMember(src => src.SubmitCount, dest => dest.MapFrom(po => po.Projects.Publication.Count(x => x.Status == (int)Enum.JournalStatus.Submitted)))
            .ForMember(src => src.RejectCount, dest => dest.MapFrom(po => po.Projects.Publication.Count(x => x.Status == (int)Enum.JournalStatus.Rejected)))
            .ReverseMap();

        CreateMap<JournalDetailDto, Publication>().ReverseMap();
        CreateMap<Visitors, VisitorsListDto>()
            .ForMember(src => src.ClientName, dest => dest.MapFrom(po => po.Clients.ClientName))
            .ForMember(src => src.EmployeeName, dest => dest.MapFrom(po => po.Employees.EmployeeName))
            .ReverseMap();
    }
}
