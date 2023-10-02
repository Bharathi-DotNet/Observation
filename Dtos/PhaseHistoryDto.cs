
using System.ComponentModel.DataAnnotations;

namespace Enquiry.Web.Dtos
{
    public class PhaseHistoryDto
    {
        public string EmpName { get; set; }
        public string Comments { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? CreatedDate { get; set; }

    }
}
