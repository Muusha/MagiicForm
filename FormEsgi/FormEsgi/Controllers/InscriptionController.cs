using FormEsgi.Data;
using FormEsgi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FormEsgi.Controllers
{
    public class InscriptionController : Controller
    {
        //
        // GET: /Inscription/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Inscription/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Inscription/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Inscription/Create

        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {       
                try
                {
                    user.date_registration = DateTime.Now;
                    user.TypeUserId = 2;
                    User userLocal = UserData.create(user);

                    System.Diagnostics.Trace.WriteLine("Name : " + user.name);

                    return RedirectToAction("Index", "Home");
                }
                catch (Exception e)
                {
                    System.Diagnostics.Trace.WriteLine("exception  : " + e);
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("message", "Salut !! Comment ça va ?");
                return View("Index");
            }

        }

        //
        // GET: /Inscription/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Inscription/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Inscription/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Inscription/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
