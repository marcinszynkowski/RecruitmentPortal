namespace RP.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddDataAnnotationsInCourseTypeTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CourseTypes", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CourseTypes", "Name", c => c.String());
        }
    }
}
