using Owin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Talent_Crowd.Models
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

        }
    }
    public class JobRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(500)]
        public string Job_Title { get; set; }
        [MaxLength(2000)]
        public string Job_Title_Synonyms { get; set; }
        [MaxLength(100)]
        public string Client { get; set; }
        [MaxLength(100)]
        public string Contact_Name { get; set; }
        public DateTime Job_Date { get; set; }
        [MaxLength(2000)]
        public string Locations { get; set; }
        public int Maximum_Commuting_Distance { get; set; }
        [MaxLength(8000)]
        public string Job_Description { get; set; }
        [MaxLength(500)]
        public string Google_Doc_Specs { get; set; }
        public virtual ICollection<JobRequestSkill> JobRequestSkill { get; set; }
    }

    public class JobRequestSkill
    {
        [Key]
        public int Id { get; set; }
        public virtual JobRequest JobRequest { get; set; }
        [MaxLength(200)]
        public string Skill { get; set; }
        [MaxLength(1)]
        public string Type_Skill { get; set; }

    }

    public class Candidate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column(Order = 0), ForeignKey("WorkerJobRequest")]
        public int WorkerJobRequest_Worker_Id { get; set; }
        [Column(Order = 1), ForeignKey("WorkerJobRequest")]
        public int WorkerJobRequest_JobRequest_Id { get; set; }        
        public virtual WorkerJobRequest WorkerJobRequest { get; set; }
        [Required(ErrorMessage = "First Name is required")]
        [MaxLength(100)]
        public string First_Name { get; set; }
        [Required(ErrorMessage = "Surname is required")]
        [MaxLength(100)]
        public string Sur_Name { get; set; }
        [Required(ErrorMessage = "Location is required")]
        [MaxLength(2000)]
        public string Location { get; set; }
        [MaxLength(2000)]
        public string Current_Employeer { get; set; }
        [Required(ErrorMessage = "Source Site is required")]
        [MaxLength(2000)]
        public string Source_Site { get; set; }
        [MaxLength(2000)]
        public string Profile_URL { get; set; }
        public DateTime DOB { get; set; }
        [MaxLength(8000)]
        public string Description_of_Current_Work { get; set; }
        public virtual ICollection<CandidateSkill> CandidateSkill { get; set; }
    }

    public class CandidateSkill
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public virtual Candidate Candidate { get; set; }
        [ForeignKey("Candidate")]
        public int Candidate_Id { get; set; }        
        [MaxLength(1)]
        public string Type_Skill { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }
        [MaxLength(1)]
        public string Experience { get; set; }
    }

    public class Worker
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string First_Name { get; set; }
        public string Sur_Name { get; set; }
        public string EmployeeFreelancer { get; set; }
        public string Company { get; set; }
        public string JobTitle { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string LinkedIn_Public_URL { get; set; }
        public string Twitter { get; set; }
        public decimal HourlyRate { get; set; }
        public int AvailableHoursperWeek { get; set; }
        public string NativeLanguage { get; set; }
        public string SpokenEnglishLevel { get; set; }
        public string AreaofExpertise { get; set; }
        public string ProductAccess { get; set; }
        public string OtherProducts { get; set; }
        public string Interest { get; set; }
    }

    public class WorkerJobRequest
    {        
        [Column(Order = 0), Key, ForeignKey("Worker")]
        public int Worker_Id { get; set; }
        [Column(Order = 1), Key, ForeignKey("JobRequest")]
        public int JobRequest_Id { get; set; }        
        public virtual Worker Worker { get; set; }        
        public virtual JobRequest JobRequest { get; set; }
    }


    public class SiteType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Description { get; set; }
    }

    public class Task
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public virtual WorkerJobRequest WorkerJobRequest { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }

    }

    public class Polling
    {        
        [Column(Order = 0), Key, ForeignKey("WorkerJobRequest")]
        public int Worker_Id { get; set; }
        [Column(Order = 1), Key, ForeignKey("WorkerJobRequest")]
        public int JobRequest_Id { get; set; }        
        public virtual WorkerJobRequest WorkerJobRequest { get; set; }
        public virtual ICollection<PollingSite> PollingSite { get; set; }
    }

    public class PollingSite
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column(Order = 0), ForeignKey("Polling")]
        public int Polling_Worker_Id { get; set; }
        [Column(Order = 1), ForeignKey("Polling")]
        public int Polling_JobRequest_Id { get; set; }        
        public virtual Polling Polling { get; set; }
        [MaxLength(8000)]
        public string SiteURL { get; set; }
        [ForeignKey("SiteType")]
        public int SiteType_Id { get; set; }
        public virtual SiteType SiteType { get; set; }
        public int EstimateVolProfiles { get; set; }
        public int EstimateVolProfilesHour { get; set; }
        [MaxLength(1)]
        public string FreeOrPremium { get; set; }
        [MaxLength(8000)]
        public string WhyAppropiate { get; set; }
        [MaxLength(1)]
        public string SiteOrProduct { get; set; }
    }



    public class Profile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(5)]
        public string Code { get; set; }
        [MaxLength(100)]
        public string Description { get; set; }
    }
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(100)]
        public string UserName { get; set; }
        [MaxLength(20)]
        public string Password { get; set; }
        public string ProfileId { get; set; }
    }

}