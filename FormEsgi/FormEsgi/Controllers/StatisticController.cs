using FormEsgi.Data;
using FormEsgi.Models;
using FormEsgi.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace FormEsgi.Controllers
{
    public class StatisticController : Controller
    {
        //
        // GET: /Statistic/

        public ActionResult Index()
        {

            return View();
        }

        //
        // GET: /Statistic/Details/5

        public ActionResult Statistics(int id)
        {

            statisticById(id);
            
            return View();
        }

        /// <summary>
        /// Récupère les graphiques
        /// </summary>
        /// <param name="id"></param>
        public void statisticById(int id)
        {
            // récupérer le nombre de réponses au formulaire
            Form form = FormData.getForm(id);
            ViewData["form"] = form;
            int numberAnswers = InterventionData.getNumberInterventionByForm(form);
            ViewData["numberAnswers"] = numberAnswers;

            // Créer un dictionnaire qui contiendra la question et les statistiques associées
            List<System.Web.Helpers.Chart> charts = new List<System.Web.Helpers.Chart>();
            // Pour chaque question, récupérer le classement des réponses
            List<Question> questions = FormData.getQuestions(form);
            foreach (var question in questions)
            {

                // Récupérer le couple Clé(réponse de l'utilisateur) valeur(nombre de fois que cette réponse fut choisit)
                Dictionary<string, int> dict = new Dictionary<string, int>();
                dict = AnswerData.getNumberAnswerStatistic(question);

                List<string> answers = new List<string>();
                List<string> count = new List<string>();
                foreach (KeyValuePair<string, int> item in dict)
                {
                    answers.Add(item.Key);
                    count.Add(item.Value.ToString());
                }

                var myChart = new System.Web.Helpers.Chart(width: 900, height: 200)
                            .AddTitle(question.question)
                            .AddSeries(
                                chartType: "pie",
                                name: question.question,
                                xValue: answers,
                                yValues: count);
                charts.Add(myChart);
                
            }
            ViewData["questions"] = questions;
            ViewData["statistics"] = charts;
        }

        /// <summary>
        /// Types de Graphique
        /// </summary>
        enum ChartType { Pie, Column, Bar, Line, Area, stock }

        /// <summary>
        /// retourne en chaine de caractère le type de graphique
        /// </summary>
        /// <param name="chartType"></param>
        /// <returns></returns>
        private static string StrChartType(ChartType chartType)
        {
            string str = "pie";
            switch (chartType)
            {
                case ChartType.Pie: str = "pie";        break;
                case ChartType.Column: str = "column";  break;
                case ChartType.Bar: str = "bar";        break;
                case ChartType.Line: str = "line";      break;
                case ChartType.Area: str = "area";      break;
                case ChartType.stock: str = "stock";    break;
                default:
                    str = "pie";
                    break;
            }

            return str;
        }

        /// <summary>
        /// Récupérer un graphique d'une question
        /// </summary>
        /// <param name="id"></param>
        public ActionResult Chart (int id)
        {
            // récupérer la question
            Question question = FormData.getQuestion(id);
            // Récupérer le couple Clé(réponse de l'utilisateur) valeur(nombre de fois que cette réponse fut choisit)
            Dictionary<string, int> dict = new Dictionary<string, int>();
            dict = AnswerData.getNumberAnswerStatistic(question);
            
            List<string> answers = new List<string>();
            List<string> count = new List<string>();
            foreach (KeyValuePair<string, int> item in dict)
            {
                answers.Add(item.Key);
                count.Add(item.Value.ToString());
            }

            // Récupérer ne chaine de caractère le type de graph utilisé
            string strChartType = StrChartType(ChartType.Pie);

            System.Diagnostics.Debug.WriteLine("Question est : " + question.question);

            // Créer le graphique
            var myChart = new System.Web.Helpers.Chart(width: 900, height: 200)
                        .AddTitle(question.question)
                        .AddSeries(
                            chartType: strChartType,
                            name: question.question,
                            xValue: answers,
                            yValues: count);
            ViewData["chart"] = myChart;

            return View();
        }

        public ActionResult DonwloadChart(int id)
        {
            // récupérer le nombre de réponses au formulaire
            Form form = FormData.getForm(id);
            int numberAnswers = InterventionData.getNumberInterventionByForm(form);

            // récupérer la question
            Question question = FormData.getQuestion(id);
            // Récupérer le couple Clé(réponse de l'utilisateur) valeur(nombre de fois que cette réponse fut choisit)
            Dictionary<string, int> dict = new Dictionary<string, int>();
            dict = AnswerData.getNumberAnswerStatistic(question);

            List<string> answers = new List<string>();
            List<string> count = new List<string>();
            foreach (KeyValuePair<string, int> item in dict)
            {
                answers.Add(item.Key);
                count.Add(item.Value.ToString());
            }

            // Récupérer ne chaine de caractère le type de graph utilisé
            string strChartType = StrChartType(ChartType.Column);

            // Créer le graphique
            var myChart = new System.Web.Helpers.Chart(width: 400, height: 400)
                        .AddTitle(question.question)
                        .AddSeries(
                            chartType: strChartType,
                            name: question.question,
                            xValue: answers,
                            yValues: count)
                            .Write(format: "png");

            myChart.Save("C:\\Projets\\chartImage.jpeg");

            PDFTools.generatePDF(form, dict);

            return null;
        }

        public ActionResult test(int id)
        {

            // récupérer le nombre de réponses au formulaire
            Form form = FormData.getForm(id);
            int numberAnswers = InterventionData.getNumberInterventionByForm(form);

            // Créer un dictionnaire qui contiendra la question et les statistiques associées
            List<System.Web.Helpers.Chart> charts = new List<System.Web.Helpers.Chart>();
            // Pour chaque question, récupérer le classement des réponses
            List<Question> questions = FormData.getQuestions(form);
            foreach (var question in questions)
            {

                // Récupérer le couple Clé(réponse de l'utilisateur) valeur(nombre de fois que cette réponse fut choisit)
                Dictionary<string, int> dict = new Dictionary<string, int>();
                dict = AnswerData.getNumberAnswerStatistic(question);

                List<string> answers = new List<string>();
                List<string> count = new List<string>();
                foreach (KeyValuePair<string, int> item in dict)
                {
                    answers.Add(item.Key);
                    count.Add(item.Value.ToString());
                }

                var myChart = new System.Web.Helpers.Chart(width: 900, height: 200)
                            .AddTitle(question.question)
                            .AddSeries(
                                chartType: "pie",
                                name: question.question,
                                xValue: answers,
                                yValues: count);
                charts.Add(myChart);

            }

            return null;
        }

    }
}
