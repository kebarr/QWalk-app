using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Text.RegularExpressions;

namespace QWalker
{
    public class Inputs
    {

        //function to check form of user input
        public static bool IsValid(string input)
        {
            const string pattern = @"^i|\b[0-9]*\.*[0-9]+\b \+ \b[0-9]*\.*[0-9]+\bi|\b[0-9]*\.*[0-9]+\b|\b[0-9]*\.*[0-9]+\bi|sqrt\(\b[0-9]*\.*[0-9]+\b\)|sqrt\(\b[0-9]*\.*[0-9]+\b\)i|sqrt\(\b[0-9]*\.*[0-9]+\b\) \+ sqrt\(\b[0-9]*\.*[0-9]+\b\) i$";
            return Regex.IsMatch(input, pattern, RegexOptions.Compiled);
        }

        public static Complex Convert(string input)
        {
            // different possible regular expressions need different conversions this is eg 1 + 0i
            Complex value;
            const string poss1 = @"^\b[0-9]*\.*[0-9]+\b \+ \b[0-9]*\.*[0-9]+\bi$";
            const string poss2 = @"^\b[0-9]*\.*[0-9]+\b$";
            const string poss3 = @"^\b[0-9]*\.*[0-9]+\bi$";
            const string poss4 = @"^sqrt\(\b[0-9]*\.*[0-9]+\b\)$";
            const string poss5 = @"^sqrt\(\b[0-9]*\.*[0-9]+\b\)i$";
            const string poss6 = @"^sqrt\(\b[0-9]*\.*[0-9]+\b\) \+ sqrt\(\b[0-9]*\.*[0-9]+\b\)i$";
            if (Regex.IsMatch(input, poss1))
            {
                //Console.WriteLine(input);
                MatchCollection matches = Regex.Matches(poss1, @"\b[0-9]*\.*[0-9]+\b");
                string realString = matches[0].Value;
                string imgString = matches[1].Value;
                //Console.WriteLine(realString);
                //Console.WriteLine(imgString);
                double real = System.Convert.ToDouble(realString);
                double img = System.Convert.ToDouble(imgString);
                value = new Complex(img, real);
            }
            else if (Regex.IsMatch(input, poss2))
            {
                double real = System.Convert.ToDouble(input);
                value = new Complex(0, real);
            }
            else if (Regex.IsMatch(input, poss3))
            {
                string resultString = Regex.Match(input, @"\b[0-9]*\.*[0-9]+\b").Value;
                double img = System.Convert.ToDouble(resultString);
                value = new Complex(img, 0);
            }
            else if (Regex.IsMatch(input, poss4))
            {
                string resultString = Regex.Match(input, @"\b[0-9]*\.*[0-9]+\b").Value;
                double number = System.Convert.ToDouble(resultString);
                number = Math.Sqrt(number);
                value = new Complex(0, number);
            }
            else if (Regex.IsMatch(input, poss5))
            {
                string resultString = Regex.Match(input, @"\b[0-9]*\.*[0-9]+\b").Value;
                double number = System.Convert.ToDouble(resultString);
                number = Math.Sqrt(number);
                value = new Complex(number, 0);
            }
            else if (Regex.IsMatch(input, poss6))
            {
                value = new Complex(0, 1);
            }
            else
            {
                value = new Complex(0, Math.Sqrt(0.5));
            }
            return value;
        }
    }
}
