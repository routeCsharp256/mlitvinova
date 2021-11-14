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
                        id BIGSERIAL PRIMARY KEY,
                        name TEXT NOT NULL,
                        item_type_id INT NOT NULL,
                        clothing_size INT);"
            );
        }

        public override void Down()
        {
            Execute.Sql("DROP TABLE if exists MerchRequests;");
        }
    }
}