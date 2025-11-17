using DataObjects;

namespace Interfaces.Function;

public interface IFunction
{
    double Value(IVector point);
}