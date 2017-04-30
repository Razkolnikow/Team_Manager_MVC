namespace Team_Manager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class taskFixes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TeamTasks", "Team_Id", c => c.Int());
            CreateIndex("dbo.TeamTasks", "Team_Id");
            AddForeignKey("dbo.TeamTasks", "Team_Id", "dbo.Teams", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeamTasks", "Team_Id", "dbo.Teams");
            DropIndex("dbo.TeamTasks", new[] { "Team_Id" });
            DropColumn("dbo.TeamTasks", "Team_Id");
        }
    }
}
