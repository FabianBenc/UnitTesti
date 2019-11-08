namespace HoteliTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class backtoold : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rezervacija", "Prijava", c => c.DateTime(nullable: false));
            AddColumn("dbo.Rezervacija", "Odjava", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rezervacija", "Odjava");
            DropColumn("dbo.Rezervacija", "Prijava");
        }
    }
}
