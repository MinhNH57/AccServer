using System.Collections.ObjectModel;
using System.Net;

namespace BuildingBlocks.Exceptions;

public class ForbiddenException : SmartException
{
    public ForbiddenException()
        : base("unauthorized", new Collection<string>(), HttpStatusCode.Forbidden)
    {
    }

    public ForbiddenException(string message)
        : base(message, new Collection<string>(), HttpStatusCode.Forbidden)
    {
    }
}