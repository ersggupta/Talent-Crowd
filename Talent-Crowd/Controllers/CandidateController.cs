using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Talent_Crowd.Models;

namespace Talent_Crowd.Controllers
{
    public class CandidateController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: Candidate
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Save(Candidate candidate)
        {
            db.Candidates.Add(candidate);

            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
                {
                    // Get entry

                    DbEntityEntry entry = item.Entry;
                    string entityTypeName = entry.Entity.GetType().Name;

                    // Display or log error messages

                    foreach (DbValidationError subItem in item.ValidationErrors)
                    {
                        string message = string.Format("Error '{0}' occurred in {1} at {2}",subItem.ErrorMessage, entityTypeName, subItem.PropertyName);
                        return Json(new { success = false, responseText = message }, JsonRequestBehavior.AllowGet);           
                    }
                }
            }

            return Json(new { success = true, responseText = candidate.Id.ToString() }, JsonRequestBehavior.AllowGet);           
        }
    }
}