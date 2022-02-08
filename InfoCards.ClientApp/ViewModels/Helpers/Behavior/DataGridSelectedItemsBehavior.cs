using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace InfoCards.ClientApp.ViewModels.Helpers.Behavior {
  public class DataGridSelectedItemsBehavior : Behavior<DataGrid> {
    protected override void OnAttached() {
      AssociatedObject.SelectionChanged += AssociatedObjectSelectionChanged;
    }

    protected override void OnDetaching() {
      AssociatedObject.SelectionChanged -= AssociatedObjectSelectionChanged;
    }

    void AssociatedObjectSelectionChanged(object sender, SelectionChangedEventArgs e) {
      var array = new object[AssociatedObject.SelectedItems.Count];
      AssociatedObject.SelectedItems.CopyTo(array, 0);
      SelectedItems = array;
    }

    public static readonly DependencyProperty SelectedItemsProperty =
        DependencyProperty.Register("SelectedItems", typeof(IEnumerable), typeof(DataGridSelectedItemsBehavior),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public IEnumerable SelectedItems {
      get { return (IEnumerable)GetValue(SelectedItemsProperty); }
      set { SetValue(SelectedItemsProperty, value); }
    }
  }
}
