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
            _name,          
        }

        private static Type_lyxsemm type_lyxsemm;
        
        private static string[][] dictionary_lyxsemms = new string[2][]
        {
            dictionary_lyxsemms[0]= new string[7] { "if", "else" , "for", "to", "do", "begin", "end"},
            dictionary_lyxsemms[1]= new string[2],
        };

        private static bool IsComand(string s_lyxsemm)
        {
            for (int i = 0; i<dictionary_lyxsemms[0].Length; i++)
            {
                if (s_lyxsemm == dictionary_lyxsemms[0][i])
                    return true;
            }
            return false;
        }

        public static void Analyz(List<Lyxsemma> lyxsemmas)
        {
            foreach (Lyxsemma lyxsemm in lyxsemmas)
            {
                if (lyxsemm.type == Lyxsemma.Type.identifier)
                    if (IsComand(lyxsemm.context))
                        type_lyxsemm=Type_lyxsemm._operator;

            }
        }       
    }
}
