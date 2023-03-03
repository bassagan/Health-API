public class HealthException : Exception
{
    public HealthException()
    {
    }

    public HealthException(string message)
        : base(message)
    {
    }

    public HealthException(string message, Exception inner)
        : base(message, inner)
    {
    }
}