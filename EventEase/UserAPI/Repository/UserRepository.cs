using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using UserAPI.Model;

namespace UserAPI.Repository
{
    public class UserRepository : IRepository
    {
        private readonly IConfiguration _config;
        public UserRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task<List<EventModel>> GetAllEvents()
        {
            using (IDbConnection conn = new SqlConnection(_config.GetConnectionString("LocalConnectionString")))
            {
                conn.Open();
                string query = "Select * from EventTbls";
                List<EventModel> concertevents = (await conn.QueryAsync<EventModel>(query)).ToList();
                return concertevents;

            }
        }

        public async Task<EventModel> GetEventByEventId(int eventId)
        {
            using (IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("LocalConnectionString")))
            {
                dbConnection.Open();
                var parameters = new
                {
                    EventId = eventId,

                };
                var res = await dbConnection.QueryAsync<EventModel>("GetEventByEventId", parameters, commandType: CommandType.StoredProcedure);
                EventModel eventmodel = res.FirstOrDefault();
                return eventmodel;
            }

        }

        public async Task<List<EventModel>> SearchEvent(string request)
        {
            using (IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("LocalConnectionString")))
            {
                dbConnection.Open();
                var parameters = new
                {
                    SearchText = request,

                };
                var bookingList = await dbConnection.QueryAsync<EventModel>("SearchEvents", parameters, commandType: CommandType.StoredProcedure);
                return bookingList.ToList();
            }
        }
        public async Task<bool> UpdateProfile(int id, UserModel user)
        {
            using (IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("LocalConnectionString")))
            {
                dbConnection.Open();
                var parameters = new DynamicParameters();

                foreach (PropertyInfo property in user.GetType().GetProperties())
                {
                    string propertyName = property.Name;
                    object propertyValue = property.GetValue(user);

                    parameters.Add(propertyName, propertyValue);
                }
                int rowsAffected = await dbConnection.ExecuteAsync
                (
                    "UpdateUserProfile",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
                return rowsAffected > 0;


            }


        }

        public async Task<UserModel> GetUserById(int userId)
        {
            using (IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("LocalConnectionString")))
            {
                dbConnection.Open();
                var parameters = new
                {
                    UserId = userId,

                };
                UserModel user = (UserModel)await dbConnection.QueryAsync<UserModel>("GetUserById", parameters, commandType: CommandType.StoredProcedure);
                return user;
            }
        }

        public async Task<UserModel> GetUserByEmail(string email)
        {
            using (IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("LocalConnectionString")))
            {
                dbConnection.Open();
                var parameters = new
                {
                    Email = email,

                };
                var result = await dbConnection.QueryAsync<UserModel>("GetUserByEmail", parameters, commandType: CommandType.StoredProcedure);
                UserModel user = result.FirstOrDefault();
                return user;
                
            }

        }
    }
}



