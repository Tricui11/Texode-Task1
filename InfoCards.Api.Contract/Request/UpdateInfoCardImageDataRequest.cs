using MediatR;

namespace InfoCards.Api.Contract.Request {
  public class UpdateInfoCardImageDataRequest : BaseUpdateInfoCardReques, IRequest<bool> {

    public UpdateInfoCardImageDataRequest(int id, byte[] imageData) : base(id) {
      ImageData = imageData;
    }

    public byte[] ImageData { get; }
  }
}