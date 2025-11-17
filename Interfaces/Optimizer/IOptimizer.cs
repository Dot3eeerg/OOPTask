using DataObjects;
using Interfaces.Function;
using Interfaces.Functional;

namespace Interfaces.Optimizer;

public interface IOptimizer<TFunctional, TFunction>
    where TFunctional : IFunctional<TFunction>
    where TFunction : IFunction
{
    IVector Mininize(TFunctional functional,
        IParametricFunction<TFunction> function,
        IVector? minimumParameter = default,
        IVector? maximumParameter = default);
}