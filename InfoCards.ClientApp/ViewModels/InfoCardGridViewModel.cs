using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using InfoCards.Api.Contract.DTOs;
using InfoCards.Api.Contract.Request;
using InfoCards.ClientApp.ViewModels.Helpers;
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
        if (_model.Name == value) {
          return;
        }

        var request = new UpdateInfoCardNameRequest(Id, value);
        var response = Task.Run(async() =>  await _webInfoCardService.UpdateNameAsync(request));
        if (response == null || response.Result == false) { 
          MessageBox.Show("Произошла ошибка во время обновления имени.");
          return;
        }

        _model.Name = value;
        OnPropertyChanged();
      }
    }

    public byte[] ImageData {
      get => _model.ImageData;
      set {
        _model.ImageData = value;
        OnPropertyChanged();
        OnPropertyChanged(nameof(Image));
      }
    }

    public BitmapImage Image => BitmapImageHelper.GetFromByteArray(_model.ImageData);
  }
}
