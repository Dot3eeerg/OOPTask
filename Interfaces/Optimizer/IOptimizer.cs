using DataObjects;
using Interfaces.Function;
using Interfaces.Functional;

namespace Interfaces.Optimizer;

public interface IOptimizer<TFunctional, TFunction>
    where TFunctional : IFunctional<TFunction>
    where TFunction : IFunction
{
    IVector Minimize(TFunctional functional,
        IParametricFunction<TFunction> function,
        IVector initialParameters,
        IVector? minimumParameter = null,
        IVector? maximumParameter = null);
}