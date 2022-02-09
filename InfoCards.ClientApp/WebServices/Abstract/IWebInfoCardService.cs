using System.Collections.Generic;
using System.Threading.Tasks;
using InfoCards.Api.Contract.DTOs;
using InfoCards.Api.Contract.Request;
using InfoCards.Api.Contract.Response;

namespace InfoCards.ClientApp.WebServices.Abstract {
  public interface IWebInfoCardService {
    Task<IEnumerable<InfoCardDto>> GetAllAsync();
    Task<CreateEntityResponse<InfoCardDto>> CreateAsync(CreateInfoCardRequest request);
    Task<bool> DeleteAsync(DeleteInfoCardsRequest request);
    Task<bool> UpdateNameAsync(UpdateInfoCardNameRequest request);
    Task<bool> UpdateImageDataAsync(UpdateInfoCardImageDataRequest request);
  }
}
