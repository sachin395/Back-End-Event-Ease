using BookingAPI.Model;
using BookingAPI.Repository;

namespace BookingAPI.Service
{
    public class BookingService : IBookingService
    {

        private readonly IBookingRepository _bookingRepository;

        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<bool> BookEvent(BookingModel request)
        {
            if (request == null)
            {
                return false;
            }
            else
            {
                bool isBooked = await _bookingRepository.BookEvent(request);
                return isBooked;
            }
        }

        public async Task<bool> CancelEventByBookingId(int bookingid)
        {
            if (bookingid == null)
            {
                return false;
            }
            else
            {
                bool isCancelled = await _bookingRepository.CancelEventByBookingId(bookingid);
                return isCancelled;
            }

        }

        public async Task<bool> CancelEventByEventId(int userId, int eventId)
        {
            if (userId == null || eventId == null)
            {
                return false;
            }
            else
            {
                bool isCancelled = await _bookingRepository.CancelEventByEventId(userId, eventId);
                return isCancelled;
            }
        }

        public async Task<List<BookingModel>> GetAllBookedEventByUserId(int userId)
        {
            if (userId == null)
            {
                return null;
            }
            else
            {
                List<BookingModel> booking = await _bookingRepository.GetAllBookedEventByUserId(userId);
                return booking;
            }
        }

        public async Task<BookingModel> GetBookedEventByUserIdAndEventId(int userId, int eventId)
        {
            if (userId == null || eventId == null)
            {
                return null;
            }
            else
            {
                BookingModel booking = await _bookingRepository.GetBookedEventByUserIdAndEventId(userId, eventId);
                return booking;
            }
        }
    }
}
