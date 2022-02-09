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

namespace InfoCards.ClientApp.ViewModels {
  public class MainWindowViewModel : BaseViewModel {
    private readonly IWebInfoCardService _webInfoCardService;
    private IEnumerable _selectedInfoCards;

    public MainWindowViewModel(IWebInfoCardService webInfoCardService) {
      _webInfoCardService = webInfoCardService ?? throw new ArgumentNullException(nameof(webInfoCardService));

      GetAllCommand = new AsyncRelayCommand(GetAllAsync);
      CreateCommand = new AsyncRelayCommand(CreateAsync);
      DeleteCommand = new AsyncRelayCommand(DeleteAsync);
      SetImageCommand = new AsyncRelayCommand(SetImageAsync, _ => SelectedInfoCardsToTypeVM.Count() == 1);
      OpenImageCommand = new ClientCommand(OpenImage);

      GetAllCommand.Execute(null);
    }
    public ObservableCollection<InfoCardGridViewModel> InfoCards { get; } = new ObservableCollection<InfoCardGridViewModel>();
    public InfoCardGridViewModel SelectedInfoCard {
      get => Get<InfoCardGridViewModel>();
      set => Set(value);
    }

    public IEnumerable SelectedInfoCards {
      get => _selectedInfoCards;
      set => _selectedInfoCards = value;
    }

    public IEnumerable<InfoCardGridViewModel> SelectedInfoCardsToTypeVM => _selectedInfoCards?.OfType<InfoCardGridViewModel>();

    public ICommand GetAllCommand { get; }
    public ICommand CreateCommand { get; }
    public ICommand DeleteCommand { get; }
    public ICommand SetImageCommand { get; }
    public ICommand OpenImageCommand { get; }

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
      win.ShowDialog();
      if (vm.CreatedInfoCard != null) {
        var createdInfoCardVM = new InfoCardGridViewModel(vm.CreatedInfoCard, _webInfoCardService);
        InfoCards.Add(createdInfoCardVM);
      }
    }

    private async Task DeleteAsync() {
      var ids = SelectedInfoCardsToTypeVM.Select(x => x.Id);
      var request = new DeleteInfoCardsRequest(ids);
      var response = await _webInfoCardService.DeleteAsync(request);
      if (response == false) {
        MessageBox.Show("Произошла ошибка во время удаления карточек.");
        return;
      }
      foreach (int id in ids) {
        var icToRemove = InfoCards.Single(x => x.Id == id);
        InfoCards.Remove(icToRemove);
      }
    }

    private async Task SetImageAsync() {
      string filePath = BitmapImageHelper.OpenImageFromFileAndReturnPath();
      if (string.IsNullOrEmpty(filePath)) {
        return;
      }

      try {
        var imageData = File.ReadAllBytes(filePath);
        var selectedInfoCard = SelectedInfoCard;
        var request = new UpdateInfoCardImageDataRequest(selectedInfoCard.Id, imageData);
        var response = await _webInfoCardService.UpdateImageDataAsync(request);
        if (response == false) {
          MessageBox.Show("Произошла ошибка во время обновления изображения.");
          return;
        }
        selectedInfoCard.ImageData = imageData;
      }
      catch (Exception ex) {
        MessageBox.Show(ex.ToString());
      }
    }

    private void OpenImage() {
      var vm = new BitmapImageViewModel(SelectedInfoCard.Image);
      var win = new BitmapImageView();
      win.DataContext = vm;
      win.Show();
    }
  }
}