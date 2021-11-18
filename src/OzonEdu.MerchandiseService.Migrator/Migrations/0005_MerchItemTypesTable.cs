using FluentMigrator;

namespace OzonEdu.MerchandiseService.Migrator.Migrations
{
    [Migration(5)]
    public class MerchItemTypesTable : Migration {
        public override void Up()
        {
            Execute.Sql(@"
                    CREATE TABLE if not exists MerchItemTypes(
                        id INT PRIMARY KEY,
                        name TEXT NOT NULL);");
        }

        public override void Down()
        {
            Execute.Sql("DROP TABLE if exists MerchItemTypes;");
        }
    }
}