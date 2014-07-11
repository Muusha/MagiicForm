using FormEsgi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FormEsgi.Data
{
    public class FormData
    {
        /// <summary>
        /// Créer un formulaire
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public static Form create(Form form)
        {
            using (FormulaireCtx ctx = new FormulaireCtx())
            {
                ctx.Forms.Add(form);
                ctx.SaveChanges();

                return form;
            }
        }

        /// <summary>
        /// Modifier un formulaire
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public static Form edit(Form form)
        {
            using (FormulaireCtx ctx = new FormulaireCtx())
            {
                ctx.Entry(form).State = EntityState.Modified;
                ctx.SaveChanges();

                return form;
            }
        }

        /// <summary>
        /// Supprimer le formulaire
        /// </summary>
        /// <param name="form"></param>
        public static void deleteForm(Form form)
        {
            using (FormulaireCtx ctx = new FormulaireCtx())
            {
                // Supprimer toutes les infos liées à un formulaire
                
                // supprimer les interventions
                InterventionData.deleteInterventionByForm(form);

                // Supprimer les questions (les réponses également incluses dedans)
                FormData.deleteQuestionsForm(form);

                // Supprimer le formulaire
                ctx.Forms.Attach(form);
                ctx.Forms.Remove(form);
                ctx.SaveChanges();
            }
        }

        /// <summary>
        /// Supprimer toutes les questions d'un formulaire
        /// </summary>
        /// <param name="form"></param>
        public static void deleteQuestionsForm(Form form)
        {
            using (FormulaireCtx ctx = new FormulaireCtx())
            {
                List<Question> questions = ctx.Questions.Where(q => q.FormId == form.FormId).ToList();
                foreach (var question in questions)
                {
                    // Supprimer toutes les réponses utilisateur
                    AnswerData.deleteAnswerByQuestion(question);

                    // Supprimer toutes les réponses
                    FormData.deleteFixedAnswer(question);

                    // Supprimer la question
                    ctx.Questions.Remove(question);
                    ctx.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Supprimer une question
        /// </summary>
        /// <param name="question"></param>
        public static void deleteQuestion(Question question)
        {
            using (FormulaireCtx ctx = new FormulaireCtx())
            {
                // Supprimer toutes les réponses utilisateur
                AnswerData.deleteAnswerByQuestion(question);

                // Supprimer toutes les réponses
                FormData.deleteFixedAnswer(question);

                // Supprimer la question
                ctx.Questions.Attach(question);
                ctx.Questions.Remove(question);
                ctx.SaveChanges();
            }
        }

        /// <summary>
        /// Supprimer toutes les réponses fixes du créateur du formulaire
        /// </summary>
        /// <param name="question"></param>
        public static void deleteFixedAnswer(Question question)
        {
            using (FormulaireCtx ctx = new FormulaireCtx())
            {
                List<FixedAnswer> answers = ctx.FixedAnswers.Where(f => f.QuestionId == question.QuestionId).ToList();
                foreach (var answer in answers)
                {
                    ctx.FixedAnswers.Remove(answer);
                    ctx.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Retourne la liste de tous les formulaires
        /// </summary>
        /// <returns></returns>
        public static List<Form> getForms()
        {
            using (FormulaireCtx ctx = new FormulaireCtx())
            {
                List<Form> forms = null;
                forms = ctx.Forms.ToList();

                return forms;
            }
        }

        /// <summary>
        /// Retourne un formulaire
        /// </summary>
        /// <param name="idForm"></param>
        /// <returns></returns>
        public static Form getForm(int idForm)
        {
            using (FormulaireCtx ctx = new FormulaireCtx())
            {
                Form form = ctx.Forms.First(f => f.FormId == idForm);

                return form;
            }
        }

        /// <summary>
        /// Récupère tous les formulaires liés à un utilisateur 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static List<Form> getFormsWithUser(User user)
        {
            using(FormulaireCtx ctx = new FormulaireCtx())
	        {
                return ctx.Forms.Where(f => f.UserId == user.UserId).ToList();
	        }
        }

        /// <summary>
        /// Récupère le nombre de réponses apporté pour un formulaire
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public static int getCountAnswers(Form form)
        {
            using (FormulaireCtx ctx = new FormulaireCtx())
            {
                return ((
                        from o in ctx.Interventions
                        where o.FormId == form.FormId
                        select o
                        )
                        .Count()
                       );
            }
        }

        // Questions et réponses
        // ...

        /// <summary>
        /// Ajouter une question en base de donnée
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        public static Question createQuestion(Question question)
        {
            using (FormulaireCtx ctx = new FormulaireCtx())
            {
                ctx.Questions.Add(question);
                ctx.SaveChanges();

                return question;
            }
        }

        /// <summary>
        /// Modifier une question
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        public static Question editQuestion(Question question)
        {
            using (FormulaireCtx ctx = new FormulaireCtx())
            {
                ctx.Entry(question).State = EntityState.Modified;
                ctx.SaveChanges();

                return question;
            }
        }

        /// <summary>
        /// Récupère toutes les questions d'un formulaire
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public static List<Question> getQuestions(Form form)
        {
            using (FormulaireCtx ctx = new FormulaireCtx())
            {
              //  List<Question> questions = (List<Question>)ctx.Questions.ToList().Where(q => q.FormId == form.FormId);
                List<Question> questions = ctx.Questions.Where(q => q.FormId == form.FormId).ToList();

                return questions;
            }
        }

        /// <summary>
        /// Récupère une question
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Question getQuestion(int id)
        {
            using (FormulaireCtx ctx = new FormulaireCtx())
            {
                return ctx.Questions.First(q => q.QuestionId == id);
            }
        }

        /// <summary>
        /// Vérifier si une question existe en base de donnée
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool isQuestionExist(int id, int idForm)
        {
            using (FormulaireCtx ctx = new FormulaireCtx())
            {
                List<Question> questions = ctx.Questions.Where(q => q.FormId == idForm).ToList();

                if (questions.Any(q => q.QuestionId == id))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Supprime toutes les questions qui n'existent plus dans le formulaire modifié
        /// </summary>
        /// <param name="idFormEdit"></param>
        /// <param name="idForm"></param>
        public static void deleteQuestionInDb(List<int> idFormEdit, int idForm)
        {
            using (FormulaireCtx ctx = new FormulaireCtx())
            {
                List<Question> questions = ctx.Questions.Where(q => q.FormId == idForm).ToList();
                foreach (var question in questions)
                {
                    if (idFormEdit.IndexOf(question.QuestionId) == -1)
                    {
                        // la question n'existe plus dans le formulaire modifié, donc supprimer la question
                        FormData.deleteQuestion(question);
                    }
                }
            }
        }

        /// <summary>
        /// Supprimer une réponse fixe qui ne sont plus présentes dans une question dans le formulaire modifié
        /// </summary>
        /// <param name="idFormEdit"></param>
        /// <param name="idQuestion"></param>
        public static void deleteFixedAnswerInDb(List<int> idFormEdit, int idQuestion)
        {
            using (FormulaireCtx ctx = new FormulaireCtx())
            {
                List<FixedAnswer> answers = ctx.FixedAnswers.Where(f => f.QuestionId == idQuestion).ToList();
                foreach (var answer in answers)
                {
                    if (idFormEdit.IndexOf(answer.FixedAnswerId) == -1)
                    {
                        // la réponse fixe n'existe plus dans le formulaire modifié, donc supprimer la réponse
                        ctx.FixedAnswers.Attach(answer);
                        ctx.FixedAnswers.Remove(answer);
                        ctx.SaveChanges();
                    }
                }
            }
        }

        /// <summary>
        /// Vérifier si une question fixe existe
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool isFixedAnswerExist(int id)
        {
            using (FormulaireCtx ctx = new FormulaireCtx())
            {
                
                if (ctx.FixedAnswers.Any(f => f.FixedAnswerId == id))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Ajoute une réponse prédéfinie pour une question
        /// </summary>
        /// <param name="fixedAnswer"></param>
        /// <returns></returns>
        public static FixedAnswer createFixedAnswer(FixedAnswer fixedAnswer)
        {
            using (FormulaireCtx ctx = new FormulaireCtx())
            {
                FixedAnswer answer = ctx.FixedAnswers.Add(fixedAnswer);
                ctx.SaveChanges();

                return answer;
            }
        }

        /// <summary>
        /// Modifier une réponse prédéfinie
        /// </summary>
        /// <param name="fixedAnswer"></param>
        /// <returns></returns>
        public static FixedAnswer editFixedAnswer(FixedAnswer fixedAnswer)
        {
            using (FormulaireCtx ctx = new FormulaireCtx())
            {
                ctx.Entry(fixedAnswer).State = EntityState.Modified;
                ctx.SaveChanges();

                return fixedAnswer;
            }
        }

        /// <summary>
        /// Retourne la liste des réponses pour chaque question
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        public static List<FixedAnswer> getFixedAnswers(Question question)
        {
            using (FormulaireCtx ctx = new FormulaireCtx())
            {
                List<FixedAnswer> answers = ctx.FixedAnswers.Where(f => f.QuestionId == question.QuestionId).ToList();

                return answers;
            }
        }
    }
}