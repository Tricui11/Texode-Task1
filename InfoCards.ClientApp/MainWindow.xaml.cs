using System.Windows;
using InfoCards.ClientApp.ViewModels;

namespace InfoCards.ClientApp {
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window {
    public MainWindow(MainWindowViewModel mainWindowViewModel) {
      DataContext = mainWindowViewModel;
      InitializeComponent();
    }
  }
}
