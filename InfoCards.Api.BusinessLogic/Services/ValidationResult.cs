namespace InfoCards.Api.BusinessLogic.Services {
  public class ValidationResult {
    private ValidationResult() { }

    public bool IsValid { get; set; }
    public string ErrorMessage { get; set; }

    public static ValidationResult Valid { get; } = new ValidationResult { IsValid = true };

    public static ValidationResult Failed(string errorMessage) {
      return new ValidationResult { IsValid = false, ErrorMessage = errorMessage };
    }
  }
}
