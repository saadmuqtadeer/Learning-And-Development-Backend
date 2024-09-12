using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthAPI.Models
{
    public class TrainingRequest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string RequestorName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string RequestorEmail { get; set; }

        [StringLength(50)]
        public string Department { get; set; }

        [StringLength(100)]
        public string TrainingTitle { get; set; }

        [StringLength(500)]
        public string TrainingDescription { get; set; }

        public int NumberOfEmployees { get; set; }

        [StringLength(200)]
        public string TechnicalSkills { get; set; }

        public int Duration { get; set; }

        public DateTime PreferredStartDate { get; set; }

        [StringLength(200)]
        public string TrainingLocation { get; set; }

        [StringLength(500)]
        public string SpecialRequirements { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual Register Registers { get; set; }

        [Required]
        public RequestStatus Status { get; set; } = RequestStatus.Pending;

        // New optional field for feedback
        [StringLength(1000)]
        public string AdminFeedback { get; set; }
    }

    public enum RequestStatus
    {
        Pending = 0,
        Accepted = 1,
        Rejected = 2
    }
}
