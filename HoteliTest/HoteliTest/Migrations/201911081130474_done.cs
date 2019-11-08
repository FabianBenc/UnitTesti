namespace HoteliTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class done : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Gost", "Ime", c => c.String(maxLength: 60));
            AlterColumn("dbo.Gost", "Prezime", c => c.String(maxLength: 60));
            AlterColumn("dbo.Gost", "Email", c => c.String(maxLength: 60));
            AlterColumn("dbo.Gost", "Adresa", c => c.String(maxLength: 60));
            AlterColumn("dbo.Usluga", "ImeUsluge", c => c.String(maxLength: 60));
            AlterColumn("dbo.Hotel", "Ime", c => c.String(maxLength: 60));
            AlterColumn("dbo.Hotel", "Adresa", c => c.String(maxLength: 60));
            AlterColumn("dbo.Hotel", "Lokacija", c => c.String(maxLength: 60));
            AlterColumn("dbo.TipSobe", "OpisSobe", c => c.String(maxLength: 60));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TipSobe", "OpisSobe", c => c.String());
            AlterColumn("dbo.Hotel", "Lokacija", c => c.String());
            AlterColumn("dbo.Hotel", "Adresa", c => c.String());
            AlterColumn("dbo.Hotel", "Ime", c => c.String());
            AlterColumn("dbo.Usluga", "ImeUsluge", c => c.String());
            AlterColumn("dbo.Gost", "Adresa", c => c.String());
            AlterColumn("dbo.Gost", "Email", c => c.String());
            AlterColumn("dbo.Gost", "Prezime", c => c.String());
            AlterColumn("dbo.Gost", "Ime", c => c.String());
        }
    }
}
