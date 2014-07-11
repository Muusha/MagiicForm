using FormEsgi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FormEsgi.Data
{
    public class AnswerData
    {
        /// <summary>
        /// Ajouter une nouvelle réponse utilisateur
        /// </summary>
        /// <param name="answer"></param>
        /// <returns></returns>
        public static AnswerForm createAnswer(AnswerForm answer)
        {
            using (FormulaireCtx ctx = new FormulaireCtx())
            {
                ctx.AnswerForms.Add(answer);
                ctx.SaveChanges();

                return answer;
            }
        }

        /// <summary>
        /// Modifier une réponse utilisateur
        /// </summary>
        /// <param name="answer"></param>
        /// <returns></returns>
        public static AnswerForm editAnswer(AnswerForm answer)
        {
            using (FormulaireCtx ctx = new FormulaireCtx())
            {
                AnswerForm answerForm = ctx.AnswerForms.First(a => a.AnswerFormId == answer.AnswerFormId);
                answerForm = answer;
                ctx.SaveChanges();

                return answerForm;
            }
        }

        /// <summary>
        /// Supprimer une réponse utilisateur
        /// </summary>
        /// <param name="answer"></param>
        public static void deleteAnswer(AnswerForm answer)
        {
            using (FormulaireCtx ctx = new FormulaireCtx())
            {
                AnswerForm answerForm = ctx.AnswerForms.Remove(answer);
                ctx.SaveChanges();

            }
        }

        /// <summary>
        /// Supprimer toutes les réponses utilisateur liées à une question
        /// </summary>
        /// <param name="question"></param>
        public static void deleteAnswerByQuestion(Question question)
        {
            using (FormulaireCtx ctx = new FormulaireCtx())
            {
                List<AnswerForm> answers = ctx.AnswerForms.Where(a => a.QuestionId == question.QuestionId).ToList();
                foreach (var answer in answers)
                {
                    ctx.AnswerForms.Remove(answer);
                    ctx.SaveChanges();
                }
            }
        }

        public static List<AnswerForm> getAnswerFormByForm(Question question)
        {
            using (FormulaireCtx ctx = new FormulaireCtx())
            {
                List<AnswerForm> allAnswersform = ctx.AnswerForms.Where(a => a.QuestionId == question.QuestionId).ToList();

                return allAnswersform;
            }
        }

        public static Dictionary<string, int> getNumberAnswerStatistic(Question question)
        {
            using (FormulaireCtx ctx = new FormulaireCtx())
            {
                var answers = ctx.AnswerForms
                    .Where(a => a.QuestionId == question.QuestionId)
                    .GroupBy(a => a.answer_form)
                    .Select(g => new { answer_form = g.Key, Count = g.Count() });

                Dictionary<string, int> dict = new Dictionary<string,int>();
                foreach (var item in answers)
	            {
                    System.Diagnostics.Debug.WriteLine("Key " + item.answer_form + " Valeur : "+ item.Count);
                    dict.Add(item.answer_form, item.Count);
	            }

                return dict;
            }
        }
    }
}