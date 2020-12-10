using System;
using System.Globalization;
using System.ComponentModel.DataAnnotations;
using McMaster.Extensions.CommandLineUtils;
using McMaster.Extensions.CommandLineUtils.Validation;

namespace TCXGenerator.Validators
{
    class DateTimeValidator : IOptionValidator
    {
        public ValidationResult GetValidationResult(CommandOption option, ValidationContext context)
        {
            if (!option.HasValue()) return ValidationResult.Success;
            var value = option.Value();

            DateTime result;
            if (value is string str && !DateTime.TryParseExact(str, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out result))
            {
                return new ValidationResult($"The value for --{option.LongName} must be a valid date in the format 'yyyy-MM-dd HH:mm:ss'");
            }

            return ValidationResult.Success;
        }
    }
}