namespace Talent_Crowd.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PollingC6 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Candidates", "First_Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Candidates", "Sur_Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Candidates", "Location", c => c.String(nullable: false, maxLength: 2000));
            AlterColumn("dbo.Candidates", "Source_Site", c => c.String(nullable: false, maxLength: 2000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Candidates", "Source_Site", c => c.String(maxLength: 2000));
            AlterColumn("dbo.Candidates", "Location", c => c.String(maxLength: 2000));
            AlterColumn("dbo.Candidates", "Sur_Name", c => c.String(maxLength: 100));
            AlterColumn("dbo.Candidates", "First_Name", c => c.String(maxLength: 100));
        }
    }
}
