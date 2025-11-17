using DataObjects;

namespace Interfaces.Function;

public interface IParametricFunction<out TFunction> where TFunction : IFunction
{
    TFunction Bind(IVector parameters);
}