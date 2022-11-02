using ElectricityManagement.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ElectricityManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ElectricityDataController : ControllerBase
{
    private readonly IMediator _mediatr;
    private readonly ILogger<ElectricityDataController> _logger;
    public ElectricityDataController(IMediator mediatr, ILogger<ElectricityDataController> logger)
    {
        _mediatr = mediatr;
        _logger = logger;
    }

    [HttpGet("get-electricity-data")]
    public async Task<IActionResult> GetElectricityData(CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send(new GetElectricityDataQuery(), cancellationToken);

        return Ok(result);
    }

    [HttpGet("get-electricity-data-by-network")]
    public async Task<IActionResult> GetElectricityDataByNetwork(string network, CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send(new GetElectricityDataByNetworkQuery { Network = network }, cancellationToken);

        return Ok(result);
    }


}
