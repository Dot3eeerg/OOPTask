using DataObjects;
using Interfaces.Function;
using Interfaces.Functional;

namespace Functionals;

sealed class L2Functional : IDifferentiableFunctional, ILeastSquareFunctional
{
    private readonly IList<IVector> _points;
    private readonly IVector _values;

    public double Value(IDifferentialFunction function) => 
        _points
            .Select(function.Value)
            .Select((value, i) => value - _values[i])
            .Sum(diff => diff * diff);

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
                gradient[j] += 2.0 * diff * funcGradient[j];
            }
        }

        return gradient;
    }

    public IMatrix Jacobian(IDifferentialFunction function)
    {
        var jacobian = new Matrix();
        jacobian.AddRange(_points.Select(function.Gradient));
        
        return jacobian;
    }

    public IVector Residual(IDifferentialFunction function)
    {
        var residual = new Vector();
        residual.AddRange(_points
                .Select(function.Value)
                .Select((value, i) => value - _values[i]));
        
        return residual;
    }
}