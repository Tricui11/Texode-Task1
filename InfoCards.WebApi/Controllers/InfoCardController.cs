using System.Collections.Generic;
using System.Threading.Tasks;
using InfoCards.Api.BusinessLogic.Abstract;
using InfoCards.Api.Contract.DTOs;
using InfoCards.Api.Contract.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace InfoCards.WebApi.Controllers {
  [ApiController]
  [Route("[controller]")]
  public class InfoCardController : ControllerBase {
    private readonly IInfoCardsService _infoCardsService;

    private readonly ILogger<InfoCardController> _logger;
    private readonly IMediator _mediator;

    public InfoCardController(ILogger<InfoCardController> logger, IInfoCardsService infoCardsService, IMediator mediator) {
      _logger = logger;
      _infoCardsService = infoCardsService;
      _mediator = mediator;
    }

    [HttpGet("InfoCards")]
    public async Task<IEnumerable<InfoCardDto>> GetAll() {
      _logger.LogInformation("Getting DeliveryTypes.");

      var infoCards = await _infoCardsService.GetAllAsync();
      return infoCards;
    }

    [HttpPost("Create")]
    public async Task<int> CreateInfoCard([FromBody] CreateInfoCardRequest request) {
      var response = await _mediator.Send(request);
      return response;
    }

    [HttpDelete("Delete")]
    public async Task<bool> DeleteInfoCards([FromBody] DeleteInfoCardsRequest request) {
      var response = await _mediator.Send(request);
      return response;
    }

    [HttpPut("Update/Name")]
    public async Task<bool> UpdateName([FromBody] UpdateInfoCardNameRequest request) {
      var response = await _mediator.Send(request);
      return response;
    }

    [HttpPut("Update/ImageData")]
    public async Task<bool> UpdateName([FromBody] UpdateInfoCardImageDataRequest request) {
      var response = await _mediator.Send(request);
      return response;
    }
  }
}
