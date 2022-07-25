namespace MySchoolProj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class courseId22 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Courses", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Courses", "UserId", c => c.String(maxLength: 128));
        }
    }
}
