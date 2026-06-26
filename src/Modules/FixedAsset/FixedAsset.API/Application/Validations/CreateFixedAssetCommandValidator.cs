namespace FixedAsset.API.Application.Validations;

public class CreateFixedAssetCommandValidator : AbstractValidator<CreateFixedAssetCommand>
{
    public CreateFixedAssetCommandValidator(ILogger<CreateFixedAssetCommandValidator> logger)
    {
        RuleFor(command => command.FixedAsset).SetValidator(new FixedAssetCreateRequestValidator());
        RuleForEach(command => command.FixedAssetDetailAllocations).SetValidator(new FixedAssetDetailAllocationCreateRequestValidator());

        if (logger.IsEnabled(LogLevel.Trace))
        {
            logger.LogTrace("INSTANCE CREATED - {ClassName}", GetType().Name);
        }
    }
}

public class FixedAssetCreateRequestValidator : AbstractValidator<FixedAssetCreateRequest>
{
    public FixedAssetCreateRequestValidator()
    {
        //RuleFor(x => x.FixedAssetCategoryId).NotEmpty().WithMessage("Mã định danh loại tài sản không được để trống.");
        //RuleFor(x => x.OrganizationUnitId).NotEmpty().WithMessage("Mã định danh đơn vị sử dụng không được để trống.");
        RuleFor(x => x.RefDate).NotEmpty().WithMessage("Ngày ghi tăng không được để trống.");
        RuleFor(x => x.DepreciationDate).NotEmpty().WithMessage("Ngày bắt đầu tính KH không được để trống.");
        RuleFor(x => x.RefNo).NotEmpty().WithMessage("Số CT ghi tăng không được để trống.");
        RuleFor(x => x.FixedAssetCode).NotEmpty().WithMessage("Mã tài sản không được để trống.");
        RuleFor(x => x.FixedAssetName).NotEmpty().WithMessage("Tên tài sản không được để trống.");
        RuleFor(x => x.OrgPriceAccount).NotEmpty().WithMessage("TK nguyên giá không được để trống.");
        RuleFor(x => x.DepreciationAccount).NotEmpty().WithMessage("TK khấu hao không được để trống.");
        RuleFor(x => x.OrganizationUnitName).NotEmpty().WithMessage("Mã đơn vị sử dụng không được để trống.");
        RuleFor(x => x.OrganizationUnitCode).NotEmpty().WithMessage("Tên đơn vị sử dụng không được để trống.");
        RuleFor(x => x.FixedAssetCategoryName).NotEmpty().WithMessage("Tên loại tài sản không được để trống.");
        RuleFor(x => x.FixedAssetCategoryCode).NotEmpty().WithMessage("Mã loại tài sản không được để trống.");
    }
}

public class FixedAssetDetailAllocationCreateRequestValidator : AbstractValidator<FixedAssetDetailAllocationCreateRequest>
{
    public FixedAssetDetailAllocationCreateRequestValidator()
    {
        //RuleFor(x => x.ObjectId).NotNull().WithMessage("Mã định danh đối tượng không được để trống.");
        RuleFor(x => x.CostAccount).NotEmpty().WithMessage("Tài khoản chi phí không được để trống.");
        RuleFor(x => x.ObjectCode).NotEmpty().WithMessage("Mã đối tượng không được để trống.");
        RuleFor(x => x.ObjectName).NotEmpty().WithMessage("Tên đối tượng không được để trống.");
    }
}

public class CreateFixedAssetIdentifiedCommandValidator : AbstractValidator<IdentifiedCommand<CreateFixedAssetCommand, FAIncrementCreateResponse>>
{
    public CreateFixedAssetIdentifiedCommandValidator(ILogger<CreateFixedAssetIdentifiedCommandValidator> logger)
    {
        RuleFor(command => command.Id).NotEmpty();

        if (logger.IsEnabled(LogLevel.Trace))
        {
            logger.LogTrace("INSTANCE CREATED - {ClassName}", GetType().Name);
        }
    }
}