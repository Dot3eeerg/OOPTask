using DataObjects;
using Interfaces.Function;

namespace Functions;

public class PiecewiseLinearFunction : IParametricFunction<IDifferentialFunction>
{
    public IDifferentialFunction Bind(IVector parameters)
    {
        if (parameters.Count < 1)
            throw new ArgumentException("List of coefficients cannot be empty");

        return new InternalPiecewiseLinearFunction(parameters);
    }
    
    private class InternalPiecewiseLinearFunction(IVector parameters) : IDifferentialFunction
    {
        public double Value(IVector point)
        {
            if (parameters.Count < 1)
                throw new ArgumentException("Parameters are not bound");
            if (point.Count != 1)
                throw new ArgumentException("Cannot build polynomial function, dimensions must be 1");

            var x = point[0];
            var n = parameters.Count;
            if (x <= 0) 
                return parameters[0];
            if (x >= n - 1)
                return parameters[n - 1];
            
            var i = (int)Math.Floor(x);
            var t = x - i;
            return parameters[i] * (1 - t) + parameters[i + 1] * t;
        }

        public IVector Gradient(IVector point)
        {
            if (point.Count != 1)
                throw new ArgumentException("The dimensionality of the space does not match the type of function.");
            var x = point[0];
            var n = parameters.Count;
            var grad = new Vector
            {
                Capacity = n
            };
            for (int j = 0; j < n; j++)
            {
                grad.Add(0);
            }

            if (x <= 0)
            {
                grad[0] = 1.0;
                return grad;
            }

            if (x >= n - 1)
            {
                grad[n - 1] = 1.0;
                return grad;
            }
            
            var i = (int)Math.Floor(x);
            var t = x - i;
            grad[i] = 1 - t;
            grad[i + 1] = t;

            return grad;
        }
    }
}