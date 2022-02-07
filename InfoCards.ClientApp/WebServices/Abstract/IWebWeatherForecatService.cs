using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfoCards.Api.Contract.DTOs;

namespace InfoCards.ClientApp.WebServices.Abstract {
  public interface IWebWeatherForecatService {
    Task<string[]> GetAllAsync();
  }
}
