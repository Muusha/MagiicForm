namespace FormEsgi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDateClosing : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Forms", "date_closing", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Forms", "date_closing");
        }
    }
}
