namespace SerVICE.Services.ServiceForService
{
    public class ServiceV2Service : IService
    {

        private readonly DataContext _context;

        public ServiceV2Service(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Service>> GetAllServices()
        {
            return await _context.Services.ToListAsync();
        }

        public async Task<Service?> GetSingleService(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service is null) { return null; }
            return service;
        }

        //public async Task<List<Service>> AddService(Service service)
        //{
        //    _context.Services.Add(service);
        //    await _context.SaveChangesAsync();
        //    return await _context.Services.ToListAsync();
        //}
        public async Task<List<Service>> AddService(Service service, int categoryId, string categoryName)
        {
            Category category = null;
            if (categoryId > 0)
            {
                category = await _context.Categories.FindAsync(categoryId);
            }
            else if (!string.IsNullOrEmpty(categoryName))
            {
                category = await _context.Categories.FirstOrDefaultAsync(c => c.Name == categoryName);
            }

            if (category == null)
            {
                throw new ArgumentException("Category not found");
            }

            if (service.Category == null)
            {
                service.Category = new List<Category>();
            }

            var user = await _context.Users.FindAsync(service.UserId);
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }

            service.Category.Add(category);

            _context.Services.Add(service);
            await _context.SaveChangesAsync();

            return await _context.Services.ToListAsync();
        }

        public async Task<List<Service>?> UpdateService(int id, Service request_service)
        {
            var service = await _context.Services.FindAsync(id);
            if (service is null) { return null; }

            if(request_service.Title != "string")
                service.Title = request_service.Title;

            if(request_service.Description != "string")
                service.Description = request_service.Description;

            if (request_service.Price != 0) 
                service.Price = request_service.Price;
            
            await _context.SaveChangesAsync();

            return await _context.Services.ToListAsync();
            //return await _context.Services.ToListAsync();
        }

        public async Task<List<Service>?> DeleteService(int id)
        {
            var service = await _context.Services.FindAsync(id);

            if (service is null)
                return null;

            _context.Services.Remove(service);

            await _context.SaveChangesAsync();
            return await _context.Services.ToListAsync();
        }

        //public async Task<List<Service>> UpdateCategoriesForService(int id, List<Category> categories)
        //{
        //    //var service = await _context.Services.FindAsync(id);
        //    //if(service != null)
        //    //{
        //    //    service.Category = categories;
        //    //}    
        //    //await _context.SaveChangesAsync();
        //    //return await _context.Services.ToListAsync();
        //    return await _context.Services.ToListAsync();
        //}
        
        public Task<List<Service>> UpdateCategoriesForService(int id, List<Category> categories)
        {
            throw new NotImplementedException();
        }
    }
}
