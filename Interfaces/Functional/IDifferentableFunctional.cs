using DataObjects;
using Interfaces.Function;

namespace Interfaces.Functional;

public interface IDifferentiableFunctional : IFunctional<IDifferentialFunction>
{
    IVector Gradient(IDifferentialFunction function);
}
