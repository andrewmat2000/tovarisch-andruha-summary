using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace TovarischAndruha.Summary.Identity.Domain.Attributes;

[AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
public sealed class FullNameValidationAttribute : ValidationAttribute {
  private const string ValidateLoginRegexString = @"^(([А-я\s]+)|([A-z\s]+))$";
  private static readonly Regex _validateLoginRegex = new(ValidateLoginRegexString);
  public override bool IsValid(object? value) {
    if (value == null) {
      return true;
    }

    if (value is not string login) {
      ErrorMessage = "Full name shoud be string.";
      return false;
    }

    if (!_validateLoginRegex.IsMatch(login)) {
      ErrorMessage = "Full name should consist of words in Cyrillic or Latin.";
      return false;
    }

    return true;
  }
}