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
        private static string[] sTypeText = new string[3] { " индефикатор", " разделитель", " литерал" };

        public readonly Type type;
        public readonly string context;

        public Lyxsemma(string text, Type type)
        {
            this.context = text;
            this.type = type;
        }

        public string Print()
        {
            return context + sTypeText[(int)type];
        }

    }
}
