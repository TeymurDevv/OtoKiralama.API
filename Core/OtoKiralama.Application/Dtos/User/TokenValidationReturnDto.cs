namespace OtoKiralama.Application.Dtos.User;

public class TokenValidationReturnDto
{
    public string id { get; set; }
    public string username { get; set; }
    public string first_name { get; set; }
    public string last_name { get; set; }
    public string email { get; set; }
    public string roles { get; set; }
}