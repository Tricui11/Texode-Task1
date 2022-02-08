using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using InfoCards.Api.Contract.DTOs;
using InfoCards.Api.Contract.Request;
using InfoCards.ClientApp.WebServices.Abstract;

namespace InfoCards.ClientApp.ViewModels {
  public class InfoCardGridViewModel : BaseViewModel{
    private readonly InfoCardDto _model;
    private readonly IWebInfoCardService _webInfoCardService;

    public InfoCardGridViewModel(InfoCardDto model, IWebInfoCardService webInfoCardService) {
      _model = model;
      _webInfoCardService = webInfoCardService;
    }

    public int Id => _model.Id;
    public string Name {
      get => _model.Name;
      set {
        _model.Name = value;

        var request = new UpdateInfoCardNameRequest(Id, Name);
        var response = Task.Run(async() =>  await _webInfoCardService.UpdateNameAsync(request));
        if (response == null) { 
          MessageBox.Show("Произошла ошибка во время обновления имени.");
          return;
        }
        OnPropertyChanged();
      }
    }

    public byte[] ImageData => _model.ImageData;

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
