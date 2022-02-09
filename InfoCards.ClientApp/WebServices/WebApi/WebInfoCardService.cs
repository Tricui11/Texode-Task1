using System.Collections.Generic;
using System.Threading.Tasks;
using InfoCards.Api.Contract.DTOs;
using InfoCards.Api.Contract.Request;
using InfoCards.Api.Contract.Response;
using InfoCards.ClientApp.WebServices.Abstract;
using InfoCards.ClientApp.WebServices.WebClient;

namespace InfoCards.ClientApp.WebServices.WebApi {
  public class WebInfoCardService : WebService, IWebInfoCardService {
    public WebInfoCardService(WebApiHttpClient httpClient) : base(httpClient, "InfoCard") { }

    public async Task<IEnumerable<InfoCardDto>> GetAllAsync() {
      var carrierDtos = await GetAsync<IEnumerable<InfoCardDto>>($"{BaseUrl}/InfoCards");
      return carrierDtos;
    }

    public async Task<CreateEntityResponse<InfoCardDto>> CreateAsync(CreateInfoCardRequest request) {
      var response = await PostAsync<CreateInfoCardRequest, CreateEntityResponse<InfoCardDto>>($"{BaseUrl}/Create", request);
      return response;
    }

    public async Task<bool> DeleteAsync(DeleteInfoCardsRequest request) {
      var response = await DeleteAsync<DeleteInfoCardsRequest, bool>($"{BaseUrl}/Delete", request);
      return response;
    }

    public async Task<bool> UpdateNameAsync(UpdateInfoCardNameRequest request) {
      var response = await PutAsync<UpdateInfoCardNameRequest, bool>($"{BaseUrl}/Update/Name", request);
      return response;
    }

    public async Task<bool> UpdateImageDataAsync(UpdateInfoCardImageDataRequest request) {
      var response = await PutAsync<UpdateInfoCardImageDataRequest, bool>($"{BaseUrl}/Update/ImageData", request);
      return response;
    }
  }
}
