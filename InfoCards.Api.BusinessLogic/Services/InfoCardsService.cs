using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using InfoCards.Api.BusinessLogic.Abstract;
using InfoCards.Api.BusinessLogic.Helpers;
using InfoCards.Api.Contract.DTOs;

namespace InfoCards.Api.BusinessLogic.Services {
  public class InfoCardsService : IInfoCardsService {

    public InfoCardsService() { }

    public async Task<IEnumerable<InfoCardDto>> GetAllAsync() {
      try {
        var xdoc = XDocument.Load(Constants.PhonesPath);
        var allXmlInfoCards = xdoc.Root.Elements();
        var unDeletedXmlInfoCards = allXmlInfoCards.Where(x => x.Element("IsDeleted").Value == "0");
        var dtos = new List<InfoCardDto>();
        foreach (var el in unDeletedXmlInfoCards) {
          var ic = new InfoCardDto();
          ic.Id = int.Parse(el.Element("Id").Value);
          ic.Name = el.Element("Name").Value;
          ic.ImageData = Convert.FromBase64String(el.Element("ImageData").Value);
          dtos.Add(ic);
        }
        return dtos;
      }
      catch (Exception ex) {
      }

      return null;
    }
  }
}
