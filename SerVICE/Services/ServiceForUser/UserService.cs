namespace SerVICE.Services.ServiceForUser
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        public UserService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<User>> AddUser(User user)
        {
            if (user == null)
            {
                return null;
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            
            return await _context.Users.ToListAsync();
            
        }

        public async Task<List<User>> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }
            await _context.SaveChangesAsync();
            return await _context.Users.ToListAsync();
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetUserById(int id)
        {
            var user_by_id = await _context.Users.FindAsync(id);
            if(user_by_id != null)
            {
                return user_by_id;
            }
            throw new ArgumentException($"User with id '{id}' not found.");
        }

        public async Task<User> GetUserByUsername(string name)
        {
            var user_by_name = await _context.Users.FirstOrDefaultAsync(c => c.Username.ToUpper().Contains(name.ToUpper()));
            if (user_by_name != null)
            {
                return user_by_name;
            }
            throw new ArgumentException($"User with name '{name}' not found.");
        }

        public async Task<List<User>> UpdateUser(int id, User user)
        {
            var userToUpdate = await _context.Users.FindAsync(id);
            if (userToUpdate == null)
            {
                throw new ArgumentException($"User with id {id} not found");
            }
            if(user.Username != "string")
                userToUpdate.Username = user.Username;
            if (user.Email != "string")
                userToUpdate.Email = user.Email;
            await _context.SaveChangesAsync();
            return await _context.Users.ToListAsync();
        }
    }
}
