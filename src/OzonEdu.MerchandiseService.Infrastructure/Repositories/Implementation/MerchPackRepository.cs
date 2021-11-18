using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchandiseService.Infrastructure.Repositories.Infrastructure.Interfaces;

namespace OzonEdu.MerchandiseService.Infrastructure.Repositories.Implementation
{
    public class MerchPackRepository : IMerchPackRepository
    {
        private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;
        private readonly IChangeTracker _changeTracker;
        
        private const int Timeout = 5;
        
        public MerchPackRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory, IChangeTracker changeTracker)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _changeTracker = changeTracker;
        }
        
        public async Task<MerchPack> GetMerchPackByName(MerchPackName name, CancellationToken cancellationToken = default)
        {
            const string sql = @"
                select mp.name as MerchPackName, mit.name as ItemType, mic.constraintName, mic.constraintValue from merchpacks mp
                join merchitems mi on mi.packId = mp.id
                join merchitemtypes mit on mi.typeid = mit.id
                left join merchitemconstraints mic on mi.id = mic.merchItemId
                WHERE mp.name = @Name;";
            
            var parameters = new
            {
                Name = name.Value,
            };
            
            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            var packItems = await connection.QueryAsync<Models.MerchPackItemModel>(commandDefinition);

            var items = packItems
                .GroupBy(
                    x => x.ItemType,
                    x => (x.ConstraintName != null && x.ConstraintValue != null) ? 
                        new GenericMerchConstraint(x.ConstraintName, x.ConstraintValue)
                        : null,
                    (key, value) => new MerchItem(ItemType.ToItemType(key), 
                        value
                            .Any(x => x is not null) ? value.ToList() 
                            : new List<GenericMerchConstraint>()))
                .ToList();
            
            Console.Write(items.Count);
            
            var merchPack = new MerchPack(name, items);
            _changeTracker.Track(merchPack);
            
            return merchPack;
        }

        public async Task<List<MerchPack>> GetAllMerchPacks(CancellationToken cancellationToken = default)
        {
            const string sql = @"
                select mp.name as MerchPackName, mit.name as ItemType, mic.constraintName, mic.constraintValue from merchpacks mp
                join merchitems mi on mi.packId = mp.id
                join merchitemtypes mit on mi.typeid = mit.id
                left join merchitemconstraints mic on mi.id = mic.merchItemId";
            
            var commandDefinition = new CommandDefinition(
                sql,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            var packItems = await connection.QueryAsync<Models.MerchPackItemModel>(commandDefinition);

            var items = packItems
                .GroupBy(
                    x => x.MerchPackName,
                    x => new {x.ItemType, x.ConstraintName, x.ConstraintValue},
                    (key, value) => new MerchPack(new MerchPackName(key),
                        value.GroupBy(
                            x => x.ItemType,
                            x => (x.ConstraintName != null && x.ConstraintValue != null)
                                ? new GenericMerchConstraint(x.ConstraintName, x.ConstraintValue)
                                : null,
                            (key, value) => new MerchItem(ItemType.ToItemType(key),
                                value
                                    .Any(x => x is not null)
                                    ? value.ToList()
                                    : new List<GenericMerchConstraint>())).ToList()));

            return items.ToList();
        }

        public async Task<bool> MerchPackExists(MerchPackName name, CancellationToken cancellationToken = default)
        {
            const string sql = @"
                select * from merchpacks mp
                WHERE mp.name = @Name;";
            
            var parameters = new
            {
                Name = name.Value,
            };
            
            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            var packItems = await connection.QueryAsync(commandDefinition);

            return packItems.Any();
        }
    }
}