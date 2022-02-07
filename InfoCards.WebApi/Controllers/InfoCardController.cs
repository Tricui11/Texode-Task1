using InfoCards.Api.Contract.DTOs;
using InfoCards.WebApi.Abstract;
using InfoCards.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfoCards.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InfoCardController : ControllerBase
    {
    private readonly IInfoCardsService _infoCardsService;

        private readonly ILogger<InfoCardController> _logger;

        public InfoCardController(ILogger<InfoCardController> logger, IInfoCardsService infoCardsService)
        {
            _logger = logger;
            _infoCardsService = infoCardsService;
        }

    [HttpGet("InfoCards")]
    public async Task<IEnumerable<InfoCardDto>> GetAll() {
      _logger.LogInformation("Getting DeliveryTypes.");

      var infoCards = await _infoCardsService.GetAllAsync();
      return infoCards;
    }
  }
}
