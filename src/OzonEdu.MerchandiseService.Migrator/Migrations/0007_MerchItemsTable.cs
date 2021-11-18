using FluentMigrator;

namespace OzonEdu.MerchandiseService.Migrator.Migrations
{
    [Migration(7)]
    public class MerchItemsTable : Migration {
        public override void Up()
        {
            Execute.Sql(@"
                    CREATE TABLE if not exists MerchItems(
                        id INT PRIMARY KEY,
                        typeId INT NOT NULL,
                        packId INT NOT NULL);");
        }

        public override void Down()
        {
            Execute.Sql("DROP TABLE if exists MerchItems;");
        }
    }
}