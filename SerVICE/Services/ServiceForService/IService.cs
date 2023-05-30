namespace SerVICE.Services.ServiceForService
{
    public interface IService
    {
        Task<List<Service>> GetAllServices();
        Task<Service?> GetSingleService(int id);
        Task<List<Service>> AddService(Service service, int categoryId, string categoryName);
        Task<List<Service>?> UpdateService(int id, Service request_service);
        Task<List<Service>?> DeleteService(int id);
        Task<List<Service>> UpdateCategoriesForService(int id, List<Category> categories);
    }
}
