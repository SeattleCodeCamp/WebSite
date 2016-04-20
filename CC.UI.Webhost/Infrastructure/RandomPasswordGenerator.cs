using System;

namespace CC.UI.Webhost.Infrastructure
{
    public static class RandomPasswordGenerator
    {
        public static string Generate()
        {
            return CreateRandomPassword();
        }

        private static string CreateRandomPassword()
        {
            const int passwordLength = 18;
            const string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!@$?_-";
            
            var chars = new char[passwordLength];
            var rd = new Random();
            for (int i = 0; i < passwordLength; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }
            return new string(chars);
        }
    }
}