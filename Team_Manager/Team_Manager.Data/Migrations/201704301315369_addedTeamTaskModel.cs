namespace Team_Manager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedTeamTaskModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TeamTasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        FinalDate = c.DateTime(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        TeamMember_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.TeamMember_Id)
                .Index(t => t.TeamMember_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeamTasks", "TeamMember_Id", "dbo.AspNetUsers");
            DropIndex("dbo.TeamTasks", new[] { "TeamMember_Id" });
            DropTable("dbo.TeamTasks");
        }
    }
}
