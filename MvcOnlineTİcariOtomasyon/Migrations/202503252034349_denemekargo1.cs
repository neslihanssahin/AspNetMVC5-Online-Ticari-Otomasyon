﻿namespace MvcOnlineTİcariOtomasyon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class denemekargo1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.KargoDetays",
                c => new
                    {
                        KargoDetayidy = c.Int(nullable: false, identity: true),
                        Aciklama = c.String(maxLength: 300, unicode: false),
                        TakipKodu = c.String(maxLength: 10, unicode: false),
                        Personel = c.String(maxLength: 20, unicode: false),
                        Alici = c.String(maxLength: 25, unicode: false),
                        Tarih = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.KargoDetayidy);
            
            CreateTable(
                "dbo.KargoTakips",
                c => new
                    {
                        KargoTakipid = c.Int(nullable: false, identity: true),
                        TakipKodu = c.String(maxLength: 10, unicode: false),
                        Aciklama = c.String(maxLength: 100, unicode: false),
                        TarihZaman = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.KargoTakipid);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.KargoTakips");
            DropTable("dbo.KargoDetays");
        }
    }
}
