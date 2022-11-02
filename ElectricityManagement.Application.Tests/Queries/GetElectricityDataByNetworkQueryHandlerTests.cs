using ElectricityManagement.Application.Handlers;
using ElectricityManagement.Application.IRepositories;
using ElectricityManagement.Application.Queries;
using ElectricityManagement.Domain.ElectricityDataEntity;
using FluentAssertions;
using Moq;
using System.Linq.Expressions;
using Xunit;

namespace ElectricityManagement.Application.Tests.Queries;

public class GetElectricityDataByNetworkQueryHandlerTests
{
    private readonly Mock<IElectricityDataRepository> _electricityRepositoryMock;

    public GetElectricityDataByNetworkQueryHandlerTests()
    {
        _electricityRepositoryMock = new();
    }

    [Fact]
    public async Task Shoud_Return_Electricity_Data_List_When_Successful()
    {
        // Arrange
        var findAllAsyncResponse = new List<ElectricityData>();

        var query = new GetElectricityDataByNetworkQuery();

        var handler = new GetElectricityDataByNetworkQueryHandler(
            _electricityRepositoryMock.Object);

        _electricityRepositoryMock.Setup(x => x.FindAllAsync((It.IsAny<Expression<Func<ElectricityData, bool>>>()),
            It.IsAny<CancellationToken>())).ReturnsAsync(findAllAsyncResponse);
        // Act
        var result = await handler.Handle(query, default);

        // Assert
        result.Should().BeOfType<List<ElectricityData>>();
    }
}
