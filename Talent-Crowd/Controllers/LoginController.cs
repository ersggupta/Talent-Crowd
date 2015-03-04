using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Talent_Crowd.Models;

namespace Talent_Crowd.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        private MyDbContext db = new MyDbContext();

        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }

        public bool IsAuthenticated()
        {
            if (Session["UserName"] != null)
                return true;

            return false;
        }

        [HttpPost]
        public ActionResult LoginAuthentication(LoginModel model)
        {
            var UserList = from p in db.Users
                           where p.UserName == model.UserName && p.Password == model.Password
                           select p;

            var ultimoUser = UserList.FirstOrDefault();

            if ( ultimoUser != null)
            {
                HttpCookie cookie = new HttpCookie("UserInfo");
                cookie["UserName"] = model.UserName;
                cookie["Password"] = model.Password;
                cookie.Expires = DateTime.Now.AddDays(7);
                Response.Cookies.Add(cookie);

                Session["UserName"] = model.UserName;
                Session["Password"] = model.Password;

                if (model.requestURL != null)
                {
                    return Redirect(model.requestURL);
                }
                else
                {
                    return RedirectToAction("List", "Task");
                }
            }
            else
            {
                ViewBag.Error = "Invalid user or password";
                return View("LoginUser", model);
            }
        }

        public ActionResult Login(string requestURL)
        {
            ViewBag.requestURL = requestURL;
            Session.Clear();
            LoginModel model = new LoginModel();
            HttpCookie cookie = Request.Cookies["UserInfo"];

            if (cookie != null) 
            { 
                model.UserName = cookie["UserName"];
                model.Password = cookie["Password"];
            }
            model.RememberMe = true;
            return View("LoginUser", model);
        }

        public ActionResult LogOut(string errorMessage)
        {
            //If logout should still remember the credentials but disabled autologin
            Session.Clear();
            return RedirectToAction("Login", "Login");
        }


        public ActionResult LoginPlugin(string requestURL)
        {
            ViewBag.requestURL = requestURL;
            Session.Clear();
            LoginModel model = new LoginModel();
            HttpCookie cookie = Request.Cookies["UserInfo"];

            if (cookie != null)
            {
                model.UserName = cookie["UserName"];
                model.Password = cookie["Password"];
            }
            model.RememberMe = true;
            return View("LoginPlugin", model);
        }

        [HttpPost]
        public ActionResult LoginPluginAuthentication(LoginModel model)
        {
            var UserList = from p in db.Users
                           where p.UserName == model.UserName && p.Password == model.Password
                           select p;

            var ultimoUser = UserList.FirstOrDefault();

            if (ultimoUser != null)
            {
                HttpCookie cookie = new HttpCookie("UserInfo");
                cookie["UserName"] = model.UserName;
                cookie["Password"] = model.Password;
                cookie.Expires = DateTime.Now.AddDays(7);
                Response.Cookies.Add(cookie);

                Session["UserName"] = model.UserName;
                Session["Password"] = model.Password;

                if (model.requestURL != null)
                {
                    return Redirect(model.requestURL);
                }
                else
                {
                    return RedirectToAction("Panel", "Plugin");
                }
            }
            else
            {
                ViewBag.Error = "Invalid user or password";
                return View("LoginPlugin", model);
            }
        }

        public ActionResult LogOutPlugin(string errorMessage)
        {
            //If logout should still remember the credentials but disabled autologin
            Session.Clear();
            return RedirectToAction("LoginPlugin", "Login");
        }


    }
}