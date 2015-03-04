using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Talent_Crowd.Models;

namespace Talent_Crowd.Controllers
{
    public class JobRequestController : Controller
    {
        // GET: JobRequest
        public MyDbContext db = new MyDbContext();

        [HttpPost]
        public ActionResult JobsbyWorker(int workerId)
        {
            using (var db = new MyDbContext())
            { 
                var records = from a in db.JobRequests
                              let af = db.WorkerJobRequests.Where(x => x.Worker.Id == workerId).Select(x => x.JobRequest.Id)
                              where af.Contains(a.Id)
                              select new
                              {
                                  Id = a.Id,
                                  Job_Title = a.Job_Title
                              };

                return Json(new { success = true, responseText = records }, JsonRequestBehavior.AllowGet);    
            }

        }
        
    }
}