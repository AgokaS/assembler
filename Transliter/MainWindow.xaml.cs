using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Transliter
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            StartDebag();
        }

        private void StartDebag()
        {
            var text = "for i : = 0 to 5 do  if (i<4) ";
            var selectedWord = Selector.SelectKeyWord(text);
            SyntaxAnalyzer.Analyz(selectedWord);
            string o_Text = "";
            foreach (var lyxsemm in selectedWord)
            {
                o_Text += lyxsemm.Print() + "\n";
            }
            ctxt_Text.Text = o_Text;
        }

        private void B_inputText_Click(object sender, RoutedEventArgs e)
        {
            var text = t_Input.Text;
            var selectedWord = Selector.SelectKeyWord(text);
            SyntaxAnalyzer.Analyz(selectedWord);
            string o_Text = "";
            foreach (var lyxsemm in selectedWord)
            {
                o_Text += lyxsemm.Print() + "\n";
            }
            ctxt_Text.Text = o_Text;
        }
    }
}
