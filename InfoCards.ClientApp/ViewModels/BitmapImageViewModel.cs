using System.Windows.Media.Imaging;

namespace InfoCards.ClientApp.ViewModels {
  public class BitmapImageViewModel {
    private readonly BitmapImage _model;
    public BitmapImageViewModel(BitmapImage model) {
      _model = model;
    }

    public BitmapImage Image => _model;
  }
}
