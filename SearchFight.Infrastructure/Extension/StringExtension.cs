using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace SearchFight.Infrastructure.Extension
{
    public static class StringExtension
    {
        public static bool TryToLong(this string input, out long result)
        {
            var regex = new Regex("[^0-9]");
            var s = regex.Replace(input, "");

            return long.TryParse(s, out result);
        }

        public static bool TryToLong(this string input, out long result, string regexPattern, string splitSeparator = " ")
        {
            var regexGroup = input.RegexGroup(regexPattern);
            result = 0L;

            if (regexGroup != null)
            {
                var valueSplit = regexGroup.Value.Split(splitSeparator);

                for (var i = 0; i < valueSplit.Length; i++)
                {
                    if (valueSplit[i].TryToLong(out result))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static Group RegexGroup(this string input, string regexPattern)
        {
            var regex = new Regex(regexPattern);

            if (regex.IsMatch(input))
            {
                var match = regex.Match(input);
                var groups = match.Groups;

                if (groups.Count >= 2)
                {
                    return groups[1];
                }
            }

            return null;
        }
    }
}
