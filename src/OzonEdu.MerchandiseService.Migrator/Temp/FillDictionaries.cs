using FluentMigrator;

namespace OzonEdu.MerchandiseService.Migrator.Temp
{
    [Migration(9)]
    public class FillDictionaries : ForwardOnlyMigration
    {
        public override void Up()
        {
            Execute.Sql(@"
                INSERT INTO MerchPacks (id, name)
                VALUES 
                    (1, 'StarterPack'),
                    (2, 'WelcomePack'),
                    (3, 'Goodbyepack')
                ON CONFLICT DO NOTHING
            ");
            
            Execute.Sql(@"
                INSERT INTO MerchItemTypes (id, name)
                VALUES 
                    (1, 'TShirt'),
                    (2, 'Sweatshirt'),
                    (3, 'Notepad'),
                    (4, 'Bag'),
                    (5, 'Pen'),
                    (6, 'Socks')
                ON CONFLICT DO NOTHING
            ");
            
            Execute.Sql(@"
                INSERT INTO MerchItems (id, typeId, packId)
                VALUES 
                    (1, 1, 1),
                    (2, 2, 1)
                ON CONFLICT DO NOTHING
            ");
        }
    }
}