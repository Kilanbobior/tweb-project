﻿using System;
using System.Security.Cryptography;
using System.Text;

namespace TWeb48.Helpers
{
    public class PasswordHelper
    {
        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        public static bool VerifyPassword(string password, string hash)
        {
            return HashPassword(password) == hash;
        }

        internal static bool VerifyPassword(string password, object passwordHash)
        {
            throw new NotImplementedException();
        }
    }
}