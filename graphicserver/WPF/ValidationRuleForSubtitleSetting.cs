using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace graphicserver.WPF
{
   public  class ValidationRuleForSubtitleSetting : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if(value.ToString().ToLower()=="yes")
            {
                return new ValidationResult( true,null);
            }else if(value.ToString().ToLower() == "no")
            {
                return new ValidationResult(true, null);
            }
            else
            {
                return new ValidationResult(false, "Allow only yes and no value");
            }            
        }
    }

    public class ValidateIntegerValue : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int intValue;
            if (int.TryParse(value.ToString(), out intValue))
            {
                return new ValidationResult(true, null);
            }
            return new ValidationResult(false, "Wrong Value");
        }
    }
}
