using MediatR;

namespace InfoCards.Api.Contract.Request {
  public class UpdateInfoCardNameRequest : BaseUpdateInfoCardReques, IRequest<bool> {

    public UpdateInfoCardNameRequest(int id, string name) : base(id) {
      Name = name;
    }

    public string Name { get; }
  }
}