using LoginAPI.EncryptionDecryption;
using LoginAPI.Models;
using LoginAPI.Service;
using LoginAPI.Token;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Drawing.Imaging;

namespace LoginAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly EncryptDecrypt _encryptDecrypt;
        private readonly ITokenGenration _tokenGenration;



        public AuthController(IAuthService authService, EncryptDecrypt encryptDecrypt, ITokenGenration tokenGenration)
        {
            _authService = authService;
            _encryptDecrypt = encryptDecrypt;
            _tokenGenration = tokenGenration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel request)
        {

            if (ModelState.IsValid)
            {
                bool isRegistered = await _authService.Register(request);
                return Ok(isRegistered);
            }
            return BadRequest(ModelState);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel request)
        {
            if (ModelState.IsValid)
            {
                //string encryptPassword = _encryptDecrypt.EncryptUsingAES256();
                //string decryptPassword=_encryptDecrypt.DecryptString(request.Password);
                //request.Password = decryptPassword; 
                bool isCredentialValid = await _authService.IsCredentialValid(request);
                if(isCredentialValid)
                {
                    string token = _tokenGenration.GenrateToken(request.Email);
                    return Ok(token);
                }
      
                   
               
            }
            return BadRequest("inavild Credentials");

        }



        [HttpGet]
        [Route("generate")]

        public IActionResult GenerateCaptcha()
        {
            string captchaCode = GenerateRandomText();
            string imageData = GenerateCaptchaImage(captchaCode);

            return Ok(new { imageData, captchaCode });
        }

        private string GenerateRandomText()
        {
            // Generate a random text for the CAPTCHA challenge.
            // You can customize this method to generate a text that suits your needs.
            const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789abcdefghjklmnpqrstuvwxyz";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private string GenerateCaptchaImage(string text)
        {
            int CaptchaWidth = 200;
            int CaptchaHeight = 50;
            int CaptchaFontSize = 30;
            var image = new Bitmap(CaptchaWidth, CaptchaHeight);
            var graphics = Graphics.FromImage(image);
            graphics.Clear(Color.White);

            // Add background noise
            using (var noiseBrush = new SolidBrush(Color.LightGray))
            {
                var random = new Random();
                for (int i = 0; i < 100; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);
                    graphics.FillRectangle(noiseBrush, x, y, 2, 2);
                }
            }

            // Apply distortion and add random colors
            using (var font = new Font(FontFamily.GenericMonospace, CaptchaFontSize))
            {
                var random = new Random();
                for (int i = 0; i < text.Length; i++)
                {
                    char c = text[i];
                    float x = i * (CaptchaFontSize * 0.7f) + random.Next(-5, 5);
                    float y = random.Next(-5, 5);
                    float angle = random.Next(-10, 10);

                    Color randomColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));

                    using (var brush = new SolidBrush(randomColor))
                    {
                        graphics.TranslateTransform(x, y);
                        graphics.RotateTransform(angle);
                        graphics.DrawString(c.ToString(), font, brush, 0, 0);
                        graphics.RotateTransform(-angle);
                        graphics.TranslateTransform(-x, -y);
                    }
                }
            }

            // Add random lines
            using (var linePen = new Pen(Color.Black, 1))
            {
                var random = new Random();
                for (int i = 0; i < 10; i++)
                {
                    int x1 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int x2 = random.Next(image.Width);
                    int y2 = random.Next(image.Height);
                    graphics.DrawLine(linePen, x1, y1, x2, y2);
                }
            }

            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Png);
                byte[] imageBytes = ms.ToArray();
                string imageData = "data:image/png;base64," + Convert.ToBase64String(imageBytes);

                return imageData;
            }
        }
        [HttpPost]
        [Route("validate")]
        public IActionResult ValidateCaptcha(CaptchaModel request)
        {
            if (string.IsNullOrEmpty(request.UserInput) || string.IsNullOrEmpty(request.GeneratedText))
            {
                return BadRequest("Both user input and generated text are required.");
            }

            bool isValid = string.Equals(request.UserInput, request.GeneratedText, StringComparison.OrdinalIgnoreCase);

            if (isValid)
            {
                // CAPTCHA validation succeeded
                // You can perform further actions here, such as registration, login, etc.
                return Ok("CAPTCHA validation succeeded.");
            }
            else
            {
                // CAPTCHA validation failed
                return BadRequest("CAPTCHA validation failed.");
            }
        }
    }


}

