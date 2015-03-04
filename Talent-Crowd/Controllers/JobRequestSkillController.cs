using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Talent_Crowd.Models;

namespace Talent_Crowd.Controllers
{
    public class JobRequestSkillController : Controller
    {
        // GET: JobRequestSkill
        private MyDbContext db = new MyDbContext();

        [HttpPost]
        public ActionResult SkillsbyJob(int jobId, string type)
        {
            var records = from JobRequestSkill in db.JobRequestSkills
                          where (JobRequestSkill.JobRequest.Id == jobId) && (JobRequestSkill.Type_Skill == type)
                          select new
                          {
                              Skill = JobRequestSkill.Skill
                          };

            return Json(new { success = true, responseText = records }, JsonRequestBehavior.AllowGet);           
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}