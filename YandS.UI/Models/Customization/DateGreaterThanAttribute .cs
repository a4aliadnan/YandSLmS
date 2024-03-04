using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YandS.UI.Models
{
    public class DateGreaterThanAttribute : ValidationAttribute, IClientValidatable
    {
        public string otherPropertyName;
        public DateGreaterThanAttribute() { }
        public DateGreaterThanAttribute(string otherPropertyName, string errorMessage)
            : base(errorMessage)
        {
            this.otherPropertyName = otherPropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ValidationResult validationResult = ValidationResult.Success;

            if (value == null)
                return validationResult;

            try
            {
                // Using reflection we can get a reference to the other date property, in this example the project start date
                var containerType = validationContext.ObjectInstance.GetType();
                var field = containerType.GetProperty(this.otherPropertyName);
                var extensionValue = field.GetValue(validationContext.ObjectInstance, null);
                if (extensionValue == null)
                {
                    //validationResult = new ValidationResult("Start Date is empty");
                    return validationResult;
                }
                var datatype = extensionValue.GetType();

                //var otherPropertyInfo = validationContext.ObjectInstance.GetType().GetProperty(this.otherPropertyName);
                if (field == null)
                    return new ValidationResult(String.Format("Unknown property: {0}.", otherPropertyName));
                // Let's check that otherProperty is of type DateTime as we expect it to be
                if ((field.PropertyType == typeof(DateTime) || (field.PropertyType.IsGenericType && field.PropertyType == typeof(Nullable<DateTime>))))
                {
                    DateTime toValidate = (DateTime)value;
                    DateTime referenceProperty = (DateTime)field.GetValue(validationContext.ObjectInstance, null);
                    // if the end date is lower than the start date, than the validationResult will be set to false and return
                    // a properly formatted error message
                    int res = toValidate.CompareTo(referenceProperty);

                    if (toValidate.CompareTo(referenceProperty) < 0)
                    {
                        validationResult = new ValidationResult(ErrorMessageString);
                    }
                }
                else
                {
                    validationResult = new ValidationResult("An error occurred while validating the property. OtherProperty is not of type DateTime");
                }
            }
            catch (Exception ex)
            {
                // Do stuff, i.e. log the exception
                // Let it go through the upper levels, something bad happened
                throw ex;
            }

            return validationResult;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
                ValidationType = "isgreater",
            };
            rule.ValidationParameters.Add("otherproperty", otherPropertyName);
            yield return rule;
        }
    }
}