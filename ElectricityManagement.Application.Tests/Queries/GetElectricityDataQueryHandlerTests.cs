using ElectricityManagement.Application.Handlers;
using ElectricityManagement.Application.IRepositories;
using ElectricityManagement.Application.Queries;
using ElectricityManagement.Domain.ElectricityDataEntity;
using FluentAssertions;
using Moq;
using Xunit;

namespace ElectricityManagement.Application.Tests.Queries;

public class GetElectricityDataQueryHandlerTests
{
    private readonly Mock<IElectricityDataRepository> _electricityRepositoryMock;

    public GetElectricityDataQueryHandlerTests()
    {
        _electricityRepositoryMock = new();
    }

    [Fact]
    public async Task Shoud_Return_Electricity_Data_List_When_Successful()
    {
        // Arrange
        var getAllAsyncResponse = new List<ElectricityData>();

        var query = new GetElectricityDataQuery();

        var handler = new GetElectricityDataQueryHandler(
            _electricityRepositoryMock.Object);

        _electricityRepositoryMock.Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(getAllAsyncResponse);
        // Act
        var result = await handler.Handle(query, default);

        // Assert
        result.Should().BeOfType<List<ElectricityData>>();
    }
}
