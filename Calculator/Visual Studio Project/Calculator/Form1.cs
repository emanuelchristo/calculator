using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {    
        public Form1()
        {
            InitializeComponent();
            inputBox.Text = "0";
            equnBox.Clear();
        }

        #region My Global Variables

        double input = 0.0d;
        double result = 0.0d;

        char lastOperator;
        char currentOperator;

        bool start = true;
        bool equals = false;
        bool plusOrMinus = false;
        bool otherButtonPress = false;

        #endregion

        #region Clearing Methods

        private void ceButton_Click(object sender, EventArgs e)
        {
            this.inputBox.Clear();
        }

        private void delButton_Click(object sender, EventArgs e)
        {
            if (inputBox.Text != "")
            {
                inputBox.Text = inputBox.Text.Remove(inputBox.Text.Length - 1, 1);
                plusOrMinusBackSpaced();
                setCaretInputBox(inputBox.Text.Length);
            }
        }

        private void cButton_Click(object sender, EventArgs e)
        {
            this.equnBox.Clear();
            this.inputBox.Clear();
            resetSomeVars();
        }

        #endregion

        #region Number Button Methods

        private void zeroButton_Click(object sender, EventArgs e)
        {
            enterNumber("0");
        }

        private void oneButton_Click(object sender, EventArgs e)
        {
            enterNumber("1");
        }

        private void twoButton_Click(object sender, EventArgs e)
        {
            enterNumber("2");
        }

        private void threeButton_Click(object sender, EventArgs e)
        {
            enterNumber("3");
        }

        private void fourButton_Click(object sender, EventArgs e)
        {
            enterNumber("4");
        }

        private void fiveButton_Click(object sender, EventArgs e)
        {
            enterNumber("5");
        }

        private void sixButton_Click(object sender, EventArgs e)
        {
            enterNumber("6");
        }

        private void sevenButton_Click(object sender, EventArgs e)
        {
            enterNumber("7");
        }

        private void eightButton_Click(object sender, EventArgs e)
        {
            enterNumber("8");
        }

        private void nineButton_Click(object sender, EventArgs e)
        {
            enterNumber("9");
        }

        private void pointButton_Click(object sender, EventArgs e)
        {
            enterNumber(".");
        }

        private void plusOrMinusButton_Click(object sender, EventArgs e)
        {
            if (plusOrMinus == false)
            {
                if (inputBox.Text != "0")
                    inputBox.Text = inputBox.Text.Insert(0, "-");
                else
                    inputBox.Text = "-";
                plusOrMinus = true;
            }
            else
            {
                int pos = inputBox.SelectionStart;
                if(inputBox.Text != "")
                    inputBox.Text = inputBox.Text.Remove(0,1);
                
                if(pos == 0)
                    setCaretInputBox(inputBox.Text.Length);

                plusOrMinus = false;
            }
        }

        #endregion

        #region Operator Buttons

        private void divideButton_Click(object sender, EventArgs e)
        {
            getInput();
            currentOperator = '/';
            updateEqnBox();
            doOperation();
        }

        private void multiplyButton_Click(object sender, EventArgs e)
        {
            getInput();
            currentOperator = '*';
            updateEqnBox();
            doOperation();
        }

        private void minusButton_Click(object sender, EventArgs e)
        {
            getInput();
            currentOperator = '-';
            updateEqnBox();
            doOperation();
        }

        private void plusButton_Click(object sender, EventArgs e)
        {
            getInput();
            currentOperator = '+';
            updateEqnBox();
            doOperation();
        }

        private void equalsButton_Click(object sender, EventArgs e)
        {
            getInput();
            currentOperator = '=';
            updateEqnBox();
            doOperation();
        }

        #endregion

        #region Other Buttons

        private void percentageButton_Click(object sender, EventArgs e)
        {
            if (lastOperator == '/')
            {
                getInput();
                result = (result * 100) / input;
                updateInputBox(result.ToString());
                resetSomeVars();
                otherButtonPress = true;
            }
        }

        private void oneByButton_Click(object sender, EventArgs e)
        {
            getInput();
            updateInputBox((1/input).ToString());
            updateEqnBox("some");
            otherButtonPress = true;
        }

        private void squareButton_Click(object sender, EventArgs e)
        {
            getInput();
            updateInputBox((input*input).ToString());
            updateEqnBox("some");
            otherButtonPress = true;
        }

        private void sqrtButton_Click(object sender, EventArgs e)
        {
            getInput();
            updateInputBox(Math.Sqrt(input).ToString());
            updateEqnBox("some");
            otherButtonPress = true;
        }

        #endregion

        #region Private Helpers

        // Button clicks to number in input box
        private void enterNumber(string s)
        {
            if (equals == true)
            {
                inputBox.Clear();
                this.inputBox.Text = s;
                setCaretInputBox(inputBox.Text.Length);
                equals = false;
                otherButtonPress = false;
            }
            else
            { 
                if (inputBox.Text != "0")
                    this.inputBox.Text = this.inputBox.Text += s;
                else
                    this.inputBox.Text = s;
                setCaretInputBox(inputBox.Text.Length);
                otherButtonPress = false;
            }
        }

        // Sets the position of caret in input box
        private void setCaretInputBox(int i)
        {
            if(i<=inputBox.Text.Length)
                this.inputBox.SelectionStart = i;
        }

        // Sets the position of caret in equn box
        private void setCaretEqunBox(int i)
        {
            if (i <= equnBox.Text.Length)
                this.equnBox.SelectionStart = i;
        }

        // Handles plus or minus button 
        private void plusOrMinusBackSpaced()
        {
            if (inputBox.Text == "-")
            {
                this.inputBox.Clear();
                plusOrMinus = false;
            }
        }

        // Sets the input box to 0 if it is emptied
        private void inputBox_TextChanged(object sender, EventArgs e)
        {
            if (inputBox.Text == "")
            {
                inputBox.Text = "0";
            }
        }

        // Maps keyboard press to button click
        private void keyboardInput(char ch)
        {
            const char a = (char)127;
            switch (ch)
            {
                case '0':
                    zeroButton.PerformClick();
                    break;
                case '1':
                    oneButton.PerformClick();
                    break;
                case '2':
                    twoButton.PerformClick();
                    break;
                case '3':
                    threeButton.PerformClick();
                    break;
                case '4':
                    fourButton.PerformClick();
                    break;
                case '5':
                    fiveButton.PerformClick();
                    break;
                case '6':
                    sixButton.PerformClick();
                    break;
                case '7':
                    sevenButton.PerformClick();
                    break;
                case '8':
                    eightButton.PerformClick();
                    break;
                case '9':
                    nineButton.PerformClick();
                    break;
                case '\b':
                    delButton.PerformClick();
                    break;
                case a:
                    delButton.PerformClick();
                    break;
                case '*':
                    multiplyButton.PerformClick();
                    break;
                case '/':
                    divideButton.PerformClick();
                    break;
                case '+':
                    plusButton.PerformClick();
                    break;
                case '-':
                    minusButton.PerformClick();
                    break;
                case '.':
                    pointButton.PerformClick();
                    break;
                case 'c':
                    cButton.PerformClick();
                    break;
                case 'C':
                    cButton.PerformClick();
                    break;
                case 'e':
                    ceButton.PerformClick();
                    break;
                case 'E':
                    ceButton.PerformClick();
                    break;
                case '=':
                    equalsButton.PerformClick();
                    break;
            }
        }

        // Key press event
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            keyboardInput(ch);
        }

        // Reset some vars
        private void resetSomeVars()
        {
            start = true;
            result = 0;
            input = 0;
            equals = true;
            equnBox.Clear();
        }

        // Close the app
        private void closeLabel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Window moving
        Point p;

        private void dragPanel_MouseDown(object sender, MouseEventArgs e)
        {
            p = new Point(e.X, e.Y);
        }

        private void dragPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - p.X;
                this.Top += e.Y - p.Y;
            }
        }

        #endregion

        #region Calculation

        // Updates the value of double input
        private void getInput()
        {
            input = Convert.ToDouble(inputBox.Text);
        }

        // Updates the content of equation box
        private void updateEqnBox(string s = "none")
        {
            if (s == "none")
            {
                if (currentOperator != '=')
                {
                    char ch = currentOperator;

                    if (!equnBox.Text.Contains(' '))
                        equnBox.Clear();

                    if (ch == '*')
                        ch = '×';
                    else if (ch == '/')
                        ch = '÷';

                    equnBox.Text += input.ToString() + " " + ch + " ";
                }
                else
                    equnBox.Clear();

                otherButtonPress = false;
            }
            else
            {
                if (otherButtonPress == true)
                {
                    if (!equnBox.Text.Contains(" "))
                        equnBox.Text = inputBox.Text;
                    else
                    {
                        int a = equnBox.Text.LastIndexOf(' ');
                        int l = equnBox.Text.Length - 1 - a;
                        equnBox.Text.Remove(a, l);
                        equnBox.Text += inputBox.Text;
                    }
                }
                else
                    equnBox.Text += inputBox.Text;
            }
        }

        // Updates the content of input box
        private void updateInputBox(string s)
        {
            if (s == "NaN")
                s = "∞";
            inputBox.Text = s;
        }

        // Do substitution
        private void doSubstitution()
        {
            switch (lastOperator)
            {
                case '+':
                    result += input;
                    break;
                case '-':
                    result -= input;
                    break;
                case '*':
                    result *= input;
                    break;
                case '/':
                    result /= input;
                    break;
            }
        }

        // Does the calculation and coordination
        private void doOperation()
        {
            if (currentOperator != '=')
            {
                // Checking whether it is the first operator button click
                if (start == true)
                {
                    result = input;
                    lastOperator = currentOperator;
                    updateInputBox(result.ToString());
                    start = false;
                    equals = true;
                }
                else
                {
                    // Doing operation
                    doSubstitution();
                    lastOperator = currentOperator;
                    updateInputBox(result.ToString());
                    equals = true;
                }
            }
            else
            {
                doSubstitution();
                updateInputBox(result.ToString());
                start = true;
                result = 0;
                input = 0;
                equals = true;
            }
        }

        #endregion

        #region UI

        Color backColor;
        Color textColor;
        Color equnBoxForeColor;
        Color operatorButtonBackColor;
        Color numberButtonBackColor;

        bool darkUi = false;

        private void colorButton_Click(object sender, EventArgs e)
        {
            if (darkUi == true)
            {
                textColor = Color.White;
                equnBoxForeColor = Color.Silver;
                numberButtonBackColor = Color.FromArgb(45,45,45);
                operatorButtonBackColor = Color.FromArgb(30, 30, 30);
                backColor = Color.FromArgb(30,30,30);
                darkUi = false;
            }
            else
            {
                textColor = Color.Black;
                equnBoxForeColor = Color.LightGray;
                numberButtonBackColor = Color.White;
                operatorButtonBackColor = Color.WhiteSmoke;
                backColor = Color.Gainsboro;
                darkUi = true;
            }

            uiColorChange(backColor, textColor, equnBoxForeColor, operatorButtonBackColor, numberButtonBackColor);
        }

        private void uiColorChange(Color backColor, Color textColor, Color equnBoxForeColor, Color operatorButtonBackButton, Color numberButtonBackColor)
        {
            // Setting back colors
            this.BackColor = backColor;
            inputBox.BackColor = backColor;
            equnBox.BackColor = backColor;
            dragPanel.BackColor = backColor;
            calculatorLabel.BackColor = backColor;
            standardLabel.BackColor = backColor;

            // Setting fore colors

            equnBox.ForeColor = equnBoxForeColor;

            standardLabel.ForeColor = textColor;
            calculatorLabel.ForeColor = textColor;
            inputBox.ForeColor = textColor;
            
            oneButton.ForeColor = textColor;
            twoButton.ForeColor = textColor;
            threeButton.ForeColor = textColor;
            fourButton.ForeColor = textColor;
            fiveButton.ForeColor = textColor;
            sixButton.ForeColor = textColor;
            sevenButton.ForeColor = textColor;
            eightButton.ForeColor = textColor;
            nineButton.ForeColor = textColor;
            zeroButton.ForeColor = textColor;
            pointButton.ForeColor = textColor;
            plusOrMinusButton.ForeColor = textColor;
            equalsButton.ForeColor = textColor;
            plusButton.ForeColor = textColor;
            minusButton.ForeColor = textColor;
            multiplyButton.ForeColor = textColor;
            divideButton.ForeColor = textColor;
            delButton.ForeColor = textColor;
            cButton.ForeColor = textColor;
            ceButton.ForeColor = textColor;
            oneByButton.ForeColor = textColor;
            squareButton.ForeColor = textColor;
            percentageButton.ForeColor = textColor;
            sqrtButton.ForeColor = textColor;

            // Setting number buttons back color
            oneButton.BackColor = numberButtonBackColor;
            twoButton.BackColor = numberButtonBackColor;
            threeButton.BackColor = numberButtonBackColor;
            fourButton.BackColor = numberButtonBackColor;
            fiveButton.BackColor = numberButtonBackColor;
            sixButton.BackColor = numberButtonBackColor;
            sevenButton.BackColor = numberButtonBackColor;
            eightButton.BackColor = numberButtonBackColor;
            nineButton.BackColor = numberButtonBackColor;
            zeroButton.BackColor = numberButtonBackColor;

            // Setting operator button back color
            pointButton.BackColor = operatorButtonBackColor;
            plusOrMinusButton.BackColor = operatorButtonBackColor;
            equalsButton.BackColor = operatorButtonBackColor;
            plusButton.BackColor = operatorButtonBackColor;
            minusButton.BackColor = operatorButtonBackColor;
            multiplyButton.BackColor = operatorButtonBackColor;
            divideButton.BackColor = operatorButtonBackColor;
            delButton.BackColor = operatorButtonBackColor;
            cButton.BackColor = operatorButtonBackColor;
            ceButton.BackColor = operatorButtonBackColor;
            oneByButton.BackColor = operatorButtonBackColor;
            squareButton.BackColor = operatorButtonBackColor;
            percentageButton.BackColor = operatorButtonBackColor;
            sqrtButton.BackColor = operatorButtonBackColor;

            // Close label
            closeLabel.BackColor = backColor;
            closeLabel.ForeColor = textColor;
        }

        #endregion
    }
}