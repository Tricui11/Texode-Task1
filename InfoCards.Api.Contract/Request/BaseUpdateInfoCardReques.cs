namespace InfoCards.Api.Contract.Request {
  public class BaseUpdateInfoCardReques {

    public BaseUpdateInfoCardReques(int id) {
      Id = id;
    }

    public int Id { get; }
  }
}