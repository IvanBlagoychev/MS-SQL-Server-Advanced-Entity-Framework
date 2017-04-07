using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks5to10
{
    public class TagAttribute : ValidationAttribute
    {
        public static string Transform(string tag)
        {

            StringBuilder resultTag = new StringBuilder(tag);
            resultTag.Replace(" ", "");
            resultTag.Replace("\t", "");

            if (resultTag.Length < 1)
            {
                throw new ArgumentException("Invalid tag lenght, it must contains at least one character!");
            }
            else
            {
                if (resultTag[0] != '#')
                {
                    StringBuilder bufferBuilder = new StringBuilder();
                    bufferBuilder.Append($"#");
                    bufferBuilder.Append(resultTag);
                    resultTag = bufferBuilder;
                }
            }

            if (tag.Length != resultTag.Length)
            {

                if (resultTag.ToString().Length >= 20)
                {
                    Console.WriteLine($"Tag has been modified as : {resultTag.ToString().Substring(0, 20)}");
                    return resultTag.ToString().Substring(0, 20);
                }
                else
                {
                    Console.WriteLine($"Tag has been modified as : {resultTag.ToString().Substring(0, resultTag.Length)}");
                    return resultTag.ToString().Substring(0, resultTag.Length);
                }
            }
            else
            {
                return tag;
            }
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string text = value.ToString();
            bool isCorrect = true;

            if (text.Length > 2)
            {
                if (text[0] != '#')
                {
                    isCorrect = false;
                }

                if ((text.Contains(" ") || text.Contains("\t")) && isCorrect)
                {
                    isCorrect = false;
                }

                if ((text.Length > 20) && isCorrect)
                {
                    isCorrect = false;
                }
            }
            else
            {
                //One char tag
                isCorrect = false;
            }

            if (isCorrect)
            {
                return null;
            }
            else
            {
                return new ValidationResult("Invalid tag naming convention");
            }
        }
    }
}

