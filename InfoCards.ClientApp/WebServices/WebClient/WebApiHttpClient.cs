using System;
using System.Net.Http;

namespace InfoCards.ClientApp.WebServices.WebClient {
  public class WebApiHttpClient : HttpClient {
    public WebApiHttpClient(string baseUrl) : base(new HttpClientHandler { UseDefaultCredentials = true }) {
      BaseAddress = new Uri(baseUrl);
    }
  }
}