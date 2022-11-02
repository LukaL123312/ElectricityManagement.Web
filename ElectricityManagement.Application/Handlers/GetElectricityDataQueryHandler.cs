using ElectricityManagement.Application.IRepositories;
using ElectricityManagement.Application.Queries;
using ElectricityManagement.Domain.ElectricityDataEntity;
using MediatR;

namespace ElectricityManagement.Application.Handlers;

public class GetElectricityDataQueryHandler : IRequestHandler<GetElectricityDataQuery, List<ElectricityData>>
{
    private readonly IElectricityDataRepository _electricityRepository;

    public GetElectricityDataQueryHandler(IElectricityDataRepository electricityRepository)
    {
        _electricityRepository = electricityRepository;
    }

    public async Task<List<ElectricityData>> Handle(GetElectricityDataQuery request, CancellationToken cancellationToken)
    {
        var response = await _electricityRepository.GetAllAsync(cancellationToken);
        return response.ToList();
    }
}
