using FormEsgi.Data;
using FormEsgi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace FormEsgi.Controllers
{
    // [Authorize]
    public class FormulaireController : Controller
    {
        //
        // GET: /Formulaire/

        public ActionResult List()
        {
            
            // récupère tous les formulaires liés à un utilisateur
            List<Form> forms = null;
            User user = (User)Session["userSession"];
            forms = FormData.getFormsWithUser(user); // Ligne à utiliser

            // Récupérer le nombre de réponses pour chaque formulaire dans une liste
            List<int> countAnswers = new List<int>();
            foreach (var form in forms)
	        {
                var count = InterventionData.getNumberInterventionByForm(form);
                
                countAnswers.Add(count);
	        }
            
            // Envois les informations à la vue
            ViewData["countAnswers"] = countAnswers;
            ViewData["forms"] = forms;

            return View();
        }

        //
        // GET: /Formulaire/Details/5

        public ActionResult DetailsForm(int id)
        {

            Form form = FormData.getForm(id);
            List<Question> questions = FormData.getQuestions(form);
            ViewData["form"] = form;
            ViewData["questions"] = questions;

            List<FixedAnswer> listFixedAsnwers = new List<FixedAnswer>();

            foreach (var item in questions)
            {
                List<FixedAnswer> fixedAnswers = FormData.getFixedAnswers(item);
                
                foreach (var fixedAnswer in fixedAnswers)
                {
                    listFixedAsnwers.Add(fixedAnswer);
                }
            }

            ViewData["fixedAnswers"] = listFixedAsnwers;

            return View();
        }

        [HttpGet]
        public ActionResult CreateForm()
        {
            try
            {
                return View();
            }
            catch
            {
                return View();
            }
        }

        //
        // POST: /Formulaire/Create

        [HttpPost]
        public ActionResult CreateForm(string form, string quest)
        {
            try
            {
                User user = (User) Session["userSession"];
                ParseJsonForm.parseForm(form, quest, user);
                 
                return RedirectToAction("List");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult EditForm(string form, string quest)
        {
            try
            {
                
                User user = (User)Session["userSession"];
                ParseJsonForm.parseFormEdit(form, quest, user);
                
                return RedirectToAction("List");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Formulaire/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Formulaire/Edit/5

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
        // GET: /Formulaire/Delete/5

        public ActionResult DeleteForm(int id)
        {
            // récupérer le formulaire
            Form form = FormData.getForm(id);
            FormData.deleteForm(form);

            return RedirectToAction("List");
        }

        
    }
}
