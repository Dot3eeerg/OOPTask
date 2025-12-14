using DataObjects;
using FluentAssertions;
using FluentAssertions.Equivalency;
using Functionals;
using Functions;
using Optimizer;

namespace Tests.OptimizerTests;

public sealed class OptimizerTests(PiecewiseLinearFunction linearFunction) : IClassFixture<PiecewiseLinearFunction>
{
    private static EquivalencyOptions<double> ConfigureEquivalencyOptions(EquivalencyOptions<double> options,
        double eps) =>
        options.Using<double>(ctx =>
            ctx.Subject.Should().BeApproximately(ctx.Expectation, eps)).WhenTypeIs<double>();
    
    [Fact]
    public void Minimize_ConjugateGradientOptimizer_ShouldReturnCorrectResult()
    {
        // Arrange
        var expectedParameters = new Vector { 2.0, 6.5 };
        var l2Functional = new L2Functional( points: new List<IVector> { new Vector { 5.0 }, new Vector { 6.0 } },
            values: new Vector { 6.0, 7.0 });
        var functionParameters = new Vector { 2.0, 3.0 };
        var optimizer = new ConjugateGradientOptimizer();
        
        // Act
        var result = optimizer.Minimize(l2Functional, linearFunction, functionParameters);
        
        // Assert
        result
            .Should()
            .BeEquivalentTo(expectedParameters, options => ConfigureEquivalencyOptions(options, optimizer.Tolerance));
    }
    
    // Cannot write test for SimulatedAnnealingOptimizer, because it isn't deterministic
}