using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AuthAPI.Models; // Ensure this namespace is correct for the Register model

namespace ApplicationService.Model
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

        [Required]
        [StringLength(50)]
        public string Department { get; set; }

        [Required]
        [StringLength(500)]
        public string TrainingDetails { get; set; }

        [Required]
        [StringLength(100)]
        public string TrainingTitle { get; set; }

        [StringLength(500)]
        public string TrainingDescription { get; set; }

        [Required]
        public int NumberOfEmployees { get; set; }

        [Required]
        [StringLength(200)]
        public string TechnicalSkillSetRequired { get; set; }

        [Required]
        public int DurationInDays { get; set; }

        [Required]
        public DateTime PreferredStartDate { get; set; }

        [StringLength(200)]
        public string TrainingLocation { get; set; }

        [StringLength(500)]
        public string SpecialRequirements { get; set; }

        [ForeignKey("Register")]
        public int EmployeeId { get; set; } // Foreign key to Register.EmployeeId

        // Navigation property
        public virtual Register Register { get; set; } // Navigation property to Register
    }
}
