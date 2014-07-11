using FormEsgi.Data;
using FormEsgi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FormEsgi.Controllers
{
    public class AnswerFormController : Controller
    {
        //
        // GET: /AnswerForm/

        public ActionResult listForm()
        {

            // récupère tous les formulaires liés à un utilisateur
            List<Form> forms = null;
            User user = (User)Session["userSession"];
            // forms = FormData.getFormsWithIdUser(user); // Ligne à utiliser
            forms = FormData.getForms();

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
        // GET: /AnswerForm/Details/5

        public ActionResult AnswerForm(int id)
        {
            Form form = FormData.getForm(id);
            List<Question> questions = FormData.getQuestions(form);
            ViewData["form"] = form;
            ViewData["questions"] = questions;

            System.Diagnostics.Debug.WriteLine("Titre du Formulaire : " + form.title);

            List<FixedAnswer> listFixedAsnwers = new List<FixedAnswer>();

            foreach (var item in questions)
            {
                System.Diagnostics.Debug.WriteLine("Question : " + item.question);

                List<FixedAnswer> fixedAnswers = FormData.getFixedAnswers(item);

                foreach (var fixedAnswer in fixedAnswers)
                {
                    System.Diagnostics.Debug.WriteLine("Réponse fixes : " + fixedAnswer);

                    listFixedAsnwers.Add(fixedAnswer);
                }
            }

            ViewData["fixedAnswers"] = listFixedAsnwers;

            return View();
        }

        //
        // POST: /AnswerForm/Create

        [HttpPost]
        public ActionResult AddAnswerForm(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                // Créer un objet Intervention
                Intervention intervention = new Intervention();
                intervention.FormId = int.Parse(collection["formID"]);
                User user = (User)Session["userSession"];
                intervention.UserId = user.UserId;
                intervention.date_intervention = DateTime.Now;
                intervention = InterventionData.createIntervention(intervention);

                foreach (var idQuestion in collection.AllKeys)
                {
                    System.Diagnostics.Debug.WriteLine("Var Item : " + idQuestion);
                    System.Diagnostics.Debug.WriteLine("Item valeur : " + collection[idQuestion]);

                    if (idQuestion != "formID")
                    {
                        
                        // Créer un objet réponse
                        AnswerForm answerForm = new AnswerForm();
                        answerForm.answer_form = collection[idQuestion];
                        answerForm.QuestionId = int.Parse(idQuestion);
                        answerForm.InterventionId = intervention.InterventionId;
                        answerForm = AnswerData.createAnswer(answerForm);
                    }
                    
                }

                return RedirectToAction("listForm");
            }
            catch
            {
                return View("listForm");
            }
        }

        //
        // GET: /AnswerForm/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /AnswerForm/Edit/5

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
        // POST: /AnswerForm/Delete/5

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
