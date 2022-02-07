using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InfoCards.Api.Contract.DTOs;
using InfoCards.WebApi.Models;

namespace InfoCards.WebApi.Abstract {
  public interface IInfoCardsService {
    Task<IEnumerable<InfoCardDto>> GetAllAsync();
  }
}
