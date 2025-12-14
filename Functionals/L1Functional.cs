using DataObjects;
using Interfaces.Function;
using Interfaces.Functional;

namespace Functionals;

public class L1Functional : IDifferentiableFunctional
{
    private readonly IList<IVector> _points;
    private readonly IVector _values;
    
    public L1Functional(IList<IVector> points, IVector values)
    {
        _points = points;
        _values = values;
    }

    public double Value(IDifferentialFunction function) => 
        _points
            .Select(function.Value)
            .Select((value, i) => Math.Abs(value - _values[i]))
            .Sum();

    public IVector Gradient(IDifferentialFunction function)
    {
        int gradDim = function.Gradient(_points[0]).Count;
        var gradient = new Vector();
        gradient.AddRange(new double[gradDim]);

        for (int i = 0; i < _points.Count; i++)
        {
            double value = function.Value(_points[i]);
            double diff = value - _values[i];
            var funcGradient = function.Gradient(_points[i]);

            for (int j = 0; j < gradDim; j++)
            {
                gradient[j] += Math.Sign(diff) * funcGradient[j];
            }
        }

        return gradient;
    }
}