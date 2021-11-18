using FluentMigrator;

namespace OzonEdu.MerchandiseService.Migrator.Migrations
{
    [Migration(1)]
    public class MerchRequestTable : Migration
    {
        public override void Up()
        {
            Execute.Sql(@"
                    CREATE TABLE if not exists MerchRequests(
                        id serial,
                        employeeId INT NOT NULL,
                        merchPackName TEXT NOT NULL);"
            );
        }

        public override void Down()
        {
            Execute.Sql("DROP TABLE if exists MerchRequests;");
        }
    }
}