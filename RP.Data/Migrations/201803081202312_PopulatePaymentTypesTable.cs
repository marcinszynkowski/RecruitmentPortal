namespace RP.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulatePaymentTypesTable : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into PaymentTypes(Name) values ('Nieokreœlone')");
            Sql("Insert into PaymentTypes(Name) values ('stawka godzinowa')");
            Sql("Insert into PaymentTypes(Name) values ('stawka dniowa')");
            Sql("Insert into PaymentTypes(Name) values ('stawka tygodniowa')");
            Sql("Insert into PaymentTypes(Name) values ('stawka miesiêczna')");
        }
        
        public override void Down()
        {
            Sql("Delete from PaymentTypes where Name='Nieokreœlone'");
            Sql("Delete from PaymentTypes where Name='stawka godzinowa'");
            Sql("Delete from PaymentTypes where Name='stawka dniowa'");
            Sql("Delete from PaymentTypes where Name='stawka tygodniowa'");
            Sql("Delete from PaymentTypes where Name='stawka miesiêczna'");
        }
    }
}
