using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Data.CustomValidationeRules
{
    public class EmptyInputAttribute : ValidationAttribute
    {
        public EmptyInputAttribute() { }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if (value == null || value == "")
            {
                return new ValidationResult($"Input non puo essere vuoto");
            }

            //return new ValidationResult($"Input e un decimal");
            return ValidationResult.Success;
        }
    }
}
