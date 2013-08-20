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
            //possibilities are i, decimal, decimal + decimal i, decimali, sqrt(decimal), sqrt(decimal)i, sqrt(decimal) + sqrt(decimal)i
            const string pattern = @"^[0-9]*\.*[0-9]+\+[0-9]*\.*[0-9]+i|\b[0-9]*\.*[0-9]+\b|\b[0-9]*\.*[0-9]+\b*i|sqrt\(\b[0-9]*\.*[0-9]+\b\)|sqrt\(\b[0-9]*\.*[0-9]+\b\)i|sqrt\(\b[0-9]*\.*[0-9]+\b\)\+sqrt\(\b[0-9]*\.*[0-9]+\b\)*i|\-[0-9]*\.*[0-9]+i|\-\b[0-9]*\.*[0-9]+\b|[0-9]*\.*[0-9]+\-[0-9]*\.*[0-9]+i|\-[0-9]*\.*[0-9]+\+[0-9]*\.*[0-9]+i|\-sqrt\(\b[0-9]*\.*[0-9]+\b\)i|\-sqrt\(\b[0-9]*\.*[0-9]+\b\)|sqrt\(\b[0-9]*\.*[0-9]+\b\)\-sqrt\(\b[0-9]*\.*[0-9]+\b\)*i|-sqrt\(\b[0-9]*\.*[0-9]+\b\)\+sqrt\(\b[0-9]*\.*[0-9]+\b\)*i$";
            return Regex.IsMatch(input, pattern, RegexOptions.Compiled);
        }

        public static Complex Convert(string input)
        {
            // different possible regular expressions need different conversions this is eg 1 + 0i
            Complex value;
            string poss1 = @"^[0-9]*\.*[0-9]+\+[0-9]*\.*[0-9]+i$";
            string poss2 = @"^\b[0-9]*\.*[0-9]+\b$";
            string poss3 = @"^\b[0-9]*\.*[0-9]+\b*i$";
            string poss4 = @"^sqrt\(\b[0-9]*\.*[0-9]+\b\)$";
            string poss5 = @"^sqrt\(\b[0-9]*\.*[0-9]+\b\)i$";
            string poss6 = @"^sqrt\(\b[0-9]*\.*[0-9]+\b\)\+sqrt\(\b[0-9]*\.*[0-9]+\b\)*i$";
            string poss7 = @"^\-[0-9]*\.*[0-9]+i$";
            string poss8 = @"^\-\b[0-9]*\.*[0-9]+\b$";
            string poss9 = @"^[0-9]*\.*[0-9]+\-[0-9]*\.*[0-9]+i$";
            string poss10 = @"^\-[0-9]*\.*[0-9]+\+[0-9]*\.*[0-9]+i$";
            string poss11 = @"^\-sqrt\(\b[0-9]*\.*[0-9]+\b\)i$";
            string poss12 = @"^\-sqrt\(\b[0-9]*\.*[0-9]+\b\)$";
            string poss13 = @"^sqrt\(\b[0-9]*\.*[0-9]+\b\)\-sqrt\(\b[0-9]*\.*[0-9]+\b\)*i$";
            string poss14 = @"^-sqrt\(\b[0-9]*\.*[0-9]+\b\)\+sqrt\(\b[0-9]*\.*[0-9]+\b\)*i$";
            if (Regex.IsMatch(input, poss1) == true)
            {
                Console.WriteLine(input);
                MatchCollection matches = Regex.Matches(input, @"[0-9]*\.*[0-9]+");
                string realString = matches[0].Value;
                string imgString = matches[1].Value;
                Console.WriteLine(realString);
                Console.WriteLine(imgString);
                double real = System.Convert.ToDouble(realString);
                double img = System.Convert.ToDouble(imgString);
                value = new Complex(img, real);
            }
            else if (Regex.IsMatch(input, poss2) == true)
            {
                double real = System.Convert.ToDouble(input);
                value = new Complex(0, real);
            }
            else if (Regex.IsMatch(input, poss3) == true)
            {
                string resultString = Regex.Match(input, @"\b[0-9]*\.*[0-9]+\b").Value;
                double img = System.Convert.ToDouble(resultString);
                value = new Complex(img, 0);
            }
            else if (Regex.IsMatch(input, poss4) == true)
            {
                string resultString = Regex.Match(input, @"\b[0-9]*\.*[0-9]+\b").Value;
                double number = System.Convert.ToDouble(resultString);
                number = Math.Sqrt(number);
                value = new Complex(0, number);
            }
            else if (Regex.IsMatch(input, poss5) == true)
            {
                string resultString = Regex.Match(input, @"\b[0-9]*\.*[0-9]+\b").Value;
                double number = System.Convert.ToDouble(resultString);
                number = Math.Sqrt(number);
                value = new Complex(number,0);
            }
            else if (Regex.IsMatch(input, poss6))
            {
                MatchCollection matches = Regex.Matches(input, @"\b[0-9]*\.*[0-9]+\b");
                string realString = matches[0].Value;
                string imgString = matches[1].Value;
                //Console.WriteLine(realString);
                //Console.WriteLine(imgString);
                double real = System.Convert.ToDouble(realString);
                double img = System.Convert.ToDouble(imgString);
                real = Math.Sqrt(real);
                img = Math.Sqrt(img);
                value = new Complex(img, real);
            }
            // \-\b[0-9]*\.*[0-9]+\b*i = poss7
            else if (Regex.IsMatch(input, poss7) == true)
            {
                string resultString = Regex.Match(input, @"[0-9]*\.*[0-9]+").Value;
                double img = System.Convert.ToDouble(resultString);
                value = new Complex(0, -img);
            }
            // poss8 = @"^\-\b[0-9]*\.*[0-9]+\b$";
            else if (Regex.IsMatch(input, poss8) == true)
            {
                string resultString = Regex.Match(input, @"\b[0-9]*\.*[0-9]+\b").Value;
                double img = System.Convert.ToDouble(resultString);
                value = new Complex(-img, 0);
            }
            //poss9 = "^\b[0-9]*\.*[0-9]+\b\-\b[0-9]*\.*[0-9]+\b*i$
            else if (Regex.IsMatch(input, poss9) == true)
            {
                MatchCollection matches = Regex.Matches(input, @"[0-9]*\.*[0-9]+");
                string realString = matches[0].Value;
                string imgString = matches[1].Value;
                double real = System.Convert.ToDouble(realString);
                double img = System.Convert.ToDouble(imgString);
                value = new Complex(real, -img);
            }
            // poss10 = @"^\-\b[0-9]*\.*[0-9]+\b\+\b[0-9]*\.*[0-9]+\b*i$";
            else if (Regex.IsMatch(input, poss10) == true)
            {
                MatchCollection matches = Regex.Matches(input, @"[0-9]*\.*[0-9]+");
                string realString = matches[0].Value;
                string imgString = matches[1].Value;
                double real = System.Convert.ToDouble(realString);
                double img = System.Convert.ToDouble(imgString);
                value = new Complex(-real, img);
            }
            // poss11 = @"^-sqrt\(\b[0-9]*\.*[0-9]+\b\)i$";
            else if (Regex.IsMatch(input, poss11) == true)
            {
                string resultString = Regex.Match(input, @"\b[0-9]*\.*[0-9]+\b").Value;
                double img = System.Convert.ToDouble(resultString);
                img = Math.Sqrt(img);
                value = new Complex(0, -img);
            }
            //             string poss12 = @"^-sqrt\(\b[0-9]*\.*[0-9]+\b\)$";
            else if (Regex.IsMatch(input, poss12) == true)
            {
                string resultString = Regex.Match(input, @"\b[0-9]*\.*[0-9]+\b").Value;
                double img = System.Convert.ToDouble(resultString);
                img = Math.Sqrt(img);
                value = new Complex(-img, 0);
            }
            // poss13 = @"^sqrt\(\b[0-9]*\.*[0-9]+\b\)\-sqrt\(\b[0-9]*\.*[0-9]+\b\)*i$";
            else if (Regex.IsMatch(input, poss13) == true)
            {
                MatchCollection matches = Regex.Matches(input, @"\b[0-9]*\.*[0-9]+\b");
                string realString = matches[0].Value;
                string imgString = matches[1].Value;
                double real = System.Convert.ToDouble(realString);
                double img = System.Convert.ToDouble(imgString);
                real = Math.Sqrt(real);
                img = Math.Sqrt(img);
                value = new Complex(real, -img);
            }
            else if (Regex.IsMatch(input, poss14) == true)
            {
                MatchCollection matches = Regex.Matches(input, @"\b[0-9]*\.*[0-9]+\b");
                string realString = matches[0].Value;
                string imgString = matches[1].Value;
                double real = System.Convert.ToDouble(realString);
                double img = System.Convert.ToDouble(imgString);
                real = Math.Sqrt(real);
                img = Math.Sqrt(img);
                value = new Complex(-real, img);
            }
            else
            {
                value = new Complex(0, 20);
            }
            return value;
        }
    }
}
