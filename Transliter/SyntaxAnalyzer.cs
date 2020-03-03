using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transliter
{
    class SyntaxAnalyzer
    {
        //ключивые слова оператора
        private static string[] dictionaryToken = new string[] { "if", "else", "then", "begin", "end" };


        public static bool IsComand(string s_lyxsemm)
        {
            for (int i = 0; i < dictionaryToken.Length; i++)
            {
                if (s_lyxsemm == dictionaryToken[i])
                    return true;
            }
            return false;
        }

        public static List<Lyxsemma> Analyz ( List<Lyxsemma> lyxsemmas)
        {
            foreach (Lyxsemma lyxsemm in lyxsemmas)
            {
                if (lyxsemm.type == Lyxsemma.Type.identifier)
                    if (IsComand(lyxsemm.context))
                        lyxsemm.mToken = (int)Lyxsemma.Token._operator;
                    else
                        lyxsemm.mToken = (int)Lyxsemma.Token._name;
            }
            return lyxsemmas;
        }
    }
}
