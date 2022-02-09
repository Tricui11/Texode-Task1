using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using InfoCards.Api.BusinessLogic.Helpers;
using InfoCards.Api.Contract.Request;
using MediatR;

namespace InfoCards.Api.BusinessLogic.RequestHandlers {
  public class DeleteInfoCardsRequestHandler : IRequestHandler<DeleteInfoCardsRequest, bool> {

    public DeleteInfoCardsRequestHandler() { }

    public async Task<bool> Handle(DeleteInfoCardsRequest request, CancellationToken cancellationToken) {
      try {
        var xdoc = XDocument.Load(Constants.PhonesPath);
        var allXmlInfoCards = xdoc.Root.Elements();
        var toDeleteXmlInfoCards = allXmlInfoCards.Where(x => request.Ids.Contains(int.Parse(x.Element("Id").Value)));
        foreach (var el in toDeleteXmlInfoCards) {
          el.Element("IsDeleted").Value = "1";
        }
        xdoc.Save(Constants.PhonesPath);

        return true;
      }
      catch (Exception) {
        return false;
      }
    }
  }
}
