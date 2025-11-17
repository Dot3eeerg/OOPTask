namespace DataObjects;

public interface IMatrix : IList<IList<double>> { }

public class Matrix : List<IList<double>>, IMatrix { }