namespace Talent_Crowd.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PollingC : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PollingSites", new[] { "Polling_Worker_Id", "Polling_JobRequest_Id" }, "dbo.Pollings");
            DropIndex("dbo.PollingSites", new[] { "Polling_Worker_Id", "Polling_JobRequest_Id" });
            RenameColumn(table: "dbo.PollingSites", name: "Polling_Worker_Id", newName: "Worker_Id");
            RenameColumn(table: "dbo.PollingSites", name: "Polling_JobRequest_Id", newName: "JobRequest_Id");
            AlterColumn("dbo.PollingSites", "Worker_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.PollingSites", "JobRequest_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.PollingSites", new[] { "Worker_Id", "JobRequest_Id" });
            AddForeignKey("dbo.PollingSites", new[] { "Worker_Id", "JobRequest_Id" }, "dbo.Pollings", new[] { "Worker_Id", "JobRequest_Id" }, cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PollingSites", new[] { "Worker_Id", "JobRequest_Id" }, "dbo.Pollings");
            DropIndex("dbo.PollingSites", new[] { "Worker_Id", "JobRequest_Id" });
            AlterColumn("dbo.PollingSites", "JobRequest_Id", c => c.Int());
            AlterColumn("dbo.PollingSites", "Worker_Id", c => c.Int());
            RenameColumn(table: "dbo.PollingSites", name: "JobRequest_Id", newName: "Polling_JobRequest_Id");
            RenameColumn(table: "dbo.PollingSites", name: "Worker_Id", newName: "Polling_Worker_Id");
            CreateIndex("dbo.PollingSites", new[] { "Polling_Worker_Id", "Polling_JobRequest_Id" });
            AddForeignKey("dbo.PollingSites", new[] { "Polling_Worker_Id", "Polling_JobRequest_Id" }, "dbo.Pollings", new[] { "Worker_Id", "JobRequest_Id" });
        }
    }
}
