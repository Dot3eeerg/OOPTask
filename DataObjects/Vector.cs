namespace DataObjects;

public interface IVector : IList<double> { }

public class Vector : List<double>, IVector { }