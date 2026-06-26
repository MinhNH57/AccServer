namespace Voucher.Acc.Features.Commands.ToKhai01;

public class VatFormLayout
{
    public double DX { get; set; } = 0;
    public double DY { get; set; } = 0;

    public double X_LEFT { get; set; } = 135.0;
    public double X_RIGHT { get; set; } = 170.0;
    public double W_VAL { get; set; } = 28.0;

    public double Y_04_TenNNT { get; set; } = 64.0;
    public double Y_05_MST { get; set; } = 71.0;

    public double Y_22 { get; set; } = 92.0;
    public double Y_23 { get; set; } = 104.0;
    public double Y_23a { get; set; } = 112.0;
    public double Y_24a { get; set; } = 112.0;
    public double Y_24 { get; set; } = 104.0;
    public double Y_25 { get; set; } = 120.0;

    public double Y_26 { get; set; } = 140.0;
    public double Y_27_28 { get; set; } = 151.0;
    public double Y_29 { get; set; } = 161.0;
    public double Y_30_31 { get; set; } = 169.0;
    public double Y_32_33 { get; set; } = 177.0;
    public double Y_32a { get; set; } = 185.0;

    public double Y_34_35 { get; set; } = 197.0;
    public double Y_36 { get; set; } = 209.0;

    public double Y_37 { get; set; } = 223.0;
    public double Y_38 { get; set; } = 231.0;

    public double Y_39a { get; set; } = 243.0;

    public double Y_40a { get; set; } = 257.0;
    public double Y_40b { get; set; } = 265.0;
    public double Y_40 { get; set; } = 273.0;
    public double Y_41 { get; set; } = 281.0;
    public double Y_42 { get; set; } = 289.0;
    public double Y_43 { get; set; } = 297.0;

    public static VatFormLayout Default => new();

}
