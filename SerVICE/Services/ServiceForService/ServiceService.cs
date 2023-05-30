namespace SerVICE.Services.ServiceForService
{
    public class ServiceService// : IService
    {
       
        private readonly DataContext _context;

        public ServiceService(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Service>> AddService(Service service)
        {
            _context.Services.Add(service);
            await _context.SaveChangesAsync();
            return await _context.Services.ToListAsync();
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

        public async Task<List<Service>> GetAllServices()
        {
            var services = await _context.Services.ToListAsync();
            return services;
        }

        public async Task<Service?> GetSingleService(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service is null) { return null; }
            return service;
        }

        public async Task<List<Service>?> UpdateService(int id, Service request_service)
        {
            var service = await _context.Services.FindAsync(id);

            if (service is null)
                return null;
            /*
            if (request_service.Name != "string")
                service.Name = request_service.Name;

            if (request_service.Description != "string")
                service.Description = request_service.Description;

            if(request_service.UserName != "string")
                service.UserName = request_service.UserName;

            if (request_service.Type != "string")
                service.Type = request_service.Type;
            */
            await _context.SaveChangesAsync();

            return await _context.Services.ToListAsync();
        }
    }
}
