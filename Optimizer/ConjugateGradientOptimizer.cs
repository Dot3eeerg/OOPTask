using DataObjects;
using Interfaces.Function;
using Interfaces.Functional;
using Interfaces.Optimizer;

namespace Optimizer;

public class ConjugateGradientOptimizer : IOptimizer<IDifferentiableFunctional, IDifferentialFunction>
{
    public int MaxIter = 10000;
    public double Alpha = 1e-3;
    public double Tolerance = 1e-6;

    public IVector Minimize(IDifferentiableFunctional functional, IParametricFunction<IDifferentialFunction> function,
        IVector initialParameters, IVector? minimumParameters = null, IVector? maximumParameters = null)
    {
        var p = new Vector();
        foreach (var v in initialParameters) p.Add(v);

        var f = function.Bind(p);
        var g = functional.Gradient(f);

        var d = new Vector();
        foreach (var gi in g) d.Add(-gi);

        for (int iter = 0; iter < MaxIter; iter++)
        {
            double gnorm = 0;
            foreach (var gi in g) gnorm += gi * gi;
            gnorm = Math.Sqrt(gnorm);
            if (gnorm < Tolerance) break;

            // простой шаг по направлению сопряжённого градиента
            for (int i = 0; i < p.Count; i++)
                p[i] += Alpha * d[i];

            f = function.Bind(p);
            var newG = functional.Gradient(f);

            double num = 0, den = 0;
            for (int i = 0; i < g.Count; i++)
            {
                num += newG[i] * newG[i];
                den += g[i] * g[i];
            }
            double beta = den == 0 ? 0 : num / den;

            for (int i = 0; i < d.Count; i++)
                d[i] = -newG[i] + beta * d[i];

            g = newG;
        }

        return p;
    }
}