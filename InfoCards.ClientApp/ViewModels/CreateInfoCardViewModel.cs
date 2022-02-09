using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using InfoCards.Api.Contract.DTOs;
using InfoCards.Api.Contract.Request;
using InfoCards.ClientApp.ViewModels.Helpers;
using InfoCards.ClientApp.WebServices.Abstract;

namespace InfoCards.ClientApp.ViewModels {
  class CreateInfoCardViewModel : ValidatableViewModel {
    private readonly IWebInfoCardService _webInfoCardService;


    public CreateInfoCardViewModel(IWebInfoCardService webInfoCardService) {
      _webInfoCardService = webInfoCardService;

      SelectImageCommand = new ClientCommand(SelectImage);
      CreateCommand = new AsyncRelayCommand(CreateAsync);
    }

    [Required]
    public string Name {
      get => Get<string>();
      set => Set(value);
    }

    private byte[] ImageData { get; set; }
    [Required]

    public string ImagePath {
      get => Get<string>();
      set => Set(value);
    }

    public InfoCardDto CreatedInfoCard { get; private set; }

    public event EventHandler Closing;


    public ICommand SelectImageCommand { get; }
    public ICommand CreateCommand { get; }

    private void SelectImage() {
      ImagePath = BitmapImageHelper.OpenImageFromFileAndReturnPath();
      if (string.IsNullOrEmpty(ImagePath)) {
        return;
      }

      try {
        ImageData = File.ReadAllBytes(ImagePath);
      }
      catch (Exception ex) {
        MessageBox.Show(ex.ToString());
      }
    }

    private async Task CreateAsync() {
      if (!ValidateViewModel()) {
        return;
      }

      var request = new CreateInfoCardRequest(Name, ImageData);
      var response = await _webInfoCardService.CreateAsync(request);
      if (response == null || response.HasError) {
        string errorMessage = "Произошла ошибка во время создания карточки" + Environment.NewLine + response.ErrorMessage;
        MessageBox.Show(errorMessage);
        return;
      }

      CreatedInfoCard = response.CreatedEntity;

      Closing?.Invoke(null, null);
    }
  }
}
