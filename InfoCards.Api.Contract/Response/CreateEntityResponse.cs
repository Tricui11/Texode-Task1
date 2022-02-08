namespace InfoCards.Api.Contract.Response {
  public class CreateEntityResponse<T> : InfoCards.Api.Contract.Response.Response {
    public CreateEntityResponse() { }
    public CreateEntityResponse(T createdEntity) {
      CreatedEntity = createdEntity;
      Success = true;
    }

    public CreateEntityResponse(string errorMessage) {
      ErrorMessage = errorMessage;
    }

    public T CreatedEntity { get; set; }

    public new static CreateEntityResponse<T> Successful(T createdValue) {
      return new CreateEntityResponse<T>(createdValue);
    }

    public new static CreateEntityResponse<T> Failed(string errorMessage) {
      return new CreateEntityResponse<T>(errorMessage);
    }
  }
}