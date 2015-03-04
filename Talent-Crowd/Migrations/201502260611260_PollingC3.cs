namespace Talent_Crowd.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PollingC3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PollingSites", "SiteType_Id", "dbo.SiteTypes");
            DropIndex("dbo.PollingSites", new[] { "SiteType_Id" });
            AlterColumn("dbo.PollingSites", "SiteType_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.PollingSites", "SiteType_Id");
            AddForeignKey("dbo.PollingSites", "SiteType_Id", "dbo.SiteTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        { 
            DropForeignKey("dbo.PollingSites", "SiteType_Id", "dbo.SiteTypes");
            DropIndex("dbo.PollingSites", new[] { "SiteType_Id" });
            AlterColumn("dbo.PollingSites", "SiteType_Id", c => c.Int());
            CreateIndex("dbo.PollingSites", "SiteType_Id");
            AddForeignKey("dbo.PollingSites", "SiteType_Id", "dbo.SiteTypes", "Id");
        }
    }
}
