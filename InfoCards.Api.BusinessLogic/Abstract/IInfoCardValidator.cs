using System.Xml.Linq;
using InfoCards.Api.BusinessLogic.Services;

namespace InfoCards.Api.BusinessLogic.Abstract {
  public interface IInfoCardValidator {
    ValidationResult ValidateCreating(XDocument xdoc, string name);
    ValidationResult ValidateNameEditing(XDocument xdoc, string name);
  }
}
