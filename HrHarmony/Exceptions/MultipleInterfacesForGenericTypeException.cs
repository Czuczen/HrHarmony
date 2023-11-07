namespace HrHarmony.Exceptions;

public class MultipleInterfacesForGenericTypeException : Exception
{
    public MultipleInterfacesForGenericTypeException()
    {
    }

    public MultipleInterfacesForGenericTypeException(string message)
        : base(message)
    {
    }

    public MultipleInterfacesForGenericTypeException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}