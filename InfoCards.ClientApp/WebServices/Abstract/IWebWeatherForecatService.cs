using System.Threading.Tasks;

namespace InfoCards.ClientApp.WebServices.Abstract {
  public interface IWebWeatherForecatService {
    Task<string[]> GetAllAsync();
  }
}
