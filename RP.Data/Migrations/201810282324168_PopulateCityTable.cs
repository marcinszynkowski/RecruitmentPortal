namespace RP.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateCityTable : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into Cities(Name) values ('Elbl¹g')");
            Sql("Insert into Cities(Name) values ('Gdañsk')");
            Sql("Insert into Cities(Name) values ('Gdynia')");
            Sql("Insert into Cities(Name) values ('Gronowo Elbl¹skie')");
            Sql("Insert into Cities(Name) values ('Gronowo Górne')");
            Sql("Insert into Cities(Name) values ('Olsztyn')");
        }
        
        public override void Down()
        {
            Sql("Delete from Cities where Name='Elbl¹g'");
            Sql("Delete from Cities where Name='Gdañsk'");
            Sql("Delete from Cities where Name='Gdynia'");
            Sql("Delete from Cities where Name='Gronowo Elbl¹skie'");
            Sql("Delete from Cities where Name='Gronowo Górne'");
            Sql("Delete from Cities where Name='Olsztyn'");
        }
    }
}