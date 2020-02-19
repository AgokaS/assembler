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
            
            string _word = "";
            List<Lyxsemma> selectedWords = new List<Lyxsemma>();
            int i = 0;

            try
            {
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
                                        IdentifyType(input_text, ref _word, i, asciiDecCode);
                                        break;
                                    }

                                case Condition.selecting_identifier:
                                {
                                    if (IsSymbol(asciiDecCode))
                                    {
                                        CreateLyxswmma(ref _word, selectedWords);
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
                                        continue;
                                    }
                                    break;
                                }

                                case Condition.selecting_spliter:
                                {
                                    if (IsSymbol(asciiDecCode))
                                    {
                                        if (IsStartDoubleSymbol(asciiDecCode) && _word.Length == 0)
                                        {
                                            _word += input_text[i].ToString();
                                        }
                                        else
                                        {
                                            _word += input_text[i].ToString();
                                            CreateLyxswmma(ref _word, selectedWords);
                                        }
                                            
                                            
                                    }
                                    else
                                    {
                                        CreateLyxswmma(ref _word, selectedWords);
                                        continue;
                                    }
                                    break;
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Размер индефикатора не может быть больше 8 символов");
                            throw new Exception("Превышен размер длинны индефикатора");
                        }
                    }
                    i++; // подготовка к следующей итерации цикла
                }
                CreateLyxswmma(ref _word, selectedWords);//последняя ликсемма 
                return selectedWords;
            }
            catch 
            {
                return new List<Lyxsemma>();
            }
        }

        private static void IdentifyType(string input_text, ref string _word, int i, int asciiDecCode)
        {
            if (IsNumber(asciiDecCode))
            {
                _condition = Condition.selecting_literal;
            }
            else if (IsLetter(asciiDecCode))
            {
                _condition = Condition.selecting_identifier;
            }
            else if (IsSymbol(asciiDecCode))
            {
                _condition = Condition.selecting_spliter;
            }
            else
            {
                MessageBox.Show($"Недопустимый символ {input_text[i]}");
                throw new Exception("Недопустимый знак");
            }
            _word += input_text[i].ToString();
        }

        private static void CreateLyxswmma(ref string _word, List<Lyxsemma> selectedWords)
        {
            Lyxsemma lyxsemma = new Lyxsemma(_word, (Lyxsemma.Type)_condition);
            selectedWords.Add(lyxsemma);
            _word = "";
            _condition = Condition.selecting_none;
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
