namespace RP.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateForeignLanguageLevelsTable : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into ForeignLanguageLevels(Name) values ('pocz�tkuj�cy')");
            Sql("Insert into ForeignLanguageLevels(Name) values ('�redniozaawansowany')");
            Sql("Insert into ForeignLanguageLevels(Name) values ('zaawansowany')");
        }

        public override void Down()
        {
            Sql("Delete from ForeignLanguageLevels where Name='pocz�tkuj�cy'");
            Sql("Delete from ForeignLanguageLevels where Name='�redniozaawansowany'");
            Sql("Delete from ForeignLanguageLevels where Name='zaawansowany'");
        }
    }
}
