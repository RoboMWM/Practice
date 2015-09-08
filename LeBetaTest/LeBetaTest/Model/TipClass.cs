using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeBetaTest.Model
{
    public class TipClass
    {
        public string BillAmount { get; set; }
        public string TipAmount { get; set; }
        public string TotalAmount { get; set; }
        public string InputTipAmount { get; set; }
        public string Equation { get; set; }

        public TipClass()
        {
            this.BillAmount = String.Empty;
            this.TipAmount = String.Empty;
            this.TotalAmount = String.Empty;
        }
        public void CalculateTip(string originalAmount, string tipPercentage)
        {
            double billAmount = 0.0;
            double tipAmount = 0.0;
            double tipPercentageConverted = 0.0;
            double totalAmount = 0.0;

            if (double.TryParse(originalAmount.Replace('$', ' '), out billAmount))
            {
                if (double.TryParse(tipPercentage.Replace('%', ' '), out tipPercentageConverted))
                {
                    tipAmount = billAmount * (tipPercentageConverted/100);
                    totalAmount = billAmount + tipAmount;
                }
            }

            this.BillAmount = String.Format("{0:C}", billAmount);
            this.TipAmount = String.Format("{0:C}", tipAmount);
            this.TotalAmount = String.Format("{0:C}", totalAmount);
            this.InputTipAmount = String.Format("{0:0%}", tipPercentageConverted/100);
            this.Equation = ("Inputs: \nbill amount: " + originalAmount + "\ntip percentage: " + tipPercentage + "\nEquation: " + billAmount + " * " + (tipPercentageConverted/100) + " = " + tipAmount +
                "\n" + billAmount + " + " + tipAmount + " = " + totalAmount);
        }
    }
}
