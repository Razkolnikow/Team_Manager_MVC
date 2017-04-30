namespace Team_Manager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedInvitationModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Invitations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Team_Id = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.Team_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Team_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Invitations", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Invitations", "Team_Id", "dbo.Teams");
            DropIndex("dbo.Invitations", new[] { "User_Id" });
            DropIndex("dbo.Invitations", new[] { "Team_Id" });
            DropTable("dbo.Invitations");
        }
    }
}
