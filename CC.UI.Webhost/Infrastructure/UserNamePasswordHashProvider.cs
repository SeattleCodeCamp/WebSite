namespace CC.UI.Webhost.Infrastructure
{
    using System;
    using System.Security;
    using System.Security.Cryptography;
    using System.Text;

    public sealed class UserNamePasswordHashProvider
    {
        [SecurityCritical]        
        //private const string EmailAddressParameterName = "emailAddress"; 
        private const string PasswordParameterName = "password";
        //private const string HashSalt = "4c0d1b14-a401-442e-a790-e8919356a5e6";
        //private const string HashAlgorithmName = "SHA256";

        //public static string GenerateUserNamePasswordHash(string emailAddress, string password)
        //{
        //    if (string.IsNullOrEmpty(emailAddress))
        //    {
        //        throw new ArgumentNullException(EmailAddressParameterName);
        //    } 
            
        //    if (string.IsNullOrEmpty(password))
        //    {
        //        throw new ArgumentNullException(PasswordParameterName);
        //    } 
            
        //    string hashInput = emailAddress.Trim() + password.Trim() + HashSalt; 
        //    byte[] inputBytes = ASCIIEncoding.ASCII.GetBytes(hashInput); 
        //    byte[] outputBytes = HashAlgorithm.Create(HashAlgorithmName).ComputeHash(inputBytes); 
            
        //    return Convert.ToBase64String(outputBytes);
        //}

        public static string ComputePasswordHash(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(PasswordParameterName);
            }

            var ue = new UnicodeEncoding();
            var bytes = ue.GetBytes(password);
            var sHhash = new SHA1Managed();
            var hash = sHhash.ComputeHash(bytes);

            return ByteArrayToString(hash);
        }

        private static string ByteArrayToString(byte[] arrInput)
        {
            var sb = new StringBuilder(arrInput.Length);

            for (int i = 0; i < arrInput.Length - 1; i++)
                sb.Append(arrInput[i].ToString("X2"));

            return sb.ToString();
        }
    }
}