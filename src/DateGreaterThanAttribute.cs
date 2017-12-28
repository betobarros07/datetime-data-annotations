using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace O7.DateTimeDataAnnotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class DateGreaterThanAttribute : BaseValidatorAttribute
    {
        private readonly string _property;

        public DateGreaterThanAttribute(string Property)
            => _property = Property;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                if (!DateTime.TryParse(value.ToString(), out var datetimeValue))
                    return new ValidationResult("Invalid datetime format value");
                var propertyStringValue = GetValueFromProperty<string>(
                    _property,
                    validationContext.ObjectType,
                    validationContext.ObjectInstance);
                if (!DateTime.TryParse(propertyStringValue, out var propertyValue))
                    return new ValidationResult($"Invalid datetime format for property {_property}");
                if (datetimeValue < propertyValue)
                    return new ValidationResult(FormatErrorMessage(ErrorMessage));
            }
            return ValidationResult.Success;
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(context.Attributes, "data-date-greater-than", DisplayMessage(context));
            MergeAttribute(context.Attributes, "data-date-greater-than-property", _property);
        }
    }
}