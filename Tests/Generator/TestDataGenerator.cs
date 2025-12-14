using DataObjects;

namespace Tests.Generator;

public static class TestDataGenerator
{
    public static TheoryData<IVector, IVector, double> GetPolynomialFunctionValueData() =>
        new()
        {
            { new Vector { 1.0, 2.0 }, new Vector { 1.0 }, 3.0 },
            { new Vector { 1.0, 2.0, 3.0 }, new Vector { 2.0 }, 17.0 },
            { new Vector { 1.0, 2.0, 3.0, 4.0 }, new Vector { 3.0 }, 142.0 },
            { new Vector { 1.0, 2.0, 3.0, 4.0, 5.0 }, new Vector { 4.0 }, 1593.0 }
        };
}
