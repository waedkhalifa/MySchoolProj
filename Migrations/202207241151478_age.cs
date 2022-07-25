namespace MySchoolProj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class age : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "age", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "age");
        }
    }
}
