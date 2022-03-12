using System;

public class ContentVersionOutdated : Exception
{
    public ContentVersionOutdated()
    {
    }

    public ContentVersionOutdated(string message)
        : base(message)
    {
    }

    public ContentVersionOutdated(string message, Exception inner)
        : base(message, inner)
    {
    }
}