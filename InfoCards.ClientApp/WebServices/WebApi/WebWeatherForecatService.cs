using System.Net.Http;
using System.Threading.Tasks;
using InfoCards.ClientApp.WebServices.Abstract;
using InfoCards.ClientApp.WebServices.WebClient;

namespace InfoCards.ClientApp.WebServices.WebApi {
  public class WebWeatherForecatService : WebService, IWebWeatherForecatService {
    public WebWeatherForecatService(WebApiHttpClient httpClient) : base(httpClient, "WeatherForecast") { }

    public async Task<string[]> GetAllAsync() {
      var url = $"{BaseUrl}";
      //todo: consider to make overloaded methods with query params
      var result = await GetAsync<string[]>(url);

      return result;
    }
  }
}
