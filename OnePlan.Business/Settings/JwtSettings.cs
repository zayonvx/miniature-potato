namespace OnePlan.Business.Settings;

public class JwtSettings
{
    public const string Name = "JwtSettings";
    public string Secret { get; set; }
    public string Audience { get; set; }
    public string Issuer { get; set; }
    public int ExpireSeconds { get; set; }
}
