using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Transliter          
{
    class Selector
    {
        private enum Condition
        {           
            selecting_identifier =Lyxsemma.Type.identifier,
            selecting_spliter = Lyxsemma.Type.spliter,
            selecting_literal = Lyxsemma.Type.literal,
            selecting_none,
        }

        private static Condition _condition = Condition.selecting_none;

        public static List<Lyxsemma> SelectKeyWord(string input_text)
        {
            try
            {
                string _word = "";
                List<Lyxsemma> selectedWords = new List<Lyxsemma>();
                int i = 0;
                while (i < input_text.Length)
                {
                    if (input_text[i] == ' ')
                    {
                        if (_condition == Condition.selecting_none || _condition == Condition.selecting_spliter)
                        {
                            i++;
                            continue;
                        }
                        else
                        {
                            CreateLyxswmma(ref _word, selectedWords);
                            _condition = Condition.selecting_none;
                        }
                    }
                    else
                    {
                        if (_word.Length < 8)
                        {
                            int asciiDecCode = Convert.ToInt32(input_text[i]);
                            switch (_condition)
                            {
                                case Condition.selecting_none:
                                    {
                                        _condition = IsNumber(asciiDecCode) ? Condition.selecting_literal : IsLetter(asciiDecCode) ? Condition.selecting_identifier : Condition.selecting_spliter;
                                        _word += input_text[i].ToString();

                                        break;
                                    }

                                case Condition.selecting_identifier:
                                    {
                                        if (IsSymbol(asciiDecCode))
                                        {
                                            CreateLyxswmma(ref _word, selectedWords);
                                            _condition = Condition.selecting_none;
                                            continue;
                                        }
                                        else
                                        {
                                            _word += input_text[i].ToString();                                         
                                        }
                                        break;
                                    }

                                case Condition.selecting_literal:
                                    {
                                        if (IsNumber(asciiDecCode))
                                        {
                                            _word += input_text[i].ToString();
                                        }
                                        else if (IsSymbol(asciiDecCode))
                                        {
                                            CreateLyxswmma(ref _word, selectedWords);
                                            _condition = Condition.selecting_none;
                                            continue;
                                        }
                                        break;
                                    }

                                case Condition.selecting_spliter:
                                    {
                                        if (IsSymbol(asciiDecCode))
                                        {

                                            if (_word.Length < 2)
                                            {
                                                if (IsStartDoubleSymbol(asciiDecCode) && _word.Length == 0)
                                                {
                                                    _word += input_text[i].ToString();
                                                }
                                                else
                                                {
                                                    _word += input_text[i].ToString();
                                                    CreateLyxswmma(ref _word, selectedWords);
                                                    _condition = Condition.selecting_none;
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show("Неопознаный знак");
                                                Exception e = new Exception();
                                            }
                                            
                                        }
                                        else
                                        {
                                            CreateLyxswmma(ref _word, selectedWords);
                                            _condition = Condition.selecting_none;
                                            continue;
                                        }
                                        break;
                                    }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Размер индефикатора не может быть больше 8 символов");
                            Exception e = new Exception();
                        }
                    }
                    i++; // подготовка к следующей итерации цикла
                }
                CreateLyxswmma(ref _word, selectedWords);//последняя ликсемма 
                return selectedWords;
            }
            catch
            {
                return null;
            }
        }

        private static void CreateLyxswmma(ref string _word, List<Lyxsemma> selectedWords)
        {
            Lyxsemma lyxsemma = new Lyxsemma(_word, (Lyxsemma.Type)_condition);
            selectedWords.Add(lyxsemma);
            _word = "";
        }

        public static List<Lyxsemma> SelectKeyWords(string input_text)
        {
            string _word = "";
            List<Lyxsemma> selectedWords = new List<Lyxsemma>();
            int i = 0;
            while (i < input_text.Length)
            {
                if (input_text[i] == ' ')
                {
                    i++; continue;
                }
                else
                {
                    int dec = Convert.ToInt32(input_text[i]);
                    if (dec > 47 && dec < 58)
                    {
                        _word += input_text[i].ToString();
                        i++;
                        while (i < input_text.Length && input_text[i] != ' ')
                        {
                            int dec2 = Convert.ToInt32(input_text[i]);
                            if (dec2 > 47 && dec2 < 58)
                            {
                                _word += input_text[i].ToString();
                                i++;
                            }
                            else
                            {
                                Exception e = new Exception();
                                return null;
                            }
                        }

                        Lyxsemma lword = new Lyxsemma(_word, Lyxsemma.Type.literal);
                        selectedWords.Add(lword);
                        
                    }
                    else if (dec > 96 && dec <123) 
                    {
                        _word += input_text[i].ToString();
                        i++;
                        while (i < input_text.Length && input_text[i] != ' ' )
                        {
                            int dec2 = Convert.ToInt32(input_text[i]);
                            if ((dec2 > 47 && dec2 < 58) || (dec2 > 96 && dec2 < 123))
                            {
                                _word += input_text[i].ToString();
                                i++;
                            }
                            else
                            {
                                Exception e = new Exception();
                                return null;
                            }
                        }

                        Lyxsemma lword = new Lyxsemma(_word, Lyxsemma.Type.identifier);
                        selectedWords.Add(lword);
                    }
                }
                i++;
                _word = ""; 
            }

            return selectedWords;
        }

        /* Методы проверки типа значения */

        private static bool IsNumber(int asciiDecCode)
        {
            if (asciiDecCode > 47 && asciiDecCode < 58)
                return true;
            else
                return false;
        }

        private static bool IsLetter(int asciiDecCode)
        {
            if (asciiDecCode > 96 && asciiDecCode < 123) 
                return true;
            else
                return false;
        }

        private static bool IsSymbol(int asciiDecCode)
        {
            if ((asciiDecCode > 57 && asciiDecCode < 62) || (asciiDecCode > 39 && asciiDecCode < 44) || asciiDecCode == 33 || asciiDecCode == 45)
                return true;
            else
                return false;
        }

        private static bool IsStartDoubleSymbol(int asciiDecCode)
        {
            if (asciiDecCode == 58)
                return true;
            else
                return false;
        }
    }
}
