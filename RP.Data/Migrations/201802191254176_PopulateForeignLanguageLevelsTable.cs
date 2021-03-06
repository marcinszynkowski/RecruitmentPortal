namespace RP.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateForeignLanguageLevelsTable : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into ForeignLanguageLevels(Name) values ('początkujący')");
            Sql("Insert into ForeignLanguageLevels(Name) values ('średniozaawansowany')");
            Sql("Insert into ForeignLanguageLevels(Name) values ('zaawansowany')");
        }

        public override void Down()
        {
            Sql("Delete from ForeignLanguageLevels where Name='początkujący'");
            Sql("Delete from ForeignLanguageLevels where Name='średniozaawansowany'");
            Sql("Delete from ForeignLanguageLevels where Name='zaawansowany'");
        }
    }
}
