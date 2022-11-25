
namespace BO;

public class BlExceptions : Exception
{
    public BlExceptions(string? message) : base(message) { }
}

public class IdNotExistException : BlExceptions
{
    public IdNotExistException(string message) : base(message) { }
}

public class IdExistException : BlExceptions
{
    public IdExistException(string message) : base(message) { }
}

public class IdNotValid : BlExceptions
{
    public IdNotValid(string message) : base(message) { }
}
