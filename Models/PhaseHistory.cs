﻿using Enquiry.Web.Models.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enquiry.Web.Models
{
    public class PhaseHistory : IAuditableEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HistoryId { get; set; }
        [ForeignKey("Employees")]
        public int EmpId { get; set; }
        [ForeignKey("Phase")]
        public int PhaseId { get; set; }
        public string Comments { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual Employees Employees { get; set; }
        public virtual Phase Phase { get; set; }
    }
}