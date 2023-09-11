using Dapper;
using LoginAPI.Models;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace LoginAPI.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IConfiguration configuration;
        private readonly string connectionString;

        public AuthRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            connectionString = configuration.GetConnectionString("DefaultDbConnection");
        }

        public string GetSaltByEmail(string email)
        {
            using (IDbConnection dbConnection = new SqlConnection(connectionString))
            {
                dbConnection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("Email", email);

                string salt = dbConnection.QuerySingleOrDefault(
                        "SP_GetSaltByEmail",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                return salt;
            }
        }

        public async Task<bool> Login(LoginModel request)
        {
            using (IDbConnection dbConnection = new SqlConnection(connectionString))
            {
                dbConnection.Open();
                var parameters = new DynamicParameters();
                parameters.Add("Email", request.Email);
                parameters.Add("Password", request.Password);
                // Execute the stored procedure and retrieve the validation result
                bool isValid = await dbConnection.ExecuteScalarAsync<bool>(
                    "Sp_ValidateLogin",
                    parameters,
                    commandType: CommandType.StoredProcedure
                    );

                return isValid;

            }

        }

        public async Task<bool> Register(RegisterModel request)
        {
            using (IDbConnection dbConnection = new SqlConnection(connectionString))
            {
                var parameters = new DynamicParameters();

                foreach (PropertyInfo property in request.GetType().GetProperties())
                {
                    string propertyName = property.Name;
                    object propertyValue = property.GetValue(request);

                    parameters.Add(propertyName, propertyValue);
                }

                //parameters.Add("Salt", salt);

                int rowsAffected = await dbConnection.ExecuteAsync
                    (
                        "SP_Register",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );
                return rowsAffected > 0;

            }
        }

        //public async Task<bool> Register(RegisterModel request, string salt)
        //{
        //    using (IDbConnection dbConnection = new SqlConnection(connectionString))
        //    {
        //        var parameters = new DynamicParameters();

        //        foreach (PropertyInfo property in request.GetType().GetProperties())
        //        {
        //            string propertyName = property.Name;
        //            object propertyValue = property.GetValue(request);

        //            parameters.Add(propertyName, propertyValue);
        //        }

        //        //parameters.Add("Salt", salt);

        //        int rowsAffected = await dbConnection.ExecuteAsync
        //            (
        //                "SP_Register",
        //                parameters,
        //                commandType: CommandType.StoredProcedure
        //            );
        //        return rowsAffected > 0;

        //    }
        //}
    }
}
