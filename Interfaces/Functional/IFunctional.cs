using Interfaces.Function;

namespace Interfaces.Functional;

public interface IFunctional<TFunction>
{
    double Value(IFunction function);
}