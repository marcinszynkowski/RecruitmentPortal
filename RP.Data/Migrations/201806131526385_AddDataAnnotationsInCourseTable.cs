namespace RP.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddDataAnnotationsInCourseTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Courses", "DateFrom", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Courses", "DateTo", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Courses", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Courses", "Name", c => c.String());
            AlterColumn("dbo.Courses", "DateTo", c => c.DateTime());
            AlterColumn("dbo.Courses", "DateFrom", c => c.DateTime());
        }
    }
}
