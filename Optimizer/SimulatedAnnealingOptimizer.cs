using DataObjects;
using Interfaces.Function;
using Interfaces.Functional;
using Interfaces.Optimizer;

namespace Optimizer;

sealed class SimulatedAnnealingOptimizer : IOptimizer<IFunctional<IFunction>, IFunction>
{
    private readonly Random _random = new Random();

    public IVector Minimize(IFunctional<IFunction> functional, IParametricFunction<IFunction> function, IVector initialParameters,
        IVector? minimumParameters = default, IVector? maximumParameters = default)
    {
        int n = initialParameters.Count;
        var current = new Vector();
        for (int i = 0; i < n; i++) current.Add(initialParameters[i]);

        var best = new Vector();
        for (int i = 0; i < n; i++) best.Add(current[i]);

        double currentValue = functional.Value(function.Bind(current));
        double bestValue = currentValue;

        double t = 1.0;       // начальная "температура"
        double tMin = 1e-6;  // минимальная температура
        double alpha = 0.9;   // коэффициент охлаждения

        while (t > tMin)
        {
            for (int iter = 0; iter < 100; iter++)
            {
                // генерируем новое решение
                var candidate = new Vector();
                for (int i = 0; i < n; i++)
                {
                    double min = minimumParameters != null ? minimumParameters[i] : current[i] - 1.0;
                    double max = maximumParameters != null ? maximumParameters[i] : current[i] + 1.0;
                    double delta = (_random.NextDouble() * 2 - 1) * 0.1 * (max - min); // случайное смещение
                    double newVal = current[i] + delta;
                    newVal = Math.Max(min, Math.Min(max, newVal)); // ограничиваем диапазон
                    candidate.Add(newVal);
                }

                double candidateValue = functional.Value(function.Bind(candidate));
                double dE = candidateValue - currentValue;

                // принимаем решение по вероятности
                if (dE < 0 || Math.Exp(-dE / t) > _random.NextDouble())
                {
                    current = candidate;
                    currentValue = candidateValue;
                }

                // сохраняем лучшее
                if (currentValue < bestValue)
                {
                    best = new Vector();
                    for (int i = 0; i < n; i++) best.Add(current[i]);
                    bestValue = currentValue;
                }
            }

            t *= alpha; // охлаждение
        }

        return best;
    }
}