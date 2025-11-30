using Interfaces.Function;

namespace Interfaces.Functional;

public interface IFunctional<TFunction> where TFunction : IFunction
{
    double Value(TFunction function);
}