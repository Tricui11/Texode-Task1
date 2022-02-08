using System.Collections.Generic;
using MediatR;

namespace InfoCards.Api.Contract.Request {
  public class DeleteInfoCardsRequest : IRequest<bool> {

    public DeleteInfoCardsRequest(IEnumerable<int> ids) {
      Ids = ids;
    }

    public IEnumerable<int> Ids { get; }
  }
}