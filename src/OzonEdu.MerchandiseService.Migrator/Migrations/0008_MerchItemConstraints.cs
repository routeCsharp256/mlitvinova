using FluentMigrator;

namespace OzonEdu.MerchandiseService.Migrator.Migrations
{
    [Migration(8)]
    public class MerchItemConstraintsTable : Migration {
        public override void Up()
        {
            Execute.Sql(@"
                    CREATE TABLE if not exists MerchItemConstraints(
                        merchItemId INT NOT NULL,
                        constraintName TEXT NOT NULL,
                        constraintValue TEXT NOT NULL);");
        }

        public override void Down()
        {
            Execute.Sql("DROP TABLE if exists MerchItemConstraints;");
        }
    }
}