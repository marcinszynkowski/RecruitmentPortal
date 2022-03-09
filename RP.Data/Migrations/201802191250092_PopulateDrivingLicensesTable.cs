namespace RP.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateDrivingLicensesTable : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into DrivingLicenses(Category) values ('AM')");
            Sql("Insert into DrivingLicenses(Category) values ('A1')");
            Sql("Insert into DrivingLicenses(Category) values ('A2')");
            Sql("Insert into DrivingLicenses(Category) values ('A')");
            Sql("Insert into DrivingLicenses(Category) values ('B1')");
            Sql("Insert into DrivingLicenses(Category) values ('B')");
            Sql("Insert into DrivingLicenses(Category) values ('B+E')");
            Sql("Insert into DrivingLicenses(Category) values ('B(96)')");
            Sql("Insert into DrivingLicenses(Category) values ('C')");
            Sql("Insert into DrivingLicenses(Category) values ('C+E')");
            Sql("Insert into DrivingLicenses(Category) values ('C1')");
            Sql("Insert into DrivingLicenses(Category) values ('C1+E')");
            Sql("Insert into DrivingLicenses(Category) values ('D')");
            Sql("Insert into DrivingLicenses(Category) values ('D+E')");
            Sql("Insert into DrivingLicenses(Category) values ('D1')");
            Sql("Insert into DrivingLicenses(Category) values ('D1+E')");
            Sql("Insert into DrivingLicenses(Category) values ('T')");
            Sql("Insert into DrivingLicenses(Category) values ('Tramwaj')");
        }

        public override void Down()
        {
            Sql("Delete from DrivingLicenses where Category='AM'");
            Sql("Delete from DrivingLicenses where Category='A1'");
            Sql("Delete from DrivingLicenses where Category='A2'");
            Sql("Delete from DrivingLicenses where Category='A'");
            Sql("Delete from DrivingLicenses where Category='B1'");
            Sql("Delete from DrivingLicenses where Category='B'");
            Sql("Delete from DrivingLicenses where Category='B+E'");
            Sql("Delete from DrivingLicenses where Category='B(96)'");
            Sql("Delete from DrivingLicenses where Category='C'");
            Sql("Delete from DrivingLicenses where Category='C+E'");
            Sql("Delete from DrivingLicenses where Category='C1'");
            Sql("Delete from DrivingLicenses where Category='C1+E'");
            Sql("Delete from DrivingLicenses where Category='D'");
            Sql("Delete from DrivingLicenses where Category='D+E'");
            Sql("Delete from DrivingLicenses where Category='D1'");
            Sql("Delete from DrivingLicenses where Category='D1+E'");
            Sql("Delete from DrivingLicenses where Category='T'");
            Sql("Delete from DrivingLicenses where Category='Tramwaj'");
        }
    }
}
