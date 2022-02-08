using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using InfoCards.Api.Contract.Request;
using InfoCards.ClientApp.ViewModels.Helpers;
using InfoCards.ClientApp.Views;
using InfoCards.ClientApp.WebServices.Abstract;
using Microsoft.Win32;

namespace InfoCards.ClientApp.ViewModels {
  public class MainWindowViewModel : BaseViewModel {
    private readonly IWebInfoCardService _webInfoCardService;
    private IEnumerable _selectedInfoCards;

    public MainWindowViewModel(IWebInfoCardService webInfoCardService) {
      _webInfoCardService = webInfoCardService ?? throw new ArgumentNullException(nameof(webInfoCardService));

      GetAllCommand = new AsyncRelayCommand(GetAllAsync);
      CreateCommand = new AsyncRelayCommand(CreateAsync);
      DeleteCommand = new AsyncRelayCommand(DeleteAsync);
      SetImageCommand = new AsyncRelayCommand(SetImageAsync);
    }
    public ObservableCollection<InfoCardGridViewModel> InfoCards { get; } = new ObservableCollection<InfoCardGridViewModel>();
    public InfoCardGridViewModel SelectedInfoCard {
      get => Get<InfoCardGridViewModel>();
      set => Set(value);
    }

    public IEnumerable SelectedInfoCards {
      get => _selectedInfoCards;
      set {
        _selectedInfoCards = value;
      }
    }

    public IEnumerable<InfoCardGridViewModel> SelectedInfoCardsToTypeVM =>
        _selectedInfoCards?.OfType<InfoCardGridViewModel>();

    public ICommand GetAllCommand { get; }
    public ICommand CreateCommand { get; }
    public ICommand DeleteCommand { get; }
    public ICommand SetImageCommand { get; }

    private async Task GetAllAsync() {
      InfoCards.Clear();
      var data = await _webInfoCardService.GetAllAsync();
      if (data != null) {
        foreach (var el in data) {
          InfoCards.Add(new InfoCardGridViewModel(el, _webInfoCardService));
        }
      }
    }

    private async Task CreateAsync() {
      var vm = new CreateInfoCardViewModel(_webInfoCardService);
      var win = new CreateInfoCardView();
      vm.Closing += win.ToClose;
      win.DataContext = vm;
      win.WindowStartupLocation = WindowStartupLocation.CenterScreen;
      win.ShowDialog();
    }

    private async Task DeleteAsync() {
      var ids = SelectedInfoCardsToTypeVM.Select(x => x.Id);
      var request = new DeleteInfoCardsRequest(ids);
      var response = await _webInfoCardService.DeleteAsync(request);
      if (response == false) {
        MessageBox.Show("Произошла ошибка во время удаления карточек.");
        return;
      }
    }

    private async Task SetImageAsync() {
      var openFileDialog = new OpenFileDialog();
      if (openFileDialog.ShowDialog() == false)
        return;
      // получаем выбранный файл
      string filePath = openFileDialog.FileName;
      try {
        var imageData = File.ReadAllBytes(filePath);
        var request = new UpdateInfoCardImageDataRequest(SelectedInfoCard.Id, imageData);
        var response = Task.Run(async () => await _webInfoCardService.UpdateImageDataAsync(request));
        if (response == null) {
          MessageBox.Show("Произошла ошибка во время обновления изображения.");
          return;
        }
      }
      catch (Exception ex) {
        MessageBox.Show(ex.ToString());
      }
    }
  }
}