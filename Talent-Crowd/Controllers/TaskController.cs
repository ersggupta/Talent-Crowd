using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Talent_Crowd.Models;

namespace Talent_Crowd.Controllers
{
    public class TaskController : Controller
    {

        // GET: Task
        public ActionResult List()
        {
            ViewBag.Title = "Task Management";
            return View();
        }


    }
}