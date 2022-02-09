using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using InfoCards.Api.BusinessLogic.Abstract;
using InfoCards.Api.BusinessLogic.Helpers;
using InfoCards.Api.Contract.Request;
using MediatR;

namespace InfoCards.Api.BusinessLogic.RequestHandlers {
  public class UpdateInfoCardNameRequestHandler : IRequestHandler<UpdateInfoCardNameRequest, bool> {
    private readonly IInfoCardValidator _validator;

    public UpdateInfoCardNameRequestHandler(IInfoCardValidator validator) {
      _validator = validator;
    }

    public async Task<bool> Handle(UpdateInfoCardNameRequest request, CancellationToken cancellationToken) {
      try {
        var xdoc = XDocument.Load(Constants.PhonesPath);
        var validationResult = _validator.ValidateNameEditing(xdoc, request.Name);
        if (validationResult.IsValid == false) {
          return false;
        }

        var infoCardToUpdate = xdoc.Root.Elements().Single(x => request.Id == int.Parse(x.Element("Id").Value));
        infoCardToUpdate.Element("Name").Value = request.Name;
        xdoc.Save(Constants.PhonesPath);
        return true;
      }
      catch (Exception) {
        return false;
      }
    }
  }
}