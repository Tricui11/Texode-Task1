using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using InfoCards.Api.BusinessLogic.Abstract;
using InfoCards.Api.BusinessLogic.Helpers;
using InfoCards.Api.Contract.DTOs;
using InfoCards.Api.Contract.Request;
using InfoCards.Api.Contract.Response;
using MediatR;

namespace InfoCards.Api.BusinessLogic.RequestHandlers {
  public class CreateInfoCardRequestHandler : IRequestHandler<CreateInfoCardRequest, CreateEntityResponse<InfoCardDto>> {
    private readonly IInfoCardValidator _validator;

    public CreateInfoCardRequestHandler(IInfoCardValidator validator) {
      _validator = validator;
    }

    public async Task<CreateEntityResponse<InfoCardDto>> Handle(CreateInfoCardRequest request, CancellationToken cancellationToken) {
      try {
        var xdoc = XDocument.Load(Constants.PhonesPath);

        var validationResult = _validator.ValidateCreating(xdoc, request.Name);
        if (validationResult.IsValid == false) {
          return CreateEntityResponse<InfoCardDto>.Failed(validationResult.ErrorMessage);
        }

        var lastId = xdoc.Root.HasElements
          ? xdoc.Root.Elements().Last().Element("Id")?.Value
          : string.Empty;
        int newEntityId = string.IsNullOrEmpty(lastId) ? 1 : int.Parse(lastId) + 1;

        var infoCardTree = new XElement("InfoCard",
          new XElement("Id", newEntityId),
          new XElement("Name", request.Name),
          new XElement("ImageData", Convert.ToBase64String(request.ImageData)),
          new XElement("IsDeleted", 0));

        var metadata = xdoc.Descendants().First(x => x.Name == "metadata");
        metadata.Add(infoCardTree);

        xdoc.Save(Constants.PhonesPath);

        var createdEntity = new InfoCardDto() { Id = newEntityId, Name = request.Name, ImageData = request.ImageData };

        return CreateEntityResponse<InfoCardDto>.Successful(createdEntity);
      }
      catch (Exception ex) {
        return CreateEntityResponse<InfoCardDto>.Failed(ex.Message);
      }
    }
  }
}
