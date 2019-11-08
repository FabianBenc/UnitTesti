namespace HoteliTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class intial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Gost",
                c => new
                    {
                        GostID = c.Int(nullable: false, identity: true),
                        Ime = c.String(),
                        Prezime = c.String(),
                        Email = c.String(),
                        Adresa = c.String(),
                    })
                .PrimaryKey(t => t.GostID);
            
            CreateTable(
                "dbo.Rezervacija",
                c => new
                    {
                        RezervacijaID = c.Int(nullable: false, identity: true),
                        GostID = c.Int(nullable: false),
                        SobaID = c.Int(nullable: false),
                        Popust = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Rezervirano = c.Boolean(nullable: false),
                        Prijava = c.DateTime(nullable: false),
                        Odjava = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.RezervacijaID)
                .ForeignKey("dbo.Gost", t => t.GostID, cascadeDelete: true)
                .ForeignKey("dbo.Soba", t => t.SobaID, cascadeDelete: true)
                .Index(t => t.GostID)
                .Index(t => t.SobaID);
            
            CreateTable(
                "dbo.Racun",
                c => new
                    {
                        RacunID = c.Int(nullable: false, identity: true),
                        RezervacijaID = c.Int(nullable: false),
                        Placeno = c.Boolean(nullable: false),
                        IznosUkupno = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.RacunID)
                .ForeignKey("dbo.Rezervacija", t => t.RezervacijaID, cascadeDelete: true)
                .Index(t => t.RezervacijaID);
            
            CreateTable(
                "dbo.StavkaRacuna",
                c => new
                    {
                        StavkaRacunaID = c.Int(nullable: false, identity: true),
                        Kolicina = c.Int(nullable: false),
                        RacunID = c.Int(nullable: false),
                        UslugaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StavkaRacunaID)
                .ForeignKey("dbo.Racun", t => t.RacunID, cascadeDelete: true)
                .ForeignKey("dbo.Usluga", t => t.UslugaID, cascadeDelete: true)
                .Index(t => t.RacunID)
                .Index(t => t.UslugaID);
            
            CreateTable(
                "dbo.Usluga",
                c => new
                    {
                        UslugaID = c.Int(nullable: false, identity: true),
                        CijenaUsluge = c.Double(nullable: false),
                        ImeUsluge = c.String(),
                    })
                .PrimaryKey(t => t.UslugaID);
            
            CreateTable(
                "dbo.Soba",
                c => new
                    {
                        SobaID = c.Int(nullable: false, identity: true),
                        HotelID = c.Int(nullable: false),
                        BrojSobe = c.Int(nullable: false),
                        TipSobeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SobaID)
                .ForeignKey("dbo.Hotel", t => t.HotelID, cascadeDelete: true)
                .ForeignKey("dbo.TipSobe", t => t.TipSobeID, cascadeDelete: true)
                .Index(t => t.HotelID)
                .Index(t => t.TipSobeID);
            
            CreateTable(
                "dbo.Hotel",
                c => new
                    {
                        HotelID = c.Int(nullable: false, identity: true),
                        Ime = c.String(),
                        Adresa = c.String(),
                        Lokacija = c.String(),
                    })
                .PrimaryKey(t => t.HotelID);
            
            CreateTable(
                "dbo.TipSobe",
                c => new
                    {
                        TipSobeID = c.Int(nullable: false, identity: true),
                        OpisSobe = c.String(),
                        CijenaPoNoci = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.TipSobeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rezervacija", "SobaID", "dbo.Soba");
            DropForeignKey("dbo.Soba", "TipSobeID", "dbo.TipSobe");
            DropForeignKey("dbo.Soba", "HotelID", "dbo.Hotel");
            DropForeignKey("dbo.StavkaRacuna", "UslugaID", "dbo.Usluga");
            DropForeignKey("dbo.StavkaRacuna", "RacunID", "dbo.Racun");
            DropForeignKey("dbo.Racun", "RezervacijaID", "dbo.Rezervacija");
            DropForeignKey("dbo.Rezervacija", "GostID", "dbo.Gost");
            DropIndex("dbo.Soba", new[] { "TipSobeID" });
            DropIndex("dbo.Soba", new[] { "HotelID" });
            DropIndex("dbo.StavkaRacuna", new[] { "UslugaID" });
            DropIndex("dbo.StavkaRacuna", new[] { "RacunID" });
            DropIndex("dbo.Racun", new[] { "RezervacijaID" });
            DropIndex("dbo.Rezervacija", new[] { "SobaID" });
            DropIndex("dbo.Rezervacija", new[] { "GostID" });
            DropTable("dbo.TipSobe");
            DropTable("dbo.Hotel");
            DropTable("dbo.Soba");
            DropTable("dbo.Usluga");
            DropTable("dbo.StavkaRacuna");
            DropTable("dbo.Racun");
            DropTable("dbo.Rezervacija");
            DropTable("dbo.Gost");
        }
    }
}
