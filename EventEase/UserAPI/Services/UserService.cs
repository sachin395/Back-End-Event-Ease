using UserAPI.Model;
using UserAPI.Repository;

namespace UserAPI.Service
{
    public class UserService : IService
    {
        private readonly IRepository _repository;
        public UserService(IRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<EventModel>> GetAllEvents()
        {
            List<EventModel> eventslst = await _repository.GetAllEvents();
            return eventslst;
        }

        public async Task<EventModel> GetEventByEventId(int eventId)
        {
            if (eventId > 0)
            {
                EventModel eventModel = await _repository.GetEventByEventId(eventId);
                return eventModel;
            }
            else
            {
                return null;
            }
        }

        public async Task<UserModel> GetUserByEmail(string email)
        {
            UserModel eventmodel = await _repository.GetUserByEmail(email);
            if(eventmodel != null)
            {
                return eventmodel;
            }
            else
            {
                return null;
            }
        }

        public async Task<UserModel> GetUserById(int userId)
        {
            if (userId > 0)
            {
                UserModel userModel = await _repository.GetUserById(userId);
                return userModel;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<EventModel>> SearchEvent(string request)
        {
            List<EventModel> eventList = await _repository.SearchEvent(request);
            if (eventList == null)
            {
                return null;
            }
            else
            {
                return eventList;
            }


        }

        public async Task<bool> UpdateProfile(int id, UserModel user)
        {
            if (user == null || id == null)
            {
                return false;
            }
            else
            {
                bool res = await _repository.UpdateProfile(id, user);
                return res;
            }
        }
    }
}
