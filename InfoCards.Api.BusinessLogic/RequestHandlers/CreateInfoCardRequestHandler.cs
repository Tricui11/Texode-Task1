using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using InfoCards.Api.BusinessLogic.Helpers;
using InfoCards.Api.Contract.Request;
using MediatR;

namespace InfoCards.Api.BusinessLogic.RequestHandlers {
  public class CreateInfoCardRequestHandler : IRequestHandler<CreateInfoCardRequest, int> {

    public CreateInfoCardRequestHandler() { }

    public async Task<int> Handle(CreateInfoCardRequest request, CancellationToken cancellationToken) {
      try {
        var xdoc = XDocument.Load(Constants.PhonesPath);
        var lastId = xdoc.Root.Elements().Last().Element("Id").Value;
        var infoCardTree = new XElement("InfoCard",
          new XElement("Id", string.IsNullOrEmpty(lastId) ? 1 : int.Parse(lastId) + 1),
          new XElement("Name", request.Name),
          new XElement("ImageData", Convert.ToBase64String(request.ImageData)),
          new XElement("IsDeleted", 0));
        xdoc.Root.Add(infoCardTree);
        xdoc.Save(Constants.PhonesPath);
      }
      catch (Exception ex) {
      }

      return 6;
    }
  }
}
