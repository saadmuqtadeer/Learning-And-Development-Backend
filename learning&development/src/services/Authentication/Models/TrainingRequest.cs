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

        //[StringLength(500)]
        //public string TrainingDetails { get; set; }  // Changed from TrainingDetails to match frontend

        [StringLength(100)]
        public string TrainingTitle { get; set; }

        [StringLength(500)]
        public string TrainingDescription { get; set; }

        public int NumberOfEmployees { get; set; }  // Changed to nullable if needed, but frontend shows nulls as numbers

        [StringLength(200)]
        public string TechnicalSkills { get; set; }  // Changed from TechnicalSkillSetRequired to match frontend

        public int Duration { get; set; }  // Changed from DurationInDays to match frontend

        public DateTime PreferredStartDate { get; set; }

        [StringLength(200)]
        public string TrainingLocation { get; set; }

        [StringLength(500)]
        public string SpecialRequirements { get; set; }

        [Required]
        public int EmployeeId { get; set; }  // Foreign key to Registers.EmployeeId

        // Navigation property
        [ForeignKey("EmployeeId")]
        public virtual Register Registers { get; set; }

        // New field
        [Required]
        public RequestStatus Status { get; set; } = RequestStatus.Pending; // Default value set to Pending
    }

    public enum RequestStatus
    {
        Pending = 0,
        Accepted = 1,
        Rejected = 2
    }

}
