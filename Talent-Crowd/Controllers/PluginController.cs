using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Talent_Crowd.Models;
using System.Net;

namespace Talent_Crowd.Controllers
{
    public class PluginController : Controller
    {
        // GET: Plugin
        private MyDbContext db = new MyDbContext();

        public ActionResult Download()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TaskCombo(int workerId)
        {
            using (var db = new MyDbContext())
            {
                var records = from a in db.Tasks
                              where (a.WorkerJobRequest.Worker_Id == workerId)
                              select new
                              {
                                  Id = a.Id,
                                  Des = "C" + a.WorkerJobRequest.JobRequest.Id.ToString() + "-" + a.Description,
                                  JobId = a.WorkerJobRequest.JobRequest.Id.ToString(),
                                  JobDes = a.WorkerJobRequest.JobRequest.Job_Title
                              };

                var serializer = new JavaScriptSerializer();
                string jsonData = serializer.Serialize(records);

                return Json(new { success = true, responseText = jsonData }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult SkillCombo(int jobrequestId, string tipo)
        {
            using (var db = new MyDbContext())
            {
                var records = from a in db.JobRequestSkills
                              where (a.JobRequest.Id == jobrequestId && a.Type_Skill == tipo)
                              select new
                              {
                                  Id = a.Id,
                                  Skill = a.Skill,
                                  Type_Skill = a.Type_Skill
                              };

                var serializer = new JavaScriptSerializer();
                string jsonData = serializer.Serialize(records);

                return Json(new { success = true, responseText = jsonData }, JsonRequestBehavior.AllowGet);
            }
        }

        public string ValidatePlugin(Candidate model)
        {
            string validacion = "Please fix the following errors:<br><br>";

            if (model.First_Name == null || model.First_Name.Trim() == "")
                validacion += "- First Name is required<br>";

            if (model.Sur_Name == null || model.Sur_Name.Trim() == "")
                validacion += "- Surname is required<br>";

            if (model.Location == null || model.Location.Trim() == "")
                validacion += "- Location is required<br>";

            if (model.Source_Site == null || model.Source_Site.Trim() == "")
                validacion += "- Source Site is required<br>";

            return validacion;
        }


        public ActionResult Save(Candidate model)
        {
            if (!ModelState.IsValid)
            {
                string validacion = ValidatePlugin(model);
                return Json(new { success = true, responseText = validacion }, JsonRequestBehavior.AllowGet);
            }
                

            try
            {
                db.Candidates.Add(model);

                foreach (CandidateSkill ss in model.CandidateSkill)
                {
                    ss.Candidate_Id = model.Id;
                    db.CandidateSkills.Add(ss);
                }

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new { success = false, ex = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = true, responseText = "" }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Index()
        {
            return RedirectToAction("LoginPlugin", "Login");
        }

        public ActionResult Panel()
        {
            if (Session["UserName"] == null) return RedirectToAction("LoginPlugin", "Login", new { requestURL = Request.Url.OriginalString });

            string username = Session["UserName"].ToString();
            Candidate model = new Candidate();

            var lista = from a in db.Workers
                        where a.Email == username
                        select a;

            var ultimoUser = lista.FirstOrDefault();

            if (ultimoUser != null)
            {
                model.WorkerJobRequest_Worker_Id = ultimoUser.Id;
            }

            return View(model);
        }

    }
}