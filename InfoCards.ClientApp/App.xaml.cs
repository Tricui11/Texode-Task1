using System.Windows;
using InfoCards.ClientApp.ViewModels;
using InfoCards.ClientApp.WebServices.Abstract;
using InfoCards.ClientApp.WebServices.WebApi;
using InfoCards.ClientApp.WebServices.WebClient;
using Microsoft.Extensions.DependencyInjection;

namespace InfoCards.ClientApp {
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application {
    private ServiceProvider _serviceProvider;
    private static int _fontSize;

    public App() {
      FontSize = 16;
      ServiceCollection services = new ServiceCollection();
      ConfigureServices(services);
      _serviceProvider = services.BuildServiceProvider();
    }
    public static int FontSize {
      get { return _fontSize; }
      set {
        _fontSize = value;
      }
    }

    protected override void OnStartup(StartupEventArgs e) {
      base.OnStartup(e);

      var services = new ServiceCollection();
      _serviceProvider = services.BuildServiceProvider(true);

      ConfigureServices(services);
    }

    private void ConfigureServices(ServiceCollection services) {
      string webApiUrl = "http://localhost:48829/";
      services.AddSingleton(x => new WebApiHttpClient(webApiUrl));
      services.AddSingleton<MainWindow>();
      services.AddSingleton<MainWindowViewModel>();


      ConfigureWebServices(services);
    }

    private void ConfigureWebServices(ServiceCollection services) {
      services.AddTransient<IWebInfoCardService, WebInfoCardService>();
    }

    private void OnStartup(object sender, StartupEventArgs e) {
      var mainWindow = _serviceProvider.GetService<MainWindow>();
      mainWindow.Show();
    }
  }
}