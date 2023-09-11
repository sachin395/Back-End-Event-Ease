using BookingAPI.Model;

namespace BookingAPI.Service
{
    public interface IBookingService
    {
        Task<bool> BookEvent(BookingModel request);
        Task<bool> CancelEventByBookingId(int bookingid);
        Task<bool> CancelEventByEventId(int userId, int eventId);
        Task<List<BookingModel>> GetAllBookedEventByUserId(int userId);
        Task<BookingModel> GetBookedEventByUserIdAndEventId(int userId, int eventId);
    }
}
