namespace RP.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateForeignLanguagesTable : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into ForeignLanguages(Name) values ('angielski')");
            Sql("Insert into ForeignLanguages(Name) values ('hiszpañski')");
            Sql("Insert into ForeignLanguages(Name) values ('francuski')");
            Sql("Insert into ForeignLanguages(Name) values ('rosyjski')");
            Sql("Insert into ForeignLanguages(Name) values ('portugalski')");
            Sql("Insert into ForeignLanguages(Name) values ('niemiecki')");
            Sql("Insert into ForeignLanguages(Name) values ('w³oski')");
            Sql("Insert into ForeignLanguages(Name) values ('norweski')");
            Sql("Insert into ForeignLanguages(Name) values ('chiñski')");
            Sql("Insert into ForeignLanguages(Name) values ('japoñski')");
        }

        public override void Down()
        {
            Sql("Delete from ForeignLanguages where Name='angielski'");
            Sql("Delete from ForeignLanguages where Name='hiszpañski'");
            Sql("Delete from ForeignLanguages where Name='francuski'");
            Sql("Delete from ForeignLanguages where Name='rosyjski'");
            Sql("Delete from ForeignLanguages where Name='portugalski'");
            Sql("Delete from ForeignLanguages where Name='niemiecki'");
            Sql("Delete from ForeignLanguages where Name='w³oski'");
            Sql("Delete from ForeignLanguages where Name='norweski'");
            Sql("Delete from ForeignLanguages where Name='chiñski'");
            Sql("Delete from ForeignLanguages where Name='japoñski'");
        }
    }
}
