using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Hazel
{
    public class Pattern
    {
        public static String Match(String pattern, String document)
        {
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            MatchCollection matches = regex.Matches(document);
            return matches[0].Value;
        }
        public static String Match(String pattern, String document, RegexOptions options)
        {
            Regex regex = new Regex(pattern, options);
            MatchCollection matches = regex.Matches(document);
            return matches[0].Value;
        }
    }
}
