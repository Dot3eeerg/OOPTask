using DataObjects;
using Interfaces.Function;

namespace Interfaces.Functional;

public interface ILeastSquareFunctional : IFunctional<IDifferentiableFunctional>
{
    IVector Residual(IDifferentialFunction function);
    IMatrix Jacobian(IDifferentialFunction function); 
}