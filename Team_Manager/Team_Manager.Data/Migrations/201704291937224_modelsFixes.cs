namespace Team_Manager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modelsFixes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Topics", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Topics", "ModifiedOn", c => c.DateTime());
            AddColumn("dbo.Topics", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Topics", "DeletedOn", c => c.DateTime());
            AddColumn("dbo.Comments", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Comments", "ModifiedOn", c => c.DateTime());
            AddColumn("dbo.Comments", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Comments", "DeletedOn", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "DeletedOn");
            DropColumn("dbo.Comments", "IsDeleted");
            DropColumn("dbo.Comments", "ModifiedOn");
            DropColumn("dbo.Comments", "CreatedOn");
            DropColumn("dbo.Topics", "DeletedOn");
            DropColumn("dbo.Topics", "IsDeleted");
            DropColumn("dbo.Topics", "ModifiedOn");
            DropColumn("dbo.Topics", "CreatedOn");
        }
    }
}
