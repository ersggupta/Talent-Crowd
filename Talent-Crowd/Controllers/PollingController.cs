using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Talent_Crowd.Models;

namespace Talent_Crowd.Controllers
{
    public class PollingController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: Polling
        public ActionResult Index()
        {
            return RedirectToAction("Form");
        }

        [HttpPost]
        public ActionResult Save(Polling model)
        {
            using (var db = new MyDbContext())
            {
                Polling existe = db.Pollings.FirstOrDefault(m => m.JobRequest_Id == model.JobRequest_Id && m.Worker_Id == model.Worker_Id);
                //var existe = db.Pollings.Find(model.JobRequest_Id, model.Worker_Id);

                if (existe == null) {
                    db.Pollings.Add(model);
                }
                else {
                    db.Entry(existe).CurrentValues.SetValues(model);
                }

                foreach (PollingSite site in model.PollingSite)
                {
                    PollingSite existedet = db.PollingSites.FirstOrDefault(m => m.Id == site.Id );
                    //var existedet = db.PollingSites.Find(site.Id);
                    if (Uri.IsWellFormedUriString(site.SiteURL, UriKind.Absolute))
                    {
                        site.SiteOrProduct = "S";
                    }
                    else
                    {
                        site.SiteOrProduct = "P";
                    }

                    if (existedet == null)
                    {
                        site.Polling = model;
                        site.Polling_JobRequest_Id = model.JobRequest_Id;
                        site.Polling_Worker_Id = model.Worker_Id;
                        db.PollingSites.Add(site);
                    }
                    else
                    {
                        db.Entry(existedet).CurrentValues.SetValues(site);
                    }
                }

                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, ex = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { success = true, responseText = "" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Form(int JobRequest_Id, int Worker_Id)
        {
            if (Session["UserName"] == null) return RedirectToAction("Login", "Login", new { requestURL = Request.Url.OriginalString });

            Polling model = new Polling();

            var existe = from a in db.Pollings
                         where (a.JobRequest_Id == JobRequest_Id && a.Worker_Id == Worker_Id)
                         select a;

            if (existe.ToList().Count > 0)
            {
                model = existe.ToList().First();
            }
            else
            {
                var records = from a in db.WorkerJobRequests
                              where (a.JobRequest_Id == JobRequest_Id && a.Worker_Id == Worker_Id)
                              select a;
                model.WorkerJobRequest = records.ToList().First();
                model.Worker_Id = Worker_Id;
                model.JobRequest_Id = JobRequest_Id;

                var Sites = from a in db.PollingSites
                            where (a.Polling_JobRequest_Id == JobRequest_Id && a.Polling_Worker_Id == Worker_Id)
                            orderby a.SiteOrProduct descending
                            select a;
                model.PollingSite = Sites.ToList();
            }

            return View(model);
        }


        public ActionResult SiteTypeCombo()
        {
            using (var db = new MyDbContext())
            {
                var records = from a in db.SiteTypes
                              select a;

                var serializer = new JavaScriptSerializer();
                string jsonData = serializer.Serialize(records);

                return Json(new { success = true, responseText = jsonData }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}