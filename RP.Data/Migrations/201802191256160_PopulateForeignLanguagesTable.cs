namespace RP.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateForeignLanguagesTable : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into ForeignLanguages(Name) values ('angielski')");
            Sql("Insert into ForeignLanguages(Name) values ('hiszpa�ski')");
            Sql("Insert into ForeignLanguages(Name) values ('francuski')");
            Sql("Insert into ForeignLanguages(Name) values ('rosyjski')");
            Sql("Insert into ForeignLanguages(Name) values ('portugalski')");
            Sql("Insert into ForeignLanguages(Name) values ('niemiecki')");
            Sql("Insert into ForeignLanguages(Name) values ('w�oski')");
            Sql("Insert into ForeignLanguages(Name) values ('norweski')");
            Sql("Insert into ForeignLanguages(Name) values ('chi�ski')");
            Sql("Insert into ForeignLanguages(Name) values ('japo�ski')");
        }

        public override void Down()
        {
            Sql("Delete from ForeignLanguages where Name='angielski'");
            Sql("Delete from ForeignLanguages where Name='hiszpa�ski'");
            Sql("Delete from ForeignLanguages where Name='francuski'");
            Sql("Delete from ForeignLanguages where Name='rosyjski'");
            Sql("Delete from ForeignLanguages where Name='portugalski'");
            Sql("Delete from ForeignLanguages where Name='niemiecki'");
            Sql("Delete from ForeignLanguages where Name='w�oski'");
            Sql("Delete from ForeignLanguages where Name='norweski'");
            Sql("Delete from ForeignLanguages where Name='chi�ski'");
            Sql("Delete from ForeignLanguages where Name='japo�ski'");
        }
    }
}
