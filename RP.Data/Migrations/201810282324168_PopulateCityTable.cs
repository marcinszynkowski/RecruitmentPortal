namespace RP.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateCityTable : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into Cities(Name) values ('Elbl�g')");
            Sql("Insert into Cities(Name) values ('Gda�sk')");
            Sql("Insert into Cities(Name) values ('Gdynia')");
            Sql("Insert into Cities(Name) values ('Gronowo Elbl�skie')");
            Sql("Insert into Cities(Name) values ('Gronowo G�rne')");
            Sql("Insert into Cities(Name) values ('Olsztyn')");
        }
        
        public override void Down()
        {
            Sql("Delete from Cities where Name='Elbl�g'");
            Sql("Delete from Cities where Name='Gda�sk'");
            Sql("Delete from Cities where Name='Gdynia'");
            Sql("Delete from Cities where Name='Gronowo Elbl�skie'");
            Sql("Delete from Cities where Name='Gronowo G�rne'");
            Sql("Delete from Cities where Name='Olsztyn'");
        }
    }
}