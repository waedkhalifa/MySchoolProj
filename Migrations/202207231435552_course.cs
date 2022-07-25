namespace MySchoolProj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class course : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        courseID = c.Int(nullable: false, identity: true),
                        courseName = c.String(maxLength: 100),
                        courseDescription = c.String(),
                        courseLength = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.courseID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Courses");
        }
    }
}
