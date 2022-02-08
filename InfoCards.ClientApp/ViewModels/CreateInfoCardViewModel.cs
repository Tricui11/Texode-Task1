using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using InfoCards.Api.Contract.DTOs;
using InfoCards.Api.Contract.Request;
using InfoCards.ClientApp.ViewModels.Helpers;
using InfoCards.ClientApp.WebServices.Abstract;
using Microsoft.Win32;

namespace InfoCards.ClientApp.ViewModels {
  class CreateInfoCardViewModel : BaseViewModel {
    private readonly IWebInfoCardService _webInfoCardService;


    public CreateInfoCardViewModel(IWebInfoCardService webInfoCardService) {
      _webInfoCardService = webInfoCardService;

      SelectImageCommand = new ClientCommand(SelectImage);
      CreateCommand = new AsyncRelayCommand(CreateAsync);
    }

    public string Name {
      get => Get<string>();
      set => Set(value);
    }

    private byte[] ImageData { get; set; }
    public InfoCardDto CreatedInfoCard { get; }

    public event EventHandler Closing;


    public ICommand SelectImageCommand { get; }
    public ICommand CreateCommand { get; }

    private void SelectImage() {
      var openFileDialog = new OpenFileDialog();
      if (openFileDialog.ShowDialog() == false)
        return;
      // получаем выбранный файл
      string filePath = openFileDialog.FileName;
      try {
        ImageData = File.ReadAllBytes(filePath);
      }
      catch (Exception ex) {
        MessageBox.Show(ex.ToString());
      }
    }

    private async Task CreateAsync() {
      var request = new CreateInfoCardRequest(Name, ImageData);
      var response = await _webInfoCardService.CreateAsync(request);
      if (response == null) {
        MessageBox.Show("Произошла ошибка во время создания карточки.");
        return;
      }

      Closing?.Invoke(null, null);
    }
  }
}
