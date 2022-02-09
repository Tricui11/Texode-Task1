using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using InfoCards.Api.BusinessLogic.Abstract;

namespace InfoCards.Api.BusinessLogic.Services {
  public class InfoCardValidator : IInfoCardValidator {
    public ValidationResult ValidateCreating(XDocument xdoc, string name) {
      if (IsNameExist(xdoc, name)) {
        return ValidationResult.Failed("Информационная карточка с таким именем уже существует.");
      }

      return ValidationResult.Valid;
    }

    public ValidationResult ValidateNameEditing(XDocument xdoc, string name) {
      if (IsNameExist(xdoc, name)) {
        return ValidationResult.Failed("Информационная карточка с таким именем уже существует.");
      }

      return ValidationResult.Valid;
    }

    private bool IsNameExist(XDocument xdoc, string name) {
      IEnumerable<XElement> existingInfoCards = from ic in xdoc.Descendants("InfoCard")
                                                where ic.Element("Name").Value == name && ic.Element("IsDeleted").Value == "0"
                                                select ic;
      return existingInfoCards.Any();
    }
  }
}
