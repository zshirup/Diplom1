using System.Linq;
using System;
using System.Security.Cryptography;
using System.Text;

namespace PersonnelAccessSystem.Services
{
    internal class AuthService
    {
        private DatabaseService _dbService = new DatabaseService();

        // Хэширование пароля
        internal string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return BitConverter.ToString(hash).Replace("-", ""); // HEX формат
            }
        }

        // Аутентификация пользователя
        public (bool IsAuthenticated, string Role) Authenticate(string username, string password)
        {
            var hashedPassword = HashPassword(password);
            var user = _dbService.GetUsers().FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase) && u.PasswordHash == hashedPassword);

            if (user != null)
            {
                _dbService.LogAction($"User {username} logged in.");
                return (true, user.Role);
            }
            return (false, null);
        }

    }
}