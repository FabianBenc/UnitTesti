namespace HoteliTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Rezervacija", "Prijava", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Rezervacija", "Odjava", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Rezervacija", "Odjava", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Rezervacija", "Prijava", c => c.DateTime(nullable: false));
        }
    }
}
