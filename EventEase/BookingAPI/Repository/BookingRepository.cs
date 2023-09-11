using BookingAPI.Model;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Reflection;

namespace BookingAPI.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly IConfiguration _config;
        public BookingRepository(IConfiguration config)
        {
            _config = config;
        }
        public async Task<bool> BookEvent(BookingModel request)
        {
            using (IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("LocalConnectionString")))
            {
                dbConnection.Open();
                var parameters = new DynamicParameters();

                foreach (PropertyInfo property in request.GetType().GetProperties())
                {
                    string propertyName = property.Name;
                    object propertyValue = property.GetValue(request);

                    parameters.Add(propertyName, propertyValue);
                }
                int rowsAffected = await dbConnection.ExecuteAsync
                (
                    "CreateBooking",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
                return rowsAffected > 0;


            }
        }

        public async Task<bool> CancelEventByBookingId(int bookingid)
        {
            using (IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("LocalConnectionString")))
            {
                dbConnection.Open();
                var parameters = new
                {
                    bookingid = bookingid
                };
                int rowsAffected = await dbConnection.ExecuteAsync("CancelEventByBookingId", parameters, commandType: CommandType.StoredProcedure);
                return rowsAffected > 0;
            }
        }

        public async Task<bool> CancelEventByEventId(int userId, int eventId)
        {
            using (IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("LocalConnectionString")))
            {
                dbConnection.Open();
                var parameters = new
                {
                    UserId = userId,
                    EventId = eventId
                };
                int rowsAffected = await dbConnection.ExecuteAsync("CancelEventByEventId", parameters, commandType: CommandType.StoredProcedure);
                return rowsAffected > 0;
            }
        }

        public async Task<List<BookingModel>> GetAllBookedEventByUserId(int userId)
        {

            using (IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("LocalConnectionString")))
            {
                dbConnection.Open();
                var parameters = new
                {
                    userId = userId,

                };
                var bookingList = await dbConnection.QueryAsync<BookingModel>("GetAllBookedEventByUserId", parameters, commandType: CommandType.StoredProcedure);
                return bookingList.ToList();
            }
        }

        public async Task<BookingModel> GetBookedEventByUserIdAndEventId(int userId, int eventId)
        {
            using (IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("LocalConnectionString")))
            {
                dbConnection.Open();
                var parameters = new
                {
                    userId = userId,
                    eventId = eventId

                };

                var result = await dbConnection.QueryAsync<BookingModel>("GetBookedEventByUserIdAndEventId", parameters, commandType: CommandType.StoredProcedure);
                BookingModel booking = result.FirstOrDefault();
                return booking;
            }
        }

        private void EmailFunctionality(string Email)
        {

            #region Pass
            string appSpecificPassword = "aljpsdgjkqckjqwe";
            #endregion

            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();

            message.From = new MailAddress("sachinrathore1401@gmail.com");
            message.To.Add(new MailAddress(Email));
            message.Subject = "Your OTP Code";
            message.IsBodyHtml = true;
            message.Body = $"Succesfully Booked!!!";

            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("sachinrathore1401@gmail.com", appSpecificPassword);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            smtp.Send(message);



        }
    }
}
