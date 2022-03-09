namespace RP.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulatePaymentTypesTable : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into PaymentTypes(Name) values ('Nieokre�lone')");
            Sql("Insert into PaymentTypes(Name) values ('stawka godzinowa')");
            Sql("Insert into PaymentTypes(Name) values ('stawka dniowa')");
            Sql("Insert into PaymentTypes(Name) values ('stawka tygodniowa')");
            Sql("Insert into PaymentTypes(Name) values ('stawka miesi�czna')");
        }
        
        public override void Down()
        {
            Sql("Delete from PaymentTypes where Name='Nieokre�lone'");
            Sql("Delete from PaymentTypes where Name='stawka godzinowa'");
            Sql("Delete from PaymentTypes where Name='stawka dniowa'");
            Sql("Delete from PaymentTypes where Name='stawka tygodniowa'");
            Sql("Delete from PaymentTypes where Name='stawka miesi�czna'");
        }
    }
}
