namespace RP.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateCourseTypesTable : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into CourseTypes(Name) values ('Kurs')");
            Sql("Insert into CourseTypes(Name) values ('Szkolenie')");
            Sql("Insert into CourseTypes(Name) values ('Uprawnienie')");
        }

        public override void Down()
        {
            Sql("Delete from CourseTypes where Name='Kurs'");
            Sql("Delete from CourseTypes where Name='Szkolenie'");
            Sql("Delete from CourseTypes where Name='Uprawnienie'");
        }
    }
}
