using MediatR;

namespace InfoCards.Api.Contract.Request {
  public class CreateInfoCardRequest : IRequest<int> {

    public CreateInfoCardRequest(string name, byte[] imageData) {
      Name = name;
      ImageData = imageData;
    }

    public string Name { get; }
    public byte[] ImageData { get; }
  }
}