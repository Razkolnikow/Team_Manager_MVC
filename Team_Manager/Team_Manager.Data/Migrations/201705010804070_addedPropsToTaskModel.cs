namespace Team_Manager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedPropsToTaskModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TeamTasks", "IsAccepted", c => c.Boolean(nullable: false));
            AddColumn("dbo.TeamTasks", "IsRejected", c => c.Boolean(nullable: false));
            AddColumn("dbo.TeamTasks", "RejectionReason", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TeamTasks", "RejectionReason");
            DropColumn("dbo.TeamTasks", "IsRejected");
            DropColumn("dbo.TeamTasks", "IsAccepted");
        }
    }
}
