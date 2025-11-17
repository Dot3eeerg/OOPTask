using DataObjects;

namespace Interfaces.Function;

public interface IDifferentialFunction : IFunction
{
    IVector Gradient(IVector point);
}