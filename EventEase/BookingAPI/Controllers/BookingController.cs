using BookingAPI.Model;
using BookingAPI.Service;
using Microsoft.AspNetCore.Mvc;

namespace BookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingservice;

        public BookingController(IBookingService bookingservice)
        {
            _bookingservice = bookingservice;
        }

        [HttpPost("bookEventTicket")]
        public async Task<IActionResult> BookEvent(BookingModel request)
        {
            if (ModelState.IsValid)
            {
                bool isEventBooked = await _bookingservice.BookEvent(request);
                return Ok(isEventBooked);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost("getAllBookedEventByUserId/{userId:int}")]
        public async Task<IActionResult> GetAllBookedEventByUserId(int userId)
        {
            if (ModelState.IsValid)
            {
                List<BookingModel> allBookedEvents = await _bookingservice.GetAllBookedEventByUserId(userId);
                return Ok(allBookedEvents);
            }
            return BadRequest(ModelState);
        }

        [HttpPost("getBookedEventByUserIdAndEventId/{userId:int}/{eventId:int}")]
        public async Task<IActionResult> GetBookedEventByUserIdAndEventId(int userId, int eventId)
        {
            if (ModelState.IsValid)
            {
                BookingModel bookedEventModel = await _bookingservice.GetBookedEventByUserIdAndEventId(userId, eventId);
                return Ok(bookedEventModel);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpPost("CancelEventByBookingId")]
        public async Task<IActionResult> CancelEventByBookingId(int bookingid)
        {
            if (ModelState.IsValid && bookingid > 0)
            {
                bool isDeleted = await _bookingservice.CancelEventByBookingId(bookingid);
                return Ok(isDeleted);

            }
            return BadRequest(ModelState);
        }
        [HttpPost("CancelEventByEventId/{userId:int}/{eventId:int}")]
        public async Task<IActionResult> CancelEventByEventId(int userId, int eventId)
        {
            if (ModelState.IsValid)
            {
                bool isDelete = await _bookingservice.CancelEventByEventId(userId, eventId);
                return Ok(isDelete);
            }
            return BadRequest(ModelState);
        }
    }
}
