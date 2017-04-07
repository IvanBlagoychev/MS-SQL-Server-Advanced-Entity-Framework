namespace Tasks5to10.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRoles : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Albums", "Owner_Id", "dbo.Photographers");
            DropIndex("dbo.Albums", new[] { "Owner_Id" });
            CreateTable(
                "dbo.PhotographerAlbums",
                c => new
                    {
                        Photographer_Id = c.Int(nullable: false),
                        Album_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Photographer_Id, t.Album_Id })
                .ForeignKey("dbo.Photographers", t => t.Photographer_Id, cascadeDelete: true)
                .ForeignKey("dbo.Albums", t => t.Album_Id, cascadeDelete: true)
                .Index(t => t.Photographer_Id)
                .Index(t => t.Album_Id);
            
            CreateTable(
                "dbo.AlbumPhotographers",
                c => new
                    {
                        Album_Id = c.Int(nullable: false),
                        Photographer_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Album_Id, t.Photographer_Id })
                .ForeignKey("dbo.Albums", t => t.Album_Id, cascadeDelete: true)
                .ForeignKey("dbo.Photographers", t => t.Photographer_Id, cascadeDelete: true)
                .Index(t => t.Album_Id)
                .Index(t => t.Photographer_Id);
            
            DropColumn("dbo.Albums", "Owner_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Albums", "Owner_Id", c => c.Int());
            DropForeignKey("dbo.AlbumPhotographers", "Photographer_Id", "dbo.Photographers");
            DropForeignKey("dbo.AlbumPhotographers", "Album_Id", "dbo.Albums");
            DropForeignKey("dbo.PhotographerAlbums", "Album_Id", "dbo.Albums");
            DropForeignKey("dbo.PhotographerAlbums", "Photographer_Id", "dbo.Photographers");
            DropIndex("dbo.AlbumPhotographers", new[] { "Photographer_Id" });
            DropIndex("dbo.AlbumPhotographers", new[] { "Album_Id" });
            DropIndex("dbo.PhotographerAlbums", new[] { "Album_Id" });
            DropIndex("dbo.PhotographerAlbums", new[] { "Photographer_Id" });
            DropTable("dbo.AlbumPhotographers");
            DropTable("dbo.PhotographerAlbums");
            CreateIndex("dbo.Albums", "Owner_Id");
            AddForeignKey("dbo.Albums", "Owner_Id", "dbo.Photographers", "Id");
        }
    }
}
