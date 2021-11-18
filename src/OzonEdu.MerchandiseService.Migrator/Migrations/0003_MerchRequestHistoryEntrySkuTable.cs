using FluentMigrator;

namespace OzonEdu.MerchandiseService.Migrator.Migrations
{
    [Migration(3)]
    public class MerchRequestHistoryEntrySkuTable : Migration {
        public override void Up()
        {
            Execute.Sql(@"
                    CREATE TABLE if not exists MerchRequestHistoryEntrySkus(
                        requestId INT NOT NULL,
                        sku TEXT NOT NULL);");
        }

        public override void Down()
        {
            Execute.Sql("DROP TABLE if exists MerchRequestHistoryEntrySkus;");
        }
    }
}