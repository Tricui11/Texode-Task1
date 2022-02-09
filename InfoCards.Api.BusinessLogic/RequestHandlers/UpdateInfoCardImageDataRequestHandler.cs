using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using InfoCards.Api.BusinessLogic.Helpers;
using InfoCards.Api.Contract.Request;
using MediatR;

namespace InfoCards.Api.BusinessLogic.RequestHandlers {
  public class UpdateInfoCardImageDataRequestHandler : IRequestHandler<UpdateInfoCardImageDataRequest, bool> {

    public UpdateInfoCardImageDataRequestHandler() { }

    public async Task<bool> Handle(UpdateInfoCardImageDataRequest request, CancellationToken cancellationToken) {
      try {
        var xdoc = XDocument.Load(Constants.PhonesPath);
        var infoCardToUpdate = xdoc.Root.Elements().Single(x => request.Id == int.Parse(x.Element("Id").Value));
        infoCardToUpdate.Element("ImageData").Value = Convert.ToBase64String(request.ImageData);
        xdoc.Save(Constants.PhonesPath);

        return true;
      }
      catch (Exception) {
        return false;
      }
    }
  }
}
