using DataObjects;

namespace Interfaces.Functional;

public interface IDifferentiableFunctional : IFunctional<IDifferentiableFunctional>
{
    IVector Gradient(IDifferentiableFunctional function);
}
