using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls;

namespace CryptChat.Client.Domain
{
    public class NotEmptyValidationRule : ValidationRule
	{
		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			return string.IsNullOrWhiteSpace((value ?? "").ToString())
				? new ValidationResult(false, "Field is required.")
				: ValidationResult.ValidResult;
		}
	}
}
