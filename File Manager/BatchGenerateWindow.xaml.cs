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
using System.Windows.Shapes;

namespace File_Manager
{
    /// <summary>
    /// Interaction logic for BatchGenerateWindow.xaml
    /// </summary>
    public partial class BatchGenerateWindow : Window
    {
        public BatchGenerateWindow()
        {
            InitializeComponent();
        }

        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            string prefix = PrefixBox.Text;
            string suffix = SuffixBox.Text;
            int start, step, amount, width, current;
            string generateString = "";

            int.TryParse(StartBox.Text, out start);
            int.TryParse(StepBox.Text, out step);
            int.TryParse(AmountBox.Text, out amount);
            int.TryParse(WidthBox.Text, out width);

            current = start;
            for (int index = 0; index < amount; index++)
            {
                generateString += prefix + FormatInt(current, width) + suffix + "\n";
                current += step;
            }
            GenerateTextBox.Text = generateString;
        }

        private string FormatInt(int number, int width)
        {
            string formatString = "{0:D" + width + "}";
            return String.Format(formatString, number);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
