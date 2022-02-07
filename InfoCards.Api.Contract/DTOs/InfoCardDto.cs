using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfoCards.Api.Contract.DTOs {
  public class InfoCardDto {
    public int Id { get; set; }
    public string Name { get; set; }
    public byte[] ImageData { get; set; }
  }
}
