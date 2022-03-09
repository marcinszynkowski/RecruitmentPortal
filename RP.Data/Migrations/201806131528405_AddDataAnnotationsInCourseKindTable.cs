namespace RP.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddDataAnnotationsInCourseKindTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CourseKinds", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CourseKinds", "Name", c => c.String());
        }
    }
}
