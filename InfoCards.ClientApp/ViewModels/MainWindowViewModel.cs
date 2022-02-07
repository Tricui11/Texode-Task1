using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using InfoCards.ClientApp.ViewModels.Helpers;
using InfoCards.ClientApp.WebServices.Abstract;

namespace InfoCards.ClientApp.ViewModels {
  public class MainWindowViewModel {
    private readonly IWebInfoCardService _webInfoCardService;

    public MainWindowViewModel(IWebInfoCardService webInfoCardService) {
      _webInfoCardService = webInfoCardService ?? throw new ArgumentNullException(nameof(webInfoCardService));


      GetAllCommand = new AsyncRelayCommand(GetAllAsync);
    }
    public ObservableCollection<InfoCardViewModel> InfoCards { get; } = new ObservableCollection<InfoCardViewModel>();

    public ICommand GetAllCommand { get; }

    private async Task GetAllAsync() {
      InfoCards.Clear();
      var data = await _webInfoCardService.GetAllAsync();
      if (data != null) {
        foreach (var el in data) {
          InfoCards.Add(new InfoCardViewModel(el));
        }
      }
    }

  }
}
