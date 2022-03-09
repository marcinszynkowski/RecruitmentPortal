namespace RP.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateFormOfEmploymentTable : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into FormOfEmployments(Name) values ('Nieokreœlone')");
            Sql("Insert into FormOfEmployments(Name) values ('Umowa o pracê')");
            Sql("Insert into FormOfEmployments(Name) values ('Umowa o dzie³o')");
            Sql("Insert into FormOfEmployments(Name) values ('Umowa zlecenie')");
            Sql("Insert into FormOfEmployments(Name) values ('Umowa agencyjna')");
        }
        
        public override void Down()
        {
            Sql("Delete from FormOfEmployments where Name='Nieokreœlone'");
            Sql("Delete from FormOfEmployments where Name='Umowa o pracê'");
            Sql("Delete from FormOfEmployments where Name='Umowa o dzie³o'");
            Sql("Delete from FormOfEmployments where Name='Umowa zlecenie'");
            Sql("Delete from FormOfEmployments where Name='Umowa agencyjna'");
        }
    }
}