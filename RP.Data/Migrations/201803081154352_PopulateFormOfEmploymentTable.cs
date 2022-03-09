namespace RP.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateFormOfEmploymentTable : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into FormOfEmployments(Name) values ('Nieokre�lone')");
            Sql("Insert into FormOfEmployments(Name) values ('Umowa o prac�')");
            Sql("Insert into FormOfEmployments(Name) values ('Umowa o dzie�o')");
            Sql("Insert into FormOfEmployments(Name) values ('Umowa zlecenie')");
            Sql("Insert into FormOfEmployments(Name) values ('Umowa agencyjna')");
        }
        
        public override void Down()
        {
            Sql("Delete from FormOfEmployments where Name='Nieokre�lone'");
            Sql("Delete from FormOfEmployments where Name='Umowa o prac�'");
            Sql("Delete from FormOfEmployments where Name='Umowa o dzie�o'");
            Sql("Delete from FormOfEmployments where Name='Umowa zlecenie'");
            Sql("Delete from FormOfEmployments where Name='Umowa agencyjna'");
        }
    }
}