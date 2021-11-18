using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestAggregate;
using OzonEdu.MerchandiseService.Domain.Contracts;
using OzonEdu.MerchandiseService.Infrastructure.Repositories.Infrastructure.Interfaces;

namespace OzonEdu.MerchandiseService.Infrastructure.Repositories.Implementation
{
    public class MerchPackRequestRepository : IMerchPackRequestRepository
    {
        public IUnitOfWork UnitOfWork { get; }
        
        private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;
        private readonly IChangeTracker _changeTracker;
        
        private const int Timeout = 5;
        
        public MerchPackRequestRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory, IChangeTracker changeTracker)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _changeTracker = changeTracker;
        }
        
        public async Task<MerchPackRequest> CreateAsync(MerchPackRequest itemToCreate, CancellationToken cancellationToken = default)
        {
            const string sql = @"
                INSERT INTO merchRequests (employeeId, merchPackName)
                VALUES (@Name, @ItemTypeId, @ClothingSize);";

            throw new NotImplementedException();

            // var parameters = new
            // {
            //     EmployeeId = itemToCreate.EmployeeId.Id,
            //     MerchPackName = itemToCreate.MerchPack.Name,
            // };
            //
            // var commandDefinition = new CommandDefinition(
            //     sql,
            //     parameters: parameters,
            //     commandTimeout: Timeout,
            //     cancellationToken: cancellationToken);
            //
            // var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            // await connection.ExecuteAsync(commandDefinition);
            // _changeTracker.Track(itemToCreate);
            // return itemToCreate;
        }

        public Task<MerchPackRequest> UpdateAsync(MerchPackRequest itemToUpdate, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<MerchPackRequest>> FindAllAsync(CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<MerchPackRequest>> FindByEmployeeAsync(Employee employee, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(MerchPackRequest request, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}