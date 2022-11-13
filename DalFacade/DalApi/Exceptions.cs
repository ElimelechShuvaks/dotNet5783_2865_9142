namespace DalApi;


public class IdNotExistException : Exception
{
  public IdNotExistException(string message) : base(message) { }

}

public class IdExistException : Exception
{
    public IdExistException(string message) : base(message) { }

}