using System;
using System.Windows;

namespace InfoCards.ClientApp.Views {
  /// <summary>
  /// Interaction logic for CreateInfoCardView.xaml
  /// </summary>
  public partial class CreateInfoCardView : Window {
    public CreateInfoCardView() {
      InitializeComponent();
    }

    public void ToClose(object sender, EventArgs e) {
      Close();
    }
  }
}
