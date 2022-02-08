using InfoCards.Api.Contract.Abstract;

namespace InfoCards.Api.Contract.Response {

  namespace InfoCards.Api.Contract.Response {
    public class Response : IErrorResponse {
      public Response() {
      }

      public Response(string errorMessage) {
        ErrorMessage = errorMessage;
      }

      public bool Success { get; set; }
      public string ErrorMessage { get; set; }
      public string InnerExceptionMsg { get; set; }
      public bool HasError => !string.IsNullOrEmpty(ErrorMessage);

      public static Response Successful { get; } = new Response { Success = true };
      public static Response Failed(string errorMessage) => new Response(errorMessage);
    }

    public class Response<T> : Response {
      // ReSharper disable once UnusedMember.Global
      public Response() {
      }

      public Response(string errorMessage) {
        ErrorMessage = errorMessage;
      }

      public Response(T value) {
        Success = true;
        Value = value;
      }

      // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global
      public T Value { get; set; }
    }
  }
}