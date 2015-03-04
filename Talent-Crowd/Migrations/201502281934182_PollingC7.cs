namespace Talent_Crowd.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PollingC7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PollingSites", "SiteOrProduct", c => c.String(maxLength: 1));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PollingSites", "SiteOrProduct");
        }
    }
}
