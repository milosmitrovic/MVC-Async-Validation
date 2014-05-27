using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace mUtility.Validation
{
    public static class Extensions
    {
        public static bool ModelIsValid<T>(this Controller controller, T model) where T : class
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(model, serviceProvider: null, items: null);

            return Validator.TryValidateObject(model, validationContext, validationResults, false);
        }

        public static JsonResult GetModelErrors<T>(this Controller controller, T model) where T : class
        {
            var validationResults = new List<ValidationResult>();

            if (model != null)
            {
                var validationContext = new ValidationContext(model, serviceProvider: null, items: null);

                if (!Validator.TryValidateObject(model, validationContext, validationResults, true))
                {
                    var errors = validationResults.Where(item => item.MemberNames.Any()).Select(item => new
                    {
                        FieldName = item.MemberNames.Where(field => !string.IsNullOrWhiteSpace(field)).First(),
                        ErrorMessage = item.ErrorMessage
                    });

                    return new JsonResult() { Data = new { Errors = errors } };
                }
            }

            return new JsonResult();
        }
    }
}
