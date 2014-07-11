using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FormEsgi.Models
{
    public class FormulaireCtx : DbContext
    {
        public DbSet<User> Users                    { get; set; }
        public DbSet<TypeUser> TypeUsers            { get; set; }
        public DbSet<Form> Forms                    { get; set; }
        public DbSet<Question> Questions            { get; set; }
        public DbSet<TypeQuestion> TypeQuestions    { get; set; }
        public DbSet<AnswerForm> AnswerForms        { get; set; }
        public DbSet<FixedAnswer> FixedAnswers      { get; set; }
        public DbSet<Intervention> Interventions    { get; set; }

        public FormulaireCtx() : base("FormulaireCtx")
        {
            // Cette ligne va supprimer l'ancienne base de donnée et recréer une nouvelle base à chaque démarrage du projet
         //   Database.SetInitializer<FormulaireContext>(new CreateDatabaseIfNotExists<FormulaireContext>());
        }
    
    }
}