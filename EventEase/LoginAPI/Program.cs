using LoginAPI.EncryptionDecryption;
using LoginAPI.Repository;
using LoginAPI.Service;
using LoginAPI.Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LoginAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<IAuthRepository, AuthRepository>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<EncryptDecrypt>();
            builder.Services.AddScoped<ITokenGenration, TokenGenration>();
            builder.Services.AddControllers();
            builder.Services.AddCors();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            ValidateUserTokensWithParameters(builder.Services, builder.Configuration);

            void ValidateUserTokensWithParameters(IServiceCollection services, IConfiguration configuration)
            {
                var userSecretKey = configuration["JWtValidationDetails:UserApplicationKey"];
                var userIssuer = configuration["JWtValidationDetails:Userissuer"];
                var useraudience = configuration["JWtValidationDetails:UserAudience"];
                var userSymmatricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(userSecretKey));
                //Token validation imbuilt class
                var usertokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = userIssuer,

                    ValidateAudience = true,
                    ValidAudience = useraudience,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = userSymmatricKey,

                    ValidateLifetime = true

                };
                services.AddAuthentication(u =>
                {
                    u.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    u.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(u => u.TokenValidationParameters = usertokenValidationParameters);
            }
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors(options =>
            {
                options.AllowAnyHeader();
                options.AllowAnyMethod();
                options.AllowAnyOrigin();

            });
            app.MapControllers();

            app.Run();
        }
    }
}