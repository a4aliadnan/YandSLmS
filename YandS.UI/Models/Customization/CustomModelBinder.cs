using System;
using System.ComponentModel;
using System.Globalization;
using System.Web.Mvc;
public class CustomModelBinder : DefaultModelBinder
{
    protected override object GetPropertyValue(ControllerContext controllerContext, ModelBindingContext bindingContext, PropertyDescriptor propertyDescriptor, IModelBinder propertyBinder)
    {
        var propertyType = propertyDescriptor.PropertyType;
        if (propertyType == typeof(DateTime) || propertyType == typeof(DateTime?))
        {
            var providerValue = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (null != providerValue)
            {
                DateTime date;
                if (DateTime.TryParseExact(providerValue.AttemptedValue, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                {
                    return date;
                }
            }
        }
        return base.GetPropertyValue(controllerContext, bindingContext, propertyDescriptor, propertyBinder);
    }
}
