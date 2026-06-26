using System.Collections.ObjectModel;
using System.Net;

namespace SmartAccCloud.Application.Exceptions;

public class NotFoundException : SmartException
{
    public NotFoundException(string message)
        : base(message, new Collection<string>(), HttpStatusCode.NotFound)
    {
    }
}