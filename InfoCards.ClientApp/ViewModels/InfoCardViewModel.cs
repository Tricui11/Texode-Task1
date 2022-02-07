using System.Windows.Media.Imaging;
using InfoCards.Api.Contract.DTOs;

namespace InfoCards.ClientApp.ViewModels {
  public class InfoCardViewModel{
    private readonly InfoCardDto _model;

    public InfoCardViewModel(InfoCardDto model) {
      _model = model;
    }

    public int Id => _model.Id;
    public string Name => _model.Name;
    public BitmapImage Image => ToImage(_model.ImageData);

    public BitmapImage ToImage(byte[] array) {
      BitmapImage image = new BitmapImage();
      image.BeginInit();
      image.StreamSource = new System.IO.MemoryStream(array);
      image.EndInit();
      return image;
    }
  }
}
