namespace Talent_Crowd.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PollingC5 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CandidateSkills", "Experience", c => c.String(maxLength: 1));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CandidateSkills", "Experience", c => c.Int(nullable: false));
        }
    }
}
