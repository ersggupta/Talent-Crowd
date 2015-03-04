namespace Talent_Crowd.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Candidate1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CandidateSkills", "Candidate_Id", "dbo.Candidates");
            DropIndex("dbo.CandidateSkills", new[] { "Candidate_Id" });
            AlterColumn("dbo.CandidateSkills", "Candidate_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.CandidateSkills", "Candidate_Id");
            AddForeignKey("dbo.CandidateSkills", "Candidate_Id", "dbo.Candidates", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CandidateSkills", "Candidate_Id", "dbo.Candidates");
            DropIndex("dbo.CandidateSkills", new[] { "Candidate_Id" });
            AlterColumn("dbo.CandidateSkills", "Candidate_Id", c => c.Int());
            CreateIndex("dbo.CandidateSkills", "Candidate_Id");
            AddForeignKey("dbo.CandidateSkills", "Candidate_Id", "dbo.Candidates", "Id");
        }
    }
}
