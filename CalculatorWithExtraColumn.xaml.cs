using System.Data;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculated
{
    /// <summary>
    /// Interaction logic for CalculatorWithExtraColumn.xaml
    /// </summary>
    public partial class CalculatorWithExtraColumn : Window
    {
        public CalculatorWithExtraColumn()
        {
            InitializeComponent();

            CultureInfo customCulture = new CultureInfo("en-US");
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture = customCulture;
            Thread.CurrentThread.CurrentUICulture = customCulture;

            foreach (var el in ButtonsGrid.Children)
            {
                if (el is Button btn)
                {
                    btn.Click += ButtonClick;
                }
            }
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            var symbol = ((Button)e.OriginalSource).Content.ToString();

            if (symbol == "C")
            {
                output.Clear();
            }
            else if (symbol == "⌫")
            {
                if (output.Text.Length != 0)
                {
                    output.Text = output.Text[..^1];
                }
            }
            else if (symbol == "CE")
            {
                for (var i = output.Text.Length - 1; i >= 0; i--)
                {
                    if (output.Text[i] == '-' || output.Text[i] == '+' || output.Text[i] == '*' || output.Text[i] == '/')
                    {
                        output.Text = output.Text[..i];
                        break;
                    }
                }
            }
            else if (symbol == "=")
            {
                if (output.Text.Trim().Split().Length > 2)
                {
                    output.Text = new DataTable().Compute(output.Text, null).ToString();
                }
            }

            else if (symbol == "-" || symbol == "+" || symbol == "*" || symbol == "/")
            {
                if (output.Text.Length != 0)
                {
                    if (output.Text[^1] == '-' || output.Text[^1] == '+' || output.Text[^1] == '*' || output.Text[^1] == '/')
                    {
                        output.Text = output.Text[..^1] + symbol;
                    }
                    else
                    {
                        output.Text += $" {symbol} ";
                    }
                }
            }
            else if (symbol == "㊂")
            {
                this.Hide();
                MainWindow newWindow = new MainWindow();
                newWindow.Left = this.Left;
                newWindow.Top = this.Top;
                newWindow.Show();
            }
            else if (symbol == "log")
            {
                if (!string.IsNullOrWhiteSpace(output.Text))
                {
                    double number = double.Parse(output.Text);
                    output.Text = Math.Log10(number).ToString();
                }
            }
            else if (symbol == "^")
            {
                output.Text += "^";
            }
            else if (symbol == "sqrt")
            {
                if (!string.IsNullOrWhiteSpace(output.Text))
                {
                    double number = double.Parse(output.Text);
                    output.Text = Math.Sqrt(number).ToString();
                }
            }
            else
            {
                output.Text += symbol;
            }
        }
    }
}
