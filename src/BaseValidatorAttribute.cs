using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace O7.DateTimeDataAnnotations
{
    public abstract class BaseValidatorAttribute : ValidationAttribute, IClientModelValidator
    {
        protected string DisplayMessage(ClientModelValidationContext context)
            => FormatErrorMessage(context.ModelMetadata.GetDisplayName());

        protected T GetValueFromProperty<T>(string propertyName, Type type, object instance)
        {
            var property = type.GetProperty(propertyName);
            return (T)property.GetValue(instance);
        }

        public override string FormatErrorMessage(string name)
            => string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name);

        public virtual void AddValidation(ClientModelValidationContext context)
            => MergeAttribute(context.Attributes, "data-val", "true");

        protected void MergeAttribute(
            IDictionary<string, string> attributes,
            string key,
            string value)
        {
            if (!attributes.ContainsKey(key))
                attributes.Add(key, value);
        }
    }
}