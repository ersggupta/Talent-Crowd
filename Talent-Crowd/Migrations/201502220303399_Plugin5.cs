namespace Talent_Crowd.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Plugin5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Candidates", new[] { "WorkerJobRequest_Worker_Id", "WorkerJobRequest_JobRequest_Id" }, "dbo.WorkerJobRequests");
            DropIndex("dbo.Candidates", new[] { "WorkerJobRequest_Worker_Id", "WorkerJobRequest_JobRequest_Id" });
            AlterColumn("dbo.Candidates", "WorkerJobRequest_Worker_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Candidates", "WorkerJobRequest_JobRequest_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Candidates", new[] { "WorkerJobRequest_Worker_Id", "WorkerJobRequest_JobRequest_Id" });
            AddForeignKey("dbo.Candidates", new[] { "WorkerJobRequest_Worker_Id", "WorkerJobRequest_JobRequest_Id" }, "dbo.WorkerJobRequests", new[] { "Worker_Id", "JobRequest_Id" }, cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Candidates", new[] { "WorkerJobRequest_Worker_Id", "WorkerJobRequest_JobRequest_Id" }, "dbo.WorkerJobRequests");
            DropIndex("dbo.Candidates", new[] { "WorkerJobRequest_Worker_Id", "WorkerJobRequest_JobRequest_Id" });
            AlterColumn("dbo.Candidates", "WorkerJobRequest_JobRequest_Id", c => c.Int());
            AlterColumn("dbo.Candidates", "WorkerJobRequest_Worker_Id", c => c.Int());
            CreateIndex("dbo.Candidates", new[] { "WorkerJobRequest_Worker_Id", "WorkerJobRequest_JobRequest_Id" });
            AddForeignKey("dbo.Candidates", new[] { "WorkerJobRequest_Worker_Id", "WorkerJobRequest_JobRequest_Id" }, "dbo.WorkerJobRequests", new[] { "Worker_Id", "JobRequest_Id" });
        }
    }
}
