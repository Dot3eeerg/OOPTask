namespace DataObjects;

public interface IVector : IList<double> { }

public class Vector : List<double>, IVector
{
    public Vector() { }
    public Vector(int capacity) : base(capacity) { }
}