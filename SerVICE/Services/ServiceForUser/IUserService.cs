namespace SerVICE.Services.ServiceForUser
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers();//get
        Task<User?> GetUserById(int id);
        Task<User> GetUserByUsername(string name);
        Task<List<User>> AddUser(User user);//post
        Task<List<User>> UpdateUser(int id, User user);//put
        Task<List<User>> DeleteUser(int id);//dlt
    }
}
