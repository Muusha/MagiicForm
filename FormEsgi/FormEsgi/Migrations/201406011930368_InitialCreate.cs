namespace FormEsgi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnswerForms",
                c => new
                    {
                        AnswerFormId = c.Int(nullable: false, identity: true),
                        answer_form = c.String(),
                        QuestionId = c.Int(nullable: false),
                        InterventionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AnswerFormId);
            
            CreateTable(
                "dbo.FixedAnswers",
                c => new
                    {
                        FixedAnswerId = c.Int(nullable: false, identity: true),
                        fixed_answer = c.String(),
                        QuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FixedAnswerId);
            
            CreateTable(
                "dbo.Forms",
                c => new
                    {
                        FormId = c.Int(nullable: false, identity: true),
                        title = c.String(),
                        description = c.String(),
                        date_creation = c.DateTime(nullable: false),
                        is_active = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FormId);
            
            CreateTable(
                "dbo.Interventions",
                c => new
                    {
                        InterventionId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        FormId = c.Int(nullable: false),
                        date_intervention = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.InterventionId);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        QuestionId = c.Int(nullable: false, identity: true),
                        question = c.String(),
                        TypeQuestionId = c.Int(nullable: false),
                        FormId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.QuestionId);
            
            CreateTable(
                "dbo.TypeQuestions",
                c => new
                    {
                        TypeQuestionId = c.Int(nullable: false, identity: true),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.TypeQuestionId);
            
            CreateTable(
                "dbo.TypeUsers",
                c => new
                    {
                        TypeUserId = c.Int(nullable: false, identity: true),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.TypeUserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        email = c.String(nullable: false),
                        password = c.String(),
                        date_registration = c.DateTime(nullable: false),
                        TypeUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.TypeUsers");
            DropTable("dbo.TypeQuestions");
            DropTable("dbo.Questions");
            DropTable("dbo.Interventions");
            DropTable("dbo.Forms");
            DropTable("dbo.FixedAnswers");
            DropTable("dbo.AnswerForms");
        }
    }
}
