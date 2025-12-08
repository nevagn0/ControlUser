public class User
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string Email { get; set; }
	public string HashPassword { get; set; }
	public string[] Role { get; set; } = Array.Empty<string>();
	public DateTime DateCreate { get; set; }
	public DateTime DateUpdate { get; set; }
}