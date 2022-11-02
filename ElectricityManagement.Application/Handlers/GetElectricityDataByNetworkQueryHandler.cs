using ElectricityManagement.Application.IRepositories;
using ElectricityManagement.Application.Queries;
using ElectricityManagement.Domain.ElectricityDataEntity;
using MediatR;

namespace ElectricityManagement.Application.Handlers;

public class GetElectricityDataByNetworkQueryHandler : IRequestHandler<GetElectricityDataByNetworkQuery, List<ElectricityData>>
{
    private readonly IElectricityDataRepository _electricityRepository;

    public GetElectricityDataByNetworkQueryHandler(IElectricityDataRepository electricityRepository)
    {
        _electricityRepository = electricityRepository;
    }

    public async Task<List<ElectricityData>> Handle(GetElectricityDataByNetworkQuery request, CancellationToken cancellationToken)
    {
        var response = await _electricityRepository.FindAllAsync(x => x.Network.Equals(request.Network), cancellationToken);
        return response.ToList();
    }
}
