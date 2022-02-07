using InfoCards.ClientApp.WebServices.Abstract;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InfoCards.ClientApp.WebServices {
  public abstract class WebService : IWebService {

    protected WebService(HttpClient httpClient, string baseUrl) {
      HttpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

      if (string.IsNullOrWhiteSpace(baseUrl)) {
        throw new ArgumentNullException(nameof(baseUrl));
      }

      BaseUrl = baseUrl;

      var loadMediatrAssembly = typeof(MediatR.Mediator); //for using requests : IRequest<T>
    }

    public string BaseUrl { get; }
    public HttpClient HttpClient { get; }

    protected async Task<T> GetAsync<T>(string url) {
      try {
        var response = await HttpClient.GetAsync(url);

        if (response.IsSuccessStatusCode) {
          var result = await response.Content.ReadAsAsync<T>();
          return result;
        }

      }
      catch (Exception e) {

      }

      return default;
    }

    protected async Task<TResponse> PostAsync<TRequest, TResponse>(string url, TRequest request) {
      try {
        var response = await HttpClient.PostAsJsonAsync(url, request);
        if (response.IsSuccessStatusCode) {
          var result = await response.Content.ReadAsAsync<TResponse>();
          return result;
        }

      }
      catch (Exception e) {
      }

      return default;
    }

    protected async Task<TResponse> PutAsync<TRequest, TResponse>(string url, TRequest request) {
      try {
        var response = await HttpClient.PutAsJsonAsync(url, request);
        if (response.IsSuccessStatusCode) {
          var result = await response.Content.ReadAsAsync<TResponse>();
          return result;
        }

      }
      catch (Exception e) {
      }

      return default;
    }

    protected async Task<TResponse> DeleteAsync<TRequest, TResponse>(string url, TRequest request) {
      try {
        var message = new HttpRequestMessage(HttpMethod.Delete, url);
        if (request != null) {
          var jsonString = JsonConvert.SerializeObject(request);
          message.Content = new StringContent(jsonString, Encoding.UTF8, "application/json");
        }

        var response = await HttpClient.SendAsync(message);

        if (response.IsSuccessStatusCode) {
          var result = await response.Content.ReadAsAsync<TResponse>();

          return result;
        }

      }
      catch (Exception e) {
      }

      return default;
    }
  }
}