using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using AutoMapper;
using InfoCards.Api.Contract.DTOs;
using InfoCards.WebApi.Abstract;
using InfoCards.WebApi.Models;

namespace InfoCards.WebApi.Services {
  public class InfoCardsService : IInfoCardsService {
    private readonly IMapper _mapper;

    public InfoCardsService(IMapper mapper) {
      _mapper = mapper;
    }

    public async Task<IEnumerable<InfoCardDto>> GetAllAsync() {



      try {
        int readbyte = 0;
        int bytestoread = 1044;
        XmlTextReader xmltxtrd = new XmlTextReader("C:/Users/Furer/Downloads/11.xml");
        string dirPref = "C:/Users/Furer/Downloads/";
        int i = 111;
        MemoryStream ms = null;
        BinaryWriter bw = null;
        byte[] base64buffer = new byte[bytestoread];
        var list = new List<InfoCard>();
        while (xmltxtrd.Read()) {
          if (xmltxtrd.NodeType == XmlNodeType.Element && xmltxtrd.Name == "image") {
            ms = new MemoryStream();
            bw = new BinaryWriter(ms);
            do {
              readbyte = xmltxtrd.ReadBase64(base64buffer, 0, bytestoread);
              bw.Write(base64buffer, 0, readbyte);
            }
            while (bytestoread <= readbyte);
            i++;
            bw.Flush();
            bw.Close();


            var ic = new InfoCard();
            ic.Name = i.ToString();
            ic.ImageData = ms.ToArray();
            list.Add(ic);
            ms.Close();



          }
        }
        xmltxtrd.Close();


        var dtos = _mapper.Map<IEnumerable<InfoCardDto>>(list);
        return dtos;

        //       MessageBox.Show("End of reading and writing!");
      }
      catch (Exception ex) {
 //       MessageBox.Show(ex.ToString());
      }



      return null;


    }
  }
}
