using FormEsgi.Data;
using FormEsgi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using System.Web.Security;
using WebMatrix.WebData;

namespace FormEsgi.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult LogOut()
        {

            Session.Remove("userSession");

            return View("Login");
        }

        //
        // POST: /Login/Create

        [HttpPost]
        public ActionResult Connection(User user)
        {
            try
            {
                //   User userLocal = UserData.getByEmail(user.email);
                User userLocal = UserData.getByName(user.name, user.password);
                
                if (userLocal != null)
                {
                    HttpContext.Session.Add("userSession", userLocal);
                    FormsAuthentication.RedirectFromLoginPage(user.name, false);
                    //return RedirectToAction("Welcome", "WelcomeUser");
                }

                return View("Login");
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception Login Controller " + e);
                return View("Login");
            }
        }

        
    }
}
