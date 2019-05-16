using System;
public class CRTException : Exception
{
    public CRTException()
        : base()
    {
    }

    public CRTException(string message)
        : base(message)
    {
    }

    public CRTException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
