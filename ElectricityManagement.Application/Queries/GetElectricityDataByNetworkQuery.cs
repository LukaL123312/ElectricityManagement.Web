using ElectricityManagement.Domain.ElectricityDataEntity;
using FluentValidation;
using MediatR;

namespace ElectricityManagement.Application.Queries;

public class GetElectricityDataByNetworkQuery : IRequest<List<ElectricityData>>
{
    public string Network { get; set; }
}

public class GetElectricityDataByNetworkQueryValidator : AbstractValidator<GetElectricityDataByNetworkQuery>
{
    public GetElectricityDataByNetworkQueryValidator()
    {
        RuleFor(x => x.Network).NotEmpty();
    }
}
