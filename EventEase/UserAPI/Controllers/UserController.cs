using Microsoft.AspNetCore.Mvc;
using UserAPI.Model;
using UserAPI.Service;

namespace UserAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IService _service;

        public UserController(IService service)
        {
            _service = service;
        }
        [HttpPost]
        [Route("GetAllEvents")]

        public async Task<ActionResult> GetAllEvents()
        {
            List<EventModel> eventslst = await _service.GetAllEvents();
            if (eventslst != null)
            {
                return Ok(eventslst);
            }
            else
            {
                return StatusCode(204);
            }
        }

        [HttpPost]
        [Route("UpdateProfile")]
        public async Task<ActionResult> UpdateProfile(int id, UserModel user)
        {
            bool res = await _service.UpdateProfile(id, user);
            if (res)
            {
                return Ok(true);
            }
            else
            {
                return BadRequest(false);
            }
        }

        [HttpPost]
        [Route("SearchEvent")]
        public async Task<ActionResult> SearchEvent(string request)
        {
            request.ToLower();
            List<EventModel> eventmodel = await _service.SearchEvent(request);
            if (eventmodel != null)
            {
                return Ok(eventmodel);
            }
            else
            {
                return BadRequest("No Result Found!!");
            }
        }

        [HttpPost]
        [Route("GetEventByEventId")]
        public async Task<ActionResult> GetEventByEventId(int eventId)
        {

            EventModel eventmodel = await _service.GetEventByEventId(eventId);
            if (eventmodel != null)
            {
                return Ok(eventmodel);
            }
            else
            {
                return BadRequest("No Result Found!!");
            }
        }

        [HttpPost]
        [Route("GetUserById")]
        public async Task<ActionResult> GetUserById(int userId)
        {

            UserModel eventmodel = await _service.GetUserById(userId);
            if (eventmodel != null)
            {
                return Ok(eventmodel);
            }
            else
            {
                return BadRequest("No Result Found!!");
            }
        }

        [HttpPost]
        [Route("GetUserByEmail")]
        public async Task<ActionResult> GetUserByEmail(string email)
        {

            UserModel eventmodel = await _service.GetUserByEmail(email);
            if (eventmodel != null)
            {
                return Ok(eventmodel);
            }
            else
            {
                return BadRequest("No Result Found!!");
            }
        }
    }
}
