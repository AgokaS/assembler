using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transliter
{
    class SyntaxAnalyzer
    {
        private enum Type_lyxsemm
        {
            _operator,
            _keyWordLanguage,
            
        }

        private Type_lyxsemm type_Lyxsemm;
        
        private static string[][] dictionary_lyxsemms = new string[2][]
        {
            dictionary_lyxsemms[0]= new string[3] { "if", "else" , "for"},
            dictionary_lyxsemms[0]= new string[2],
        };

        public static void Analyz(List<Lyxsemma> lyxsemmas)
        {

        }

        
    }
}
