namespace InfoCards.Api.Contract.Abstract {
  public interface IErrorResponse {
    string ErrorMessage { get; set; }
    bool HasError { get; }
  }
}
