using InfoCards.Api.Contract.DTOs;
using InfoCards.Api.Contract.Response;
using MediatR;

namespace InfoCards.Api.Contract.Request {
  public class CreateInfoCardRequest : IRequest<CreateEntityResponse<InfoCardDto>> {

    public CreateInfoCardRequest(string name, byte[] imageData) {
      Name = name;
      ImageData = imageData;
    }

    public string Name { get; }
    public byte[] ImageData { get; }
  }
}