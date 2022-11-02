using ElectricityManagement.Domain.ElectricityDataEntity;
using Microsoft.EntityFrameworkCore;

namespace ElectricityManagement.Infrastructure.Data.DbContext;

public class ElectricityManagementDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public ElectricityManagementDbContext(DbContextOptions<ElectricityManagementDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
    public DbSet<ElectricityData> ElectricityDatas { get; set; }

}
