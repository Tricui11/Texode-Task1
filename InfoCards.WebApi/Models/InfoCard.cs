namespace InfoCards.WebApi.Models {
  public class InfoCard {
    public int Id { get; set; }
    public string Name { get; set; }
    public byte[] ImageData { get; set; }
    public bool IsDeleted { get; set; }
  }
}
