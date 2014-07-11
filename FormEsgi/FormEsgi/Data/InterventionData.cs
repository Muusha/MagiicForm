using FormEsgi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FormEsgi.Data
{
    public class InterventionData
    {

        /// <summary>
        /// Ajoute une nouvelle intervention pour un formulaire
        /// </summary>
        /// <param name="intervention"></param>
        /// <returns></returns>
        public static Intervention createIntervention(Intervention intervention)
        {
            using (FormulaireCtx ctx = new FormulaireCtx())
            {
                ctx.Interventions.Add(intervention);
                ctx.SaveChanges();

                return intervention;
            }
        }

        /// <summary>
        /// Modifier une intervention pour un formulaire
        /// </summary>
        /// <param name="intervention"></param>
        /// <returns></returns>
        public static Intervention editIntervention(Intervention intervention)
        {
            using (FormulaireCtx ctx = new FormulaireCtx())
            {
                Intervention editIntervention = ctx.Interventions.First(i => i.InterventionId == intervention.InterventionId);
                editIntervention = intervention;
                ctx.SaveChanges();

                return editIntervention;
            }
        }

        /// <summary>
        /// Supprimer une intervention
        /// </summary>
        /// <param name="intervention"></param>
        public static void deleteIntervention(Intervention intervention)
        {
            using (FormulaireCtx ctx = new FormulaireCtx())
            {
                ctx.Interventions.Remove(intervention);
                ctx.SaveChanges();
            }
        }

        public static void deleteInterventionByForm(Form form)
        {
            using (FormulaireCtx ctx = new FormulaireCtx())
            {
                List<Intervention> interventions = ctx.Interventions.Where(i => i.FormId == form.FormId).ToList();
                foreach (var intervention in interventions)
	            {
                    ctx.Interventions.Remove(intervention);
                    ctx.SaveChanges();
	            }
            }
        }

        /// <summary>
        /// Récupérer toutes les interventions
        /// </summary>
        /// <returns></returns>
        public static List<Intervention> allInterventions()
        {
            using (FormulaireCtx ctx = new FormulaireCtx())
            {
                List<Intervention> interventions = ctx.Interventions.OrderByDescending(i => i.date_intervention).ToList();

                return interventions;
            }
        }

        /// <summary>
        /// Récupérer les interventions par formulaire
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public static List<Intervention> getInterventionsByForm(Form form)
        {
            using (FormulaireCtx ctx = new FormulaireCtx())
            {
                List<Intervention> interventions = ctx.Interventions.Where(i => i.FormId == form.FormId).ToList();

                return interventions;
            }
        }

        /// <summary>
        /// Récupérer les interventions par utilisateur
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static List<Intervention> getInterventionsByUser(User user)
        {
            using (FormulaireCtx ctx = new FormulaireCtx())
            {
                List<Intervention> interventions = ctx.Interventions.Where(i => i.UserId == user.UserId).ToList();

                return interventions;
            }
        }

        /// <summary>
        /// Retourne le nombre d'interventions pour un formulaire
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public static int getNumberInterventionByForm(Form form)
        {
            using (FormulaireCtx ctx = new FormulaireCtx())
            {
                return ctx.Interventions.Where(i => i.FormId == form.FormId).Count();
            }
        }
    }
}