using FormEsgi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FormEsgi.Data
{
    public class ParseJsonForm
    {
        public static void parseForm (string form, string quest, User user)
        {
            try
            {

                dynamic stuffForm = JsonConvert.DeserializeObject(form);

                // Récupérer les informations du formulaire.
                Form formObject = new Form();
                formObject.title = stuffForm.title;
                formObject.description = stuffForm.description;
                formObject.is_active = true;
                formObject.UserId = user.UserId;
                formObject.date_creation = DateTime.Now;
                string strDte = stuffForm.dateCloture;
                formObject.date_closing = Convert.ToDateTime(strDte);
                
                // Ajouter les données générales du formulaire en BDD
                formObject = FormData.create(formObject);


                // Récupérer les Questions et Réponses
                dynamic stuffQuestAnswers = JsonConvert.DeserializeObject(quest);
                foreach (var questionItem in stuffQuestAnswers)
                {
                    Question question = new Question();
                    question.question = questionItem.question;
                    question.FormId = formObject.FormId;
                   // question.TypeQuestionId = questionItem.typeQuestion;
                    question.TypeQuestionId = 4;

                    // Créer la question en BDD
                    question = FormData.createQuestion(question);

                    foreach (var item in questionItem.answers)
                    {
                        FixedAnswer answer = new FixedAnswer();
                        answer.fixed_answer = item.answer;
                        answer.QuestionId = question.QuestionId;

                        // Enregistrer les Réponses fixes en BDD
                        answer = FormData.createFixedAnswer(answer);

                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception parsing json form " + e);
            }
        }

        public static void parseFormEdit(string form, string quest, User user)
        {
            try
            {
                dynamic stuffForm = JsonConvert.DeserializeObject(form);

                // Récupérer les informations du formulaire.
                Form formObject = new Form();
                formObject.title = stuffForm.title;
                formObject.description = stuffForm.description;
                formObject.is_active = true;
                formObject.UserId = user.UserId;
                formObject.date_creation = DateTime.Now;
                string strDte = stuffForm.dateCloture;
                formObject.date_closing = Convert.ToDateTime(strDte);
                formObject.FormId = stuffForm.FormId;

                // Ajouter les données générales du formulaire en BDD
                Form myForm = FormData.edit(formObject);

                // Créer une liste d'entier qui contiendra les id question du formulaire modifié
                // permettant ainsi de supprimer toutes les questions qui ne sont plus dans le formulaire
                List<int> idFormEdit = new List<int>();

                // Liste qui contiendra les id réponses fixes du formulaire modifié
                // permettant ainsi de supprimer toutes les réponses fixes qui ne sont plus dans le formulaire
                
                
                // Récupérer les Questions et Réponses
                dynamic stuffQuestAnswers = JsonConvert.DeserializeObject(quest);
                foreach (var questionItem in stuffQuestAnswers)
                {
                    Question question = new Question();
                    question.question = questionItem.question;
                    question.FormId = myForm.FormId;
                    question.TypeQuestionId = questionItem.type;
                    question.QuestionId = questionItem.id;
                    // Ajouter l'id question à la liste
                    idFormEdit.Add(question.QuestionId);

                    // Vérifier si la question existe déjà
                    // si elle existe modifier la question sinon la crée
                    if (FormData.isQuestionExist(question.QuestionId, myForm.FormId))
                    {
                        // Modifier la question en BDD
                        question = FormData.editQuestion(question);
                    }
                    else
                    {
                        // Créer la question en BDD
                        question = FormData.createQuestion(question);
                    }

                    List<int> idAnswerFormEdit = new List<int>();
                    foreach (var item in questionItem.answers)
                    {
                        FixedAnswer answer = new FixedAnswer();
                        answer.fixed_answer = item.answer;
                        answer.QuestionId = question.QuestionId;
                        answer.FixedAnswerId = item.id;
                        // Ajouter l'id réponse fixe à la liste
                        
                        idAnswerFormEdit.Add(answer.FixedAnswerId);

                        if (FormData.isFixedAnswerExist(answer.FixedAnswerId))
                        {
                            // Modifier la réponse fixe en BDD
                            FormData.editFixedAnswer(answer);
                        }
                        else
                        {
                            // Enregistrer les Réponses fixes en BDD
                            answer = FormData.createFixedAnswer(answer);
                        }
                    }
                    // supprimer toutes les réponses fixes plus présentes dans une question
                    FormData.deleteFixedAnswerInDb(idAnswerFormEdit, question.QuestionId);
                    // Vider la liste
                   // idAnswerFormEdit.Clear();
                }

                // Supprimer toutes les questions plus présentes dans le formulaire
                FormData.deleteQuestionInDb(idFormEdit, myForm.FormId);

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception parsing json form edit " + e);
            }
        }
    }
}