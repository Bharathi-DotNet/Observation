using System.ComponentModel.DataAnnotations;

namespace Enquiry.Web.Dtos;

public class PublicationListDto
{
    public IList<PhaseDetailDto> PhaseDetailDto { get; set; }
    public int PublicationId { get; set; }
    public int ProjectId { get; set; }
    public int EnquiryId { get; set; }
    public string ClientName { get; set; }
    public string EnquiryRef { get; set; }
    public string Contact { get; set; }
    public string Domain { get; set; }
    public string ProjectName { get; set; }
    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime RemainderDate { get; set; }
    public string JournelName { get; set; }
    public string Link { get; set; }
    public string Publisher { get; set; }
    public string Index { get; set; }
    public string UploadedDate { get; set; }
    public int Status { get; set; }
    public int PublishCount { get; set; }
    public int AcceptCount { get; set; }
    public int MajorCount { get; set; }
    public int MinorCount { get; set; }
    public int SubmitCount { get; set; }
    public int RejectCount { get; set; }
    public string Remarks { get; set; }
}

