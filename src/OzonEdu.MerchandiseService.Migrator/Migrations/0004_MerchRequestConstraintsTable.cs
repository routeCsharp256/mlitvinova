using FluentMigrator;

namespace OzonEdu.MerchandiseService.Migrator.Migrations
{
    [Migration(4)]
    public class MerchRequestConstraintsTable : Migration {
        public override void Up()
        {
            Execute.Sql(@"
                    CREATE TABLE if not exists MerchRequestConstraints(
                        requestId int not null,
                        constraintName TEXT NOT NULL,
                        constraintValue TEXT NOT NULL);");
        }

        public override void Down()
        {
            Execute.Sql("DROP TABLE if exists MerchRequestConstraints;");
        }
    }
}