namespace Team_Manager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Category = c.Int(nullable: false),
                        Subcategory = c.String(),
                        Creator_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Creator_Id)
                .Index(t => t.Creator_Id);
            
            CreateTable(
                "dbo.TeamApplicationUsers",
                c => new
                    {
                        Team_Id = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Team_Id, t.ApplicationUser_Id })
                .ForeignKey("dbo.Teams", t => t.Team_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .Index(t => t.Team_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teams", "Creator_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.TeamApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.TeamApplicationUsers", "Team_Id", "dbo.Teams");
            DropIndex("dbo.TeamApplicationUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.TeamApplicationUsers", new[] { "Team_Id" });
            DropIndex("dbo.Teams", new[] { "Creator_Id" });
            DropTable("dbo.TeamApplicationUsers");
            DropTable("dbo.Teams");
        }
    }
}
