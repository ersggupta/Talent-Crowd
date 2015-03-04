namespace Talent_Crowd.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Plugin3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Candidates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        First_Name = c.String(maxLength: 100),
                        Sur_Name = c.String(maxLength: 100),
                        Location = c.String(maxLength: 2000),
                        Current_Employeer = c.String(maxLength: 2000),
                        Source_Site = c.String(maxLength: 2000),
                        Profile_URL = c.String(maxLength: 2000),
                        DOB = c.DateTime(nullable: false),
                        Description_of_Current_Work = c.String(),
                        JobRequest_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.JobRequests", t => t.JobRequest_Id)
                .Index(t => t.JobRequest_Id);
            
            CreateTable(
                "dbo.CandidateSkills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type_Skill = c.String(maxLength: 1),
                        Description = c.String(maxLength: 200),
                        Experience = c.Int(nullable: false),
                        Candidate_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Candidates", t => t.Candidate_Id)
                .Index(t => t.Candidate_Id);
            
            CreateTable(
                "dbo.JobRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Job_Title = c.String(maxLength: 500),
                        Job_Title_Synonyms = c.String(maxLength: 2000),
                        Client = c.String(maxLength: 100),
                        Contact_Name = c.String(maxLength: 100),
                        Job_Date = c.DateTime(nullable: false),
                        Locations = c.String(maxLength: 2000),
                        Maximum_Commuting_Distance = c.Int(nullable: false),
                        Job_Description = c.String(),
                        Google_Doc_Specs = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.JobRequestSkills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Skill = c.String(maxLength: 200),
                        Type_Skill = c.String(maxLength: 1),
                        JobRequest_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.JobRequests", t => t.JobRequest_Id)
                .Index(t => t.JobRequest_Id);
            
            CreateTable(
                "dbo.Pollings",
                c => new
                    {
                        Worker_Id = c.Int(nullable: false),
                        JobRequest_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Worker_Id, t.JobRequest_Id })
                .ForeignKey("dbo.WorkerJobRequests", t => new { t.Worker_Id, t.JobRequest_Id })
                .Index(t => new { t.Worker_Id, t.JobRequest_Id });
            
            CreateTable(
                "dbo.PollingSites",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SiteURL = c.String(),
                        EstimateVolProfiles = c.Int(nullable: false),
                        EstimateVolProfilesHour = c.Int(nullable: false),
                        FreeOrPremium = c.String(maxLength: 1),
                        WhyAppropiate = c.String(),
                        Polling_Worker_Id = c.Int(),
                        Polling_JobRequest_Id = c.Int(),
                        SiteType_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pollings", t => new { t.Polling_Worker_Id, t.Polling_JobRequest_Id })
                .ForeignKey("dbo.SiteTypes", t => t.SiteType_Id)
                .Index(t => new { t.Polling_Worker_Id, t.Polling_JobRequest_Id })
                .Index(t => t.SiteType_Id);
            
            
            CreateTable(
                "dbo.WorkerJobRequests",
                c => new
                    {
                        Worker_Id = c.Int(nullable: false),
                        JobRequest_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Worker_Id, t.JobRequest_Id })
                .ForeignKey("dbo.JobRequests", t => t.JobRequest_Id, cascadeDelete: true)
                .ForeignKey("dbo.Workers", t => t.Worker_Id, cascadeDelete: true)
                .Index(t => t.Worker_Id)
                .Index(t => t.JobRequest_Id);
            
            
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(maxLength: 200),
                        WorkerJobRequest_Worker_Id = c.Int(),
                        WorkerJobRequest_JobRequest_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WorkerJobRequests", t => new { t.WorkerJobRequest_Worker_Id, t.WorkerJobRequest_JobRequest_Id })
                .Index(t => new { t.WorkerJobRequest_Worker_Id, t.WorkerJobRequest_JobRequest_Id });
            
        }
        
        public override void Down()
        {
        }
    }
}
