using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPE200Lab1
{
    public partial class MainForm : Form
    {
        private bool hasDot;
        private bool isAllowBack;
        private bool isAfterOperater;
        private bool isAfterEqual, pcFlag = false , isAfterEqual2 ;
        private string firstOperand, secondOperand, result;
        private string operate, tempOperate;
        private double memNum;

        CalculatorEngine ce = new CalculatorEngine();

        private void resetAll()
        {
            lblDisplay.Text = "0";
            isAllowBack = true;
            hasDot = false;
            isAfterOperater = false;
            isAfterEqual = false;
            firstOperand = null;
            secondOperand = null;
        }

        public MainForm()
        {
            InitializeComponent();

            resetAll();
        }

        private void btnNumber_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                resetAll();
            }
            if (isAfterOperater)
            {
                lblDisplay.Text = "0";
            }
            if (lblDisplay.Text.Length is 8)
            {
                return;
            }
            isAllowBack = true;
            string digit = ((Button)sender).Text;
            if (lblDisplay.Text is "0")
            {
                lblDisplay.Text = "";
            }
            lblDisplay.Text += digit;
            isAfterOperater = false;
        }

        private void btnOperator_Click(object sender, EventArgs e)
        {
            operate = ((Button)sender).Text;

            if (firstOperand == "0" || firstOperand == null)
            {
                    firstOperand = lblDisplay.Text;
                    result = firstOperand;
            }
            else
            {
               if(isAfterEqual){
                    firstOperand = lblDisplay.Text ;
               }
               else{
                    secondOperand = lblDisplay.Text;
               }

                result = ce.calculate(operate, firstOperand, secondOperand);
            }

            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (result == "E" || result.Length > 8)
            {
                lblDisplay.Text = "Error";
            }
            else
            {
                lblDisplay.Text = result;
            }

            switch (operate)
            {
                case "+":
                case "-":
                case "X":
                case "÷":
                    firstOperand = lblDisplay.Text;
                    isAfterOperater = true;
                    tempOperate = operate;
                    break;
                case "%":
                    pcFlag = true;
                    lblDisplay.Text = ce.calculate(operate, firstOperand, secondOperand);

                    break;
                case "1/x":
                case "√":
                    firstOperand = lblDisplay.Text;
                    result = ce.calculate(operate, firstOperand, "0");
                    lblDisplay.Text = result;
                    //secondOperand = result;
                    break;
            }
            hasDot = false ;
            isAllowBack = false;

        }

        private void btnEqual_Click(object sender, EventArgs e)
        {

           
            if (pcFlag)
            {
                //lblDisplay.Text = "sample";return;
                result = lblDisplay.Text;
                //lblDisplay.Text = "::Operate::" + tempOperate + "::firstOper::" + firstOperand + "::Now::" + lblDisplay.Text;
                lblDisplay.Text = ce.calculate(tempOperate, firstOperand, result);
                result = lblDisplay.Text; return;
            }
            if (lblDisplay.Text is "Error")
            {
                return;
            }
           // secondOperand = lblDisplay.Text;
           // result = ce.calculate(operate, firstOperand, secondOperand);

                         if (firstOperand == "0" || firstOperand == null)
            {
                if(isAfterEqual){
                        return ;
                 }else{
                    firstOperand = lblDisplay.Text;
                    
                  }result = firstOperand;
            }
            else
            {
                if(isAfterEqual){
                    firstOperand = lblDisplay.Text ;
                }
                else{
                secondOperand = lblDisplay.Text;
                }

            result = ce.calculate(operate, firstOperand, secondOperand);
            }

            if (result is "E" || result.Length > 8)
            {
                lblDisplay.Text = "Error";
            }
            else
            {
                lblDisplay.Text = result;
            }
            firstOperand = result;
            isAfterEqual = true;
            
        }

        private void btnDot_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                resetAll();
            }
            if (lblDisplay.Text.Length is 8)
            {
                return;
            }
            if (!hasDot)
            {
                lblDisplay.Text += ".";
                hasDot = true;
            }
        }

        private void btnSign_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                resetAll();
            }
            // already contain negative sign
            if (lblDisplay.Text.Length is 8)
            {
                return;
            }
            if (lblDisplay.Text[0] is '-')
            {
                lblDisplay.Text = lblDisplay.Text.Substring(1, lblDisplay.Text.Length - 1);
            }
            else
            {
                if (lblDisplay.Text is "0")
                {
                    lblDisplay.Text = "";
                }
                lblDisplay.Text = "-" + lblDisplay.Text;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            resetAll();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                return;
            }
            if (!isAllowBack)
            {
                return;
            }
            if (lblDisplay.Text != "0")
            {
                string current = lblDisplay.Text;
                char rightMost = current[current.Length - 1];
                if (rightMost is '.')
                {
                    hasDot = false;
                }
                lblDisplay.Text = current.Substring(0, current.Length - 1);
                if (lblDisplay.Text is "" || lblDisplay.Text is "-")
                {
                    lblDisplay.Text = "0";
                }
            }
        }

        private void lblDisplay_Click(object sender, EventArgs e)
        {

        }

        private void btnMemFuction(object sender, EventArgs e)
        {
            String operateMem = ((Button)sender).Text;
            double newNum = 0;
            newNum = Convert.ToDouble(lblDisplay.Text);
            isAfterOperater = true;
            switch (operateMem)
            {
                case "M+": memNum += newNum; break;
                case "M-": memNum -= newNum; break;
                case "MR": lblDisplay.Text = memNum.ToString(); break;
                case "MC":lblDisplay.Text = memNum.ToString() ; memNum = 0; break;
                case "MS": memNum = Convert.ToDouble(lblDisplay.Text); break;
            }
        }
    }
}
