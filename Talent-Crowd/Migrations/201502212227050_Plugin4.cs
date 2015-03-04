namespace Talent_Crowd.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Plugin4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Candidates", "JobRequest_Id", "dbo.JobRequests");
            DropIndex("dbo.Candidates", new[] { "JobRequest_Id" });
            AddColumn("dbo.Candidates", "WorkerJobRequest_Worker_Id", c => c.Int());
            AddColumn("dbo.Candidates", "WorkerJobRequest_JobRequest_Id", c => c.Int());
            CreateIndex("dbo.Candidates", new[] { "WorkerJobRequest_Worker_Id", "WorkerJobRequest_JobRequest_Id" });
            AddForeignKey("dbo.Candidates", new[] { "WorkerJobRequest_Worker_Id", "WorkerJobRequest_JobRequest_Id" }, "dbo.WorkerJobRequests", new[] { "Worker_Id", "JobRequest_Id" });
            DropColumn("dbo.Candidates", "JobRequest_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Candidates", "JobRequest_Id", c => c.Int());
            DropForeignKey("dbo.Candidates", new[] { "WorkerJobRequest_Worker_Id", "WorkerJobRequest_JobRequest_Id" }, "dbo.WorkerJobRequests");
            DropIndex("dbo.Candidates", new[] { "WorkerJobRequest_Worker_Id", "WorkerJobRequest_JobRequest_Id" });
            DropColumn("dbo.Candidates", "WorkerJobRequest_JobRequest_Id");
            DropColumn("dbo.Candidates", "WorkerJobRequest_Worker_Id");
            CreateIndex("dbo.Candidates", "JobRequest_Id");
            AddForeignKey("dbo.Candidates", "JobRequest_Id", "dbo.JobRequests", "Id");
        }
    }
}
