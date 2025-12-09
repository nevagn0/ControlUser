using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ControlSystem.Domain.Repository;
using ControlSystem.Application.Dto;

namespace ControkSystem.Application.Services
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterRequestDto request);
        Task<AuthResponseDto> LoginAsync(LoginRequestDto request);
    }
    
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IAuthService _authServiceImplementation;

        private static class UserRole
        {
            public const string Engineer = "Инженер";
            public const string Manager = "Менеджер";
            public const string Observer = "Наблюдатель";
        }
        
        public AuthService(IUserRepository userRepository, IConfiguration configuration, ILogger<AuthService> logger, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task RegisterAsync(RegisterRequestDto request)
        {
            var existingUser = await _userRepository.GetByEmailAsync(request.Email);
            if (existingUser != null)
                throw new ArgumentException("Пользователь с таким Email уже существует");
            
            var RoleUser = new[] {UserRole.Engineer, UserRole.Manager, UserRole.Observer};
            if (!RoleUser.Contains(request.Role))
                throw new ArgumentException($"Недопустимая роль '{request.Role}'. Допустимые роли: {string.Join(", ", RoleUser)}");
            
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email,
                HashPassword = HashPassword(request.Password),
                Role = new[] { request.Role },
                DateCreate = DateTime.UtcNow,
                DateUpdate = DateTime.UtcNow
            };

            await _userRepository.CreateUser(user);
        }

        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null)
                throw new ArgumentException("Неверный логин или пароль");

            var passwordValid = VerifyPassword(request.Password, user.HashPassword);
            if (!passwordValid)
                throw new ArgumentException("Неверный логин или пароль");
            
            var authresponse = GenerateJwtToken(user);
            
            
            
            return authresponse;
        }

        private AuthResponseDto GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            foreach (var role in user.Role)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? throw new InvalidOperationException()));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddHours(Convert.ToDouble(_configuration["Jwt:ExpireHours"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new AuthResponseDto()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expires = expires,
                User = new UserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    Role = user.Role
                }
            };
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public async Task<bool> VerifyPasswordAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
                return false;

            return VerifyPassword(password, user.HashPassword);
        }
        
        private bool VerifyPassword(string password, string hashPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashPassword);
        }
    }
}