namespace FixedAsset.API.Application.Commands;

using FixedAsset.API.Application.Models;

[DataContract]
public class DeleteFixedAssetCommand
    : IRequest<BatchVoucherResponse>
{
    [DataMember]
    private readonly List<FixedAssetDto> _fixedAssets;

    [DataMember]
    public IEnumerable<FixedAssetDto> FixedAssets => _fixedAssets;


    public DeleteFixedAssetCommand()
    {
        _fixedAssets = [];
    }

    public DeleteFixedAssetCommand(List<FixedAssetDto> fixedAssetDtos)
    {
        _fixedAssets = fixedAssetDtos;
    }
}

