using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoetryEngine {
    static class Extentions {

        private static char[] Vowels = { 'a', 'e', 'i', 'o', 'u', 'y'};

        public static bool IsVowel(this char c) {
            if(Vowels.Contains(c.GetLowerCase()))
                return true;
            return false;
        }

        public static char GetLowerCase(this char c) {
            if(c > 64 & c < 91)
                return Char.ToLower(c);
            return c;
        }
    }
}
