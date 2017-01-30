using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProjectEuler
{
    static class RegexUtil
    {
        static Regex _regex;

        static public void setExpression(string exp)
        {
            _regex = new Regex(exp);
        }

        static public bool isMatching(string input)
        {
           return _regex.Match(input).Success;
        }

    }
}
