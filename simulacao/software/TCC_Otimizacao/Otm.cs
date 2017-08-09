using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SolverFoundation.Common;
using Microsoft.SolverFoundation.Solvers;

namespace TCC_Otimizacao
{
    public class Otm
    {
        public void Get()
        {
            SimplexSolver solver = new SimplexSolver();

            double[] estimatedProfitOfProjectX = new double[] { 1, 1.8, 1.6, 0.8, 1.4 };
            double[] capitalRequiredForProjectX = new double[] { 6, 12, 10, 4, 8 };
            double availableCapital = 20;
            int[] chooseProjectX = new int[5];

            int profit;
            solver.AddRow("profit", out profit);
            solver.AddGoal(profit, 0, false);

            int expenditure;
            solver.AddRow("expenditure", out expenditure);
            solver.SetBounds(expenditure, 0, availableCapital);

            for (int i = 0; i < 5; i++)
            {
                solver.AddVariable(string.Format("project{0}", i), out chooseProjectX[i]);
                solver.SetBounds(chooseProjectX[i], 0, 1);
                solver.SetIntegrality(chooseProjectX[i], true);
                solver.SetCoefficient(profit, chooseProjectX[i], estimatedProfitOfProjectX[i]);
                solver.SetCoefficient(expenditure, chooseProjectX[i], capitalRequiredForProjectX[i]);
            }

            SimplexSolverParams param = new SimplexSolverParams();
            param.MixedIntegerGenerateCuts = true;
            solver.Solve(param);

            Console.WriteLine(solver.MipResult);
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Project {0} is {1} selected.", i, solver.GetValue(chooseProjectX[i]) == 1 ? "" : "not ");
            }
            Console.WriteLine("The estimated total profit is: ${0} million.", (double)solver.GetValue(profit).ToDouble());
            Console.WriteLine("The total expenditure is: ${0} million.", solver.GetValue(expenditure).ToDouble());
        }
    }
}
