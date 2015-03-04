using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Talent_Crowd.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(): base("talentcrowd_db")
        {

        }

        public DbSet<JobRequest> JobRequests { get; set; }
        public DbSet<JobRequestSkill> JobRequestSkills { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<CandidateSkill> CandidateSkills { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<WorkerJobRequest> WorkerJobRequests { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Polling> Pollings { get; set; }
        public DbSet<PollingSite> PollingSites { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<SiteType> SiteTypes { get; set; }
    }
}