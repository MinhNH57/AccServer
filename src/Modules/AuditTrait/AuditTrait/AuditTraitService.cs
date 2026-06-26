using Marten;
using MediatR;

namespace AuditTrait;

public class AuditTraitService(
    IMediator mediator,
    ISender sender,
    IDocumentSession documentStore)
{
    public IMediator Mediator { get; set; } = mediator;
    public ISender Sender { get; set; } = sender;
    public IDocumentSession DocumentStore { get; set; } = documentStore;

}