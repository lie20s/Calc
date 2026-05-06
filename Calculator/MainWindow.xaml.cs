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
using CalcLIB;
namespace Calculator
{
    public partial class MainWindow : Window
    {
        private double currentValue = 0;
        private string currentOperator = "";
        private bool isNewEntry = true;
        private string expression = "";

        public MainWindow()
        {
            InitializeComponent();
            txtDisplay.Text = "0";
        }

        private void Number_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string number = button.Content.ToString();

            if (isNewEntry)
            {
                txtDisplay.Text = number;
                isNewEntry = false;
            }
            else
            {
                if (txtDisplay.Text == "0" || txtDisplay.Text == "-0")
                    txtDisplay.Text = number;
                else
                    txtDisplay.Text += number;
            }
        }

        private void Operator_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string op = button.Content.ToString();

            string displayOp = GetDisplayOperator(op);
            if (!isNewEntry && currentOperator != "")
            {
                try
                {
                    double secondNumber = double.Parse(txtDisplay.Text);
                    double result = Class1.Execute(currentValue, currentOperator[0], secondNumber);
                    currentValue = result;
                    expression = currentValue.ToString() + " " + displayOp + " ";
                    txtDisplay.Text = expression;
                    currentOperator = op;
                    isNewEntry = true;
                }
                catch (DivideByZeroException)
                {
                    txtDisplay.Text = "Ошибка: деление на 0";
                    ClearAll();
                }
                catch (Exception)
                {
                    txtDisplay.Text = "Ошибка";
                    ClearAll();
                }
            }
            else if (!isNewEntry)
            {
                currentValue = double.Parse(txtDisplay.Text);
                currentOperator = op;
                expression = txtDisplay.Text + " " + displayOp + " ";
                txtDisplay.Text = expression;
                isNewEntry = true;
            }
            else if (currentOperator != "")
            {
                currentOperator = op;
                if (expression.Length > 0)
                {
                    string[] parts = expression.Split(' ');
                    if (parts.Length >= 2)
                    {
                        expression = parts[0] + " " + displayOp + " ";
                        txtDisplay.Text = expression;
                    }
                }
            }
        }

        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            if (currentOperator != "")
            {
                try
                {
                    double secondNumber = 0;
                    string currentDisplay = txtDisplay.Text;

                    if (currentDisplay.Contains(" "))
                    {
                        string[] parts = currentDisplay.Split(' ');
                        secondNumber = double.Parse(parts[parts.Length - 1]);
                    }
                    else
                    {
                        secondNumber = double.Parse(currentDisplay);
                    }

                    double result = Class1.Execute(currentValue, currentOperator[0], secondNumber);
                    txtDisplay.Text = currentValue.ToString() + " " + GetDisplayOperator(currentOperator) + " " + secondNumber.ToString() + " = " + result.ToString();
                    currentValue = result;
                    currentOperator = "";
                    expression = "";
                    isNewEntry = true;
                }
                catch (DivideByZeroException)
                {
                    txtDisplay.Text = "Ошибка: деление на 0";
                    ClearAll();
                }
                catch (Exception)
                {
                    txtDisplay.Text = "Ошибка";
                    ClearAll();
                }
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ClearAll();
        }

        private void Decimal_Click(object sender, RoutedEventArgs e)
        {
            if (isNewEntry)
            {
                txtDisplay.Text = "0,";
                isNewEntry = false;
            }
            else if (!txtDisplay.Text.Contains(","))
            {
                txtDisplay.Text += ",";
            }
        }

        private void Sign_Click(object sender, RoutedEventArgs e)
        {
            if (!isNewEntry)
            {
                double currentNumber = double.Parse(txtDisplay.Text);
                currentNumber = -currentNumber;
                txtDisplay.Text = currentNumber.ToString();
            }
            else if (expression != "")
            {
                string[] parts = expression.Split(' ');
                if (parts.Length >= 2)
                {
                    double lastNumber = double.Parse(parts[parts.Length - 1]);
                    lastNumber = -lastNumber;
                    parts[parts.Length - 1] = lastNumber.ToString();
                    expression = string.Join(" ", parts);
                    txtDisplay.Text = expression;
                }
            }
            else
            {
                txtDisplay.Text = "-0";
                isNewEntry = false;
            }
        }

        private string GetDisplayOperator(string op)
        {
            switch (op)
            {
                case "*": return "×";
                case "/": return "÷";
                case "^": return "^";
                default: return op;
            }
        }
        private void ClearAll()
        {
            txtDisplay.Text = "0";
            currentValue = 0;
            currentOperator = "";
            expression = "";
            isNewEntry = true;
        }
    }
}