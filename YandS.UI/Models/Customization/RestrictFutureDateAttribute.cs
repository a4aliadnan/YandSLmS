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
    public class RestrictFutureDateAttribute : ValidationAttribute, IClientValidatable
    {
        private DateTime _MinDate;
        public RestrictFutureDateAttribute()
        {
            _MinDate = DateTime.UtcNow.AddHours(4);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ValidationResult validationResult = ValidationResult.Success;

            if (value == null)
                return validationResult;

            //DateTime _EndDat = DateTime.ParseExact(value.ToString(), "dd/MM/yyyy", null);
            DateTime _EndDat = (DateTime)value;
            DateTime _CurDate = DateTime.UtcNow.AddHours(4);

            int cmp = _EndDat.CompareTo(_CurDate);
            if (cmp <= 0)
            {
                // date1 is lessthan means date1 is comes after date2
                return ValidationResult.Success;
            }
            else if (cmp > 0)
            {
                // date2 is greater means date1 is comes after date1
                return new ValidationResult(ErrorMessage);
            }
            else
            {
                // date1 is same as date2
                return ValidationResult.Success;
            }
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
                ValidationType = "restrictbackdates",
            };
            rule.ValidationParameters.Add("mindate", _MinDate);
            yield return rule;
        }
    }
}