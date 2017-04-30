namespace Team_Manager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifiedInvitations : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invitations", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Invitations", "ModifiedOn", c => c.DateTime());
            AddColumn("dbo.Invitations", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Invitations", "DeletedOn", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invitations", "DeletedOn");
            DropColumn("dbo.Invitations", "IsDeleted");
            DropColumn("dbo.Invitations", "ModifiedOn");
            DropColumn("dbo.Invitations", "CreatedOn");
        }
    }
}
