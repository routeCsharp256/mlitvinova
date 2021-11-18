using FluentMigrator;

namespace OzonEdu.MerchandiseService.Migrator.Migrations
{
    [Migration(2)]
    public class MerchRequestHistoryEntryTable : Migration {
        public override void Up()
        {
            Execute.Sql(@"
                    CREATE TABLE if not exists MerchRequestHistoryEntries(
                        id INT PRIMARY KEY,
                        employeeId INT NOT NULL,
                        merchPackName TEXT NOT NULL,
                        completedAt DATE);");
        }

        public override void Down()
        {
            Execute.Sql("DROP TABLE if exists MerchRequestHistoryEntries;");
        }
    }
}