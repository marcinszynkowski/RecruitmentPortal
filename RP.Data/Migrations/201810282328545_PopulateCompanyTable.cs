namespace RP.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateCompanyTable : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into Companies(Name) values ('InfoPower')");
            Sql("Insert into Companies(Name) values ('Oke')");
            Sql("Insert into Companies(Name) values ('Blue')");
            Sql("Insert into Companies(Name) values ('Connectis_')");
            Sql("Insert into Companies(Name) values ('EXERIT')");
            Sql("Insert into Companies(Name) values ('CompService')");
        }
        
        public override void Down()
        {
            Sql("Delete from Companies where Name='InfoPower'");
            Sql("Delete from Companies where Name='Oke'");
            Sql("Delete from Companies where Name='Blue'");
            Sql("Delete from Companies where Name='Connectis_'");
            Sql("Delete from Companies where Name='EXERIT'");
            Sql("Delete from Companies where Name='CompService'");
        }
    }
}