using DataObjects;
using Interfaces.Function;

namespace Functions;

public class PolynomialFunction : IParametricFunction<IFunction>
{
    public IFunction Bind(IVector parameters)
    {
        if (parameters.Count < 1)
            throw new ArgumentException("List of coefficients cannot be empty");
        
        return new InternalPolynomialFunction(parameters);
    }

    private class InternalPolynomialFunction(IVector parameters) : IFunction
    {
        public double Value(IVector point)
        {
            if (parameters.Count < 1)
                throw new ArgumentException("Parameters are not bound");
            if (point.Count != 1)
                throw new ArgumentException("Cannot build polynomial function, dimensions must be 1");
            
            return parameters.Select((coeff, index) => coeff * Math.Pow(point.ElementAt(index), index)).Sum();
        }
    }
}