namespace SMDriveV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomCadastro : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Matricula", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "UrlBehance", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "UrlBehance");
            DropColumn("dbo.AspNetUsers", "Matricula");
        }
    }
}
