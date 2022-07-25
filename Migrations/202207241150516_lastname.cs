namespace MySchoolProj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lastname : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "lastName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "lastName");
        }
    }
}
