namespace ControlSystem.Application.Dto;

public class UserDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Password { get; set; }
    public string[] Role { get; set; } = Array.Empty<string>();
    public DateTime DateCreate { get; set; }
    public DateTime DateUpdate { get; set; }
}

public class CreateUserDto
{
    public string Name { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string[] Role { get; set; } = Array.Empty<string>();
    
}