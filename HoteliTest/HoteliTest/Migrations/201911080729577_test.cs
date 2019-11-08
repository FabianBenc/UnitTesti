namespace HoteliTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Rezervacija", "Prijava");
            DropColumn("dbo.Rezervacija", "Odjava");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Rezervacija", "Odjava", c => c.DateTime(nullable: false));
            AddColumn("dbo.Rezervacija", "Prijava", c => c.DateTime(nullable: false));
        }
    }
}
