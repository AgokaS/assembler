using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transliter
{
    class Lyxsemma
    {
        public enum Type 
        {
            identifier,
            spliter,
            literal,
        }

        public enum Token
        {
            _operator,
            _name,
            _none
        }
        
        private static string[] sTypeText = new string[3] { "\t индефикатор", "\t разделитель", "\t литерал" };
        private static string[] sTokenText = new string[] { "\t оператор", "\t имя_переменной", "" };

        public readonly Type type;
        private Token token = Token._none;
        public readonly string context;

        public int mToken
        {
            get { return (int)token;}
            set { token = (Token)value; }
        }

        public Lyxsemma(string text, Type type)
        {
            this.context = text;
            this.type = type;
        }

        public string Print()
        {
            return context + sTypeText[(int)type ] + sTokenText[(int)token];
        }

    }
}
