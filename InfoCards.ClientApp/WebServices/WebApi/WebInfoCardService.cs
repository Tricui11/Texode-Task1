using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using InfoCards.Api.Contract.DTOs;
using InfoCards.ClientApp.WebServices.Abstract;
using InfoCards.ClientApp.WebServices.WebClient;

namespace InfoCards.ClientApp.WebServices.WebApi {
  public class WebInfoCardService : WebService, IWebInfoCardService {
    public WebInfoCardService(WebApiHttpClient httpClient) : base(httpClient, "InfoCard") { }

    public async Task<IEnumerable<InfoCardDto>> GetAllAsync() {
      var carrierDtos = await GetAsync<IEnumerable<InfoCardDto>>($"{BaseUrl}/InfoCards");
      return carrierDtos;
    }
  }
}
