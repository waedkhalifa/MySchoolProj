namespace MySchoolProj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class courseId2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "UserId", c => c.String(maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "UserId");
        }
    }
}
