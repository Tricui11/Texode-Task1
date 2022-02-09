using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;


namespace InfoCards.ClientApp.ViewModels {
  public class ValidatableViewModel : BaseViewModel, INotifyDataErrorInfo {
    private readonly Dictionary<string, List<ValidationResult>> _validationResults = new Dictionary<string, List<ValidationResult>>();

    public bool HasErrors => _validationResults.Count > 0;

    public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
    public IEnumerable GetErrors(string propertyName) {
      if (propertyName == null) { return null; }
      if (_validationResults.TryGetValue(propertyName, out List<ValidationResult> validationResults))
        return validationResults.Select(x => x.ErrorMessage);
      return null;
    }

    protected bool ValidateProperty([CallerMemberName] string propertyName = "") {
      var isValid = false;
      var validationResults = new List<ValidationResult>();
      var value = this.GetType().GetProperty(propertyName)?.GetValue(this);
      var context = new ValidationContext(this) { MemberName = propertyName };
      if (Validator.TryValidateProperty(value, context, validationResults)) {
        _validationResults.Remove(propertyName);
        isValid = true;
      } else {
        _validationResults[propertyName] = validationResults;
      }
      ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
      return isValid;
    }

    /// <summary>
    /// Validates view model accordingly with validation attributes.
    /// </summary>
    /// <returns>true if view model is valid, otherwise false</returns>
    public bool ValidateViewModel() {
      var type = this.GetType();
      var props = type.GetProperties();
      foreach (var propertyInfo in props) {
        ValidateProperty(propertyInfo.Name);
      }

      return !HasErrors;
    }

    protected override void Set<T>(T value, [CallerMemberName] string propertyName = null) {
      base.Set(value, propertyName);
      ValidateProperty(propertyName);
    }

    protected void SetWithoutValidation<T>(T value, [CallerMemberName] string propertyName = null) =>
        base.Set(value, propertyName);
  }
}
