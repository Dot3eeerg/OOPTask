using DataObjects;
using Functions;
using Interfaces.Function;

namespace Tests.Functions.Common;

public sealed record PiecewiseLinearFunctionFixture
{
    private readonly PiecewiseLinearFunction _function;
    private readonly IVector _parameters;

    public PiecewiseLinearFunctionFixture()
    {
        _parameters = new Vector { 0.0, 2.0, 4.0 };
        _function = new();
    }

    public IDifferentialFunction? Bind() => _function.Bind(_parameters);
}