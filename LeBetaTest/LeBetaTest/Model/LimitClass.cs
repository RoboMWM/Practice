using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeBetaTest.Model
{
    public class LimitClass
    {
        public double LimitResult { get; set; }
        public double approach;
        Eval evalclass = new Eval();

        public LimitClass()
        {
            LimitResult = 0.0;
        }

        public void CalculateExpression(string expression)
        {
            //If I knew code conventions I'd make this a non-void method with a try and catch statement to return errors... I think
            //Or I could just set it to NaN and if/else check it in code...
            try
            {
                LimitResult = evalclass.Execute(expression);
            }
            catch
            {
                LimitResult = double.NaN;
            }
        }
        protected void ProcessSymbol(object sender, SymbolEventArgs e)
        {
            if (String.Compare(e.Name, "x", true) == 0)
            {
                e.Result = Math.PI;
            }
            // Unknown symbol name
            else e.Status = SymbolStatus.UndefinedSymbol;
        }

        public void CalculateLimit(string expression, double approachInput, int side)
        {
            double approach = approachInput;
            double x;
            try
            {
                for (int i = 1; i < 100; i++)
                {
                    x = (approach + approach / i);
                }

            }
            catch
            {
                LimitResult = double.NaN;
            }
        }
    }
}
