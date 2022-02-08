using System.Collections.Generic;
using System.Threading.Tasks;
using InfoCards.Api.Contract.DTOs;

namespace InfoCards.Api.BusinessLogic.Abstract {
  public interface IInfoCardsService {
    Task<IEnumerable<InfoCardDto>> GetAllAsync();
  }
}
