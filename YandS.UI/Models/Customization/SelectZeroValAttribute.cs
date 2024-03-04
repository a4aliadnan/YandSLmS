using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace YandS.UI.Models
{
    public class SelectZeroValAttribute : ValidationAttribute, IClientValidatable
    {
        private string _Value;
        public SelectZeroValAttribute()
        {
            _Value = "0";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ValidationResult validationResult = ValidationResult.Success;

            if (value == null)
                return validationResult;

            int cmp = Convert.ToInt32(value);
            if (cmp != 0)
                return ValidationResult.Success;
            else
                return new ValidationResult(ErrorMessage);
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
                ValidationType = "selectzeroval",
            };
            rule.ValidationParameters.Add("minvalue", _Value);
            yield return rule;
        }
    }
}