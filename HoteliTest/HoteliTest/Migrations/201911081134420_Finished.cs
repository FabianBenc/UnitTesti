namespace HoteliTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Finished : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Gost", "Prezime", c => c.String(nullable: false, maxLength: 60));
            AlterColumn("dbo.Hotel", "Ime", c => c.String(nullable: false, maxLength: 60));
            AlterColumn("dbo.TipSobe", "OpisSobe", c => c.String(nullable: false, maxLength: 60));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TipSobe", "OpisSobe", c => c.String(maxLength: 60));
            AlterColumn("dbo.Hotel", "Ime", c => c.String(maxLength: 60));
            AlterColumn("dbo.Gost", "Prezime", c => c.String(maxLength: 60));
        }
    }
}
