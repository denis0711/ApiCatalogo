using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogo.Validations
{
    public class PrimeiraLetraMaiusculaAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString())){
                return ValidationResult.Success;
            }

            var primeira = value.ToString()[0].ToString();
            if (primeira != primeira.ToUpper())
            {
                return new ValidationResult("A primeira letra do nome deve ser maiuscula !!");
            }

            return ValidationResult.Success;
        }
    }
}
