namespace RP.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ChangeTablesNames : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.PrawoJazdy", newName: "DrivingLicenses");
            RenameTable(name: "dbo.UżytkownicyJęzykiObce", newName: "UserForeignLanguages");
            RenameTable(name: "dbo.PrzebiegZatrudnienia", newName: "WorkExperiences");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.WorkExperiences", newName: "PrzebiegZatrudnienia");
            RenameTable(name: "dbo.UserForeignLanguages", newName: "UżytkownicyJęzykiObce");
            RenameTable(name: "dbo.DrivingLicenses", newName: "PrawoJazdy");
        }
    }
}
