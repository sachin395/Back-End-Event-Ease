using LoginAPI.Models;
using LoginAPI.Repository;
using OtpNet;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace LoginAPI.Service
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }
        public async Task<bool> IsCredentialValid(LoginModel request)
        {
            //string salt = GenerateSalt();
            //string hashedPassword = HashPassword(request.Password, salt);
            //request.Password = hashedPassword;
            bool isCredentialValid = await _authRepository.Login(request);
            return isCredentialValid;
        }



        //public async Task<bool> Register(RegisterModel request)
        //{
        //    string salt = GenerateSalt();
        //    string hashedPassword = HashPassword(request.PasswordHash, salt);
        //    request.PasswordHash = hashedPassword;


        //    bool isRegistered = await _authRepository.Register(request, salt);

        //    //EmailFunctionality(request.Email);

        //    return isRegistered;
        //}

        private string GenerateSalt()
        {
            byte[] saltBytes = new byte[32];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        private string HashPassword(string password, string salt)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] saltBytes = Encoding.UTF8.GetBytes(salt);

            using (var sha356 = SHA256.Create())
            {
                byte[] combinedBytes = new byte[password.Length + saltBytes.Length];
                Buffer.BlockCopy(passwordBytes, 0, combinedBytes, 0, passwordBytes.Length);
                Buffer.BlockCopy(saltBytes, 0, combinedBytes, passwordBytes.Length, saltBytes.Length);

                byte[] hashedBytes = sha356.ComputeHash(combinedBytes);

                return Convert.ToBase64String(hashedBytes);

            }

        }

        private string GetSaltByEmail(string email)
        {
            return _authRepository.GetSaltByEmail(email);
        }
        private void EmailFunctionality(string Email)
        {

            #region Pass
            string appSpecificPassword = "aljpsdgjkqckjqwe";
            #endregion
            var encodedOtp = Encoding.UTF8.GetBytes("sdfsdafasdfsdafasdfsdafasdf");
            var totp = new Totp(encodedOtp);
            string otp = totp.ComputeTotp();

            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();

            message.From = new MailAddress("sachinrathore1401@gmail.com");
            message.To.Add(new MailAddress(Email));
            message.Subject = "Your OTP Code";
            message.IsBodyHtml = true;
            message.Body = $"Your OTP code is: {otp}";

            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("sachinrathore1401@gmail.com", appSpecificPassword);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            smtp.Send(message);



        }

        public async Task<bool> Register(RegisterModel request)
        {
            bool isRegistered = await _authRepository.Register(request);
            if(isRegistered)
            {
                return true;
            }
            return false;
        }
    }
}
