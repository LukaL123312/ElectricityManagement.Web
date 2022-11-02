using ElectricityManagement.Application.IRepositories;
using ElectricityManagement.Domain.ElectricityDataEntity;
using ElectricityManagement.Infrastructure.Data.DbContext;

namespace ElectricityManagement.Infrastructure.Data.Repository;

public class ElectricityDataRepository : Repository<ElectricityData>, IElectricityDataRepository
{
    public ElectricityDataRepository(ElectricityManagementDbContext context) : base(context)
    {

    }
}