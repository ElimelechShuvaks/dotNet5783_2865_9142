
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

public class IdNotValidException : BlExceptions
{
    public IdNotValidException(string message) : base(message) { }
}

public class NotValidProductException : BlExceptions
{
    public NotValidProductException(string message) : base(message) { }
}
