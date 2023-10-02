using System.ComponentModel.DataAnnotations;

namespace Enquiry.Web.Dtos
{
    public class JournalDetailDto
    {
        public JournalDetailDto()
        {
            DateOfSubmission = DateTime.Now;
            RemainderDate = DateTime.Now;
        }
        public int PublicationId { get; set; }
        public int ProjectId { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime RemainderDate { get; set; }
        
        public string JournelName { get; set; }
        
        public string Link { get; set; }
        
        public string UserId { get; set; }
        
        public string Password { get; set; }
        
        public string Publisher { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime? DateOfSubmission { get; set; }
        [Required]
        public int Status { get; set; }
        /* [Editable(true)]*/
        
        public string Reason { get; set; }
    }
}
