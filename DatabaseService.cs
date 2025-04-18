using PersonnelAccessSystem.Models;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System;
using System.Security.Cryptography;
using System.Text;

namespace PersonnelAccessSystem.Services
{
    public class DatabaseService
    {
        private PersonnelAccessSystemEntities _context = new PersonnelAccessSystemEntities();

        public List<User> GetUsers()
        {
            return _context.Users.Select(u => new User
            {
                UserId = u.UserId,
                Username = u.Username,
                PasswordHash = u.PasswordHash,
                Role = u.Role
            }).ToList();
        }

        public void AddAccessCard(AccessCard card)
        {
            var dbCard = new AccessCards
            {
                FirstName = card.FirstName,
                LastName = card.LastName,
                BirthDate = card.BirthDate,
                AccessLevel = card.AccessLevel,
                PhotoPath = card.PhotoPath
            };
            _context.AccessCards.Add(dbCard);
            _context.SaveChanges();
        }

        public List<AccessCard> GetAccessCards()
        {
            return _context.AccessCards.Select(c => new AccessCard
            {
                CardId = c.CardId,
                FirstName = c.FirstName,
                LastName = c.LastName,
                BirthDate = c.BirthDate,
                AccessLevel = c.AccessLevel,
                PhotoPath = c.PhotoPath
            }).ToList();
        }

        public void LogAction(string action)
        {
            var logEntry = new LogEntries
            {
                Action = action,
                Timestamp = DateTime.Now // или другое значение даты/времени
            };
            _context.LogEntries.Add(logEntry);
            _context.SaveChanges();
        }

        public void AddUser(User user)
        {
            var dbUser = new Users
            {
                Username = user.Username,
                PasswordHash = user.PasswordHash,
                Role = user.Role
            };
            _context.Users.Add(dbUser);
            _context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            var dbUser = _context.Users.FirstOrDefault(u => u.UserId == user.UserId);
            if (dbUser != null)
            {
                dbUser.Username = user.Username;
                dbUser.PasswordHash = user.PasswordHash;
                dbUser.Role = user.Role;
                _context.SaveChanges();
            }
        }

        public void DeleteUser(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return BitConverter.ToString(hash).Replace("-", ""); // HEX формат
            }
        }

        public void UpdateAccessCard(AccessCard card)
        {
            var dbCard = _context.AccessCards.FirstOrDefault(c => c.CardId == card.CardId);
            if (dbCard != null)
            {
                dbCard.FirstName = card.FirstName;
                dbCard.LastName = card.LastName;
                dbCard.BirthDate = card.BirthDate;
                dbCard.AccessLevel = card.AccessLevel;
                dbCard.EndDate = card.EndDate;
                _context.SaveChanges();
            }
        }

        public List<LogEntry> GetLogEntries()
        {
            return _context.LogEntries.Select(l => new LogEntry
            {
                LogId = l.LogId,
                Action = l.Action,
                Timestamp = l.Timestamp
            }).ToList();
        }

        public void DeleteAccessCard(int cardId)
        {
            var card = _context.AccessCards.FirstOrDefault(c => c.CardId == cardId);
            if (card != null)
            {
                _context.AccessCards.Remove(card);
                _context.SaveChanges();
            }
        }

    }
}