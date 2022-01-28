using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace WPFPresentation
{
    public static class ValidationHelpers
    {
        public static bool IsValidEmailAddress(this string email)
        {
            bool isValid = false;
            Regex rx = new Regex(@"^[a-zA-Z0-9]+@[a-zA-Z0-9]+[.][a-zA-Z]+");
            if (rx.IsMatch(email))
            {
                isValid = true;
            }
            return isValid;
        }

        public static bool IsValidPassword(this string password)
        {
            bool isValid = false;

            Regex rxLower = new Regex(@"[a-z]+");
            Regex rxUpper = new Regex(@"[A-Z]+");
            Regex rxNumber = new Regex(@"[0-9]+");
            Regex rxSpecial = new Regex("[ !\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~]+");

            if (rxLower.IsMatch(password) && rxUpper.IsMatch(password) &&
                rxNumber.IsMatch(password) && rxSpecial.IsMatch(password))
            {
                isValid = true;
            }

            return isValid;
        }
    }
}
