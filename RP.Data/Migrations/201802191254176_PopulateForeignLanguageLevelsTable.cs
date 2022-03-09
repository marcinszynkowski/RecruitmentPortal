namespace RP.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateForeignLanguageLevelsTable : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into ForeignLanguageLevels(Name) values ('pocz¹tkuj¹cy')");
            Sql("Insert into ForeignLanguageLevels(Name) values ('œredniozaawansowany')");
            Sql("Insert into ForeignLanguageLevels(Name) values ('zaawansowany')");
        }

        public override void Down()
        {
            Sql("Delete from ForeignLanguageLevels where Name='pocz¹tkuj¹cy'");
            Sql("Delete from ForeignLanguageLevels where Name='œredniozaawansowany'");
            Sql("Delete from ForeignLanguageLevels where Name='zaawansowany'");
        }
    }
}
