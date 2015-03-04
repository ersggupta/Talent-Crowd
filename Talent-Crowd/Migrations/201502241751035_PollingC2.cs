namespace Talent_Crowd.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PollingC2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.PollingSites", name: "Worker_Id", newName: "Polling_Worker_Id");
            RenameColumn(table: "dbo.PollingSites", name: "JobRequest_Id", newName: "Polling_JobRequest_Id");
            RenameIndex(table: "dbo.PollingSites", name: "IX_Worker_Id_JobRequest_Id", newName: "IX_Polling_Worker_Id_Polling_JobRequest_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.PollingSites", name: "IX_Polling_Worker_Id_Polling_JobRequest_Id", newName: "IX_Worker_Id_JobRequest_Id");
            RenameColumn(table: "dbo.PollingSites", name: "Polling_JobRequest_Id", newName: "JobRequest_Id");
            RenameColumn(table: "dbo.PollingSites", name: "Polling_Worker_Id", newName: "Worker_Id");
        }
    }
}
