namespace aspdotnet_project.Configurations;

public class VnPayConfig
{
    public required string VnpayKey { get; set; }
    public required string VnpayUrl { get; set; }
    public required string TmnCode { get; set; }
    public required string VnpayReturnUrl { get; set; }
    public required int TimeOut { get; set; }
}