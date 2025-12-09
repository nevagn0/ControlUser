using System.ComponentModel.DataAnnotations;

namespace ControlSystem.Application.Dto
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string[] Role { get; set; } = Array.Empty<string>();
        public DateTime DateCreate { get; set; }
        public DateTime DateUpdate { get; set; }
    }
    
    public class CreateUserDto
    {
        [Required(ErrorMessage = "Имя обязательно")]
        public string Name { get; set; } = null!;
        
        [Required(ErrorMessage = "Пароль обязателен")]
        [MinLength(6, ErrorMessage = "Пароль должен содержать минимум 6 символов")]
        public string Password { get; set; } = null!;
        
        [Required(ErrorMessage = "Email обязателен")]
        [EmailAddress(ErrorMessage = "Некорректный формат Email")]
        public string Email { get; set; } = null!;
        
        public string[] Role { get; set; } = null!; 
    }
    
    public class RegisterRequestDto
    {
        [Required(ErrorMessage = "Имя обязательно")]
        public string Name { get; set; } = null!;
        
        [Required(ErrorMessage = "Пароль обязателен")]
        [MinLength(6, ErrorMessage = "Пароль должен содержать минимум 6 символов")]
        public string Password { get; set; } = null!;
        
        [Required(ErrorMessage = "Email обязателен")]
        [EmailAddress(ErrorMessage = "Некорректный формат Email")]
        public string Email { get; set; } = null!;
        
        [Required(ErrorMessage = "Тип пользователя обязателен")]
        public string Role { get; set; } = null!;
    }

    public class LoginRequestDto
    {
        [Required(ErrorMessage = "Email обязателен")]
        public string Email { get; set; } = null!;
        
        [Required(ErrorMessage = "Пароль обязателен")]
        public string Password { get; set; } = null!;
    }

    public class AuthResponseDto
    {
        public string Token { get; set; } = null!;
        public DateTime Expires { get; set; }
        public UserDto User { get; set; } = null!;
    }
}