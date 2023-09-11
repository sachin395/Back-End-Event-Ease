using UserAPI.Model;

namespace UserAPI.Repository
{
    public interface IRepository
    {
        Task<List<EventModel>> GetAllEvents();
        Task<EventModel> GetEventByEventId(int eventId);
        Task<UserModel> GetUserByEmail(string email);
        Task<UserModel> GetUserById(int userId);
        Task<List<EventModel>> SearchEvent(string request);
        Task<bool> UpdateProfile(int id, UserModel user);
    }
}
