using FixedAsset.Infrastructure.Idempotency;

namespace FixedAsset.Infrastructure.EntityConfigurations;

internal class ClientRequestEntityTypeConfiguration
    : IEntityTypeConfiguration<ClientRequest>
{
    public void Configure(EntityTypeBuilder<ClientRequest> requestConfiguration)
    {
        requestConfiguration.ToTable("ClientRequests");
    }
}
