using ElectricityManagement.Domain.ElectricityDataEntity;
using MediatR;

namespace ElectricityManagement.Application.Queries;

public class GetElectricityDataQuery : IRequest<List<ElectricityData>>
{

}

