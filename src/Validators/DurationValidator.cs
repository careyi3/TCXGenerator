using System;
using System.Globalization;
using System.ComponentModel.DataAnnotations;
using McMaster.Extensions.CommandLineUtils;
using McMaster.Extensions.CommandLineUtils.Validation;

namespace TCXGenerator.Validators
{
    class DurationValidator : IOptionValidator
    {
        public ValidationResult GetValidationResult(CommandOption option, ValidationContext context)
        {
            if (!option.HasValue()) return ValidationResult.Success;
            var value = option.Value();

            TimeSpan timeSpan;
            if (value is string str && !TimeSpan.TryParseExact(str, @"mm\:ss", CultureInfo.CurrentCulture, TimeSpanStyles.AssumeNegative, out timeSpan))
            {
                return new ValidationResult($"The value for --{option.LongName} must be a valid duration in the format 'mm:ss'");
            }

            return ValidationResult.Success;
        }
    }
}