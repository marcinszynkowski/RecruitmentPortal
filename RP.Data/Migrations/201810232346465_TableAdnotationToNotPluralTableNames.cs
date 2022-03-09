namespace RP.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class TableAdnotationToNotPluralTableNames : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Educations", newName: "Education");
            RenameTable(name: "dbo.FormOfEmployments", newName: "FormOfEmployment");
            RenameTable(name: "dbo.PersonalDatas", newName: "PersonalData");
            RenameTable(name: "dbo.VirtualCVs", newName: "VirtualCV");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.VirtualCV", newName: "VirtualCVs");
            RenameTable(name: "dbo.PersonalData", newName: "PersonalDatas");
            RenameTable(name: "dbo.FormOfEmployment", newName: "FormOfEmployments");
            RenameTable(name: "dbo.Education", newName: "Educations");
        }
    }
}
