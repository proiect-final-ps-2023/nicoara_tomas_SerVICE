namespace SerVICE.Services.ServiceForCategory
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategories();//get
        Task<Category?> GetCategoryById(int id);
        Task<Category> GetCategoryByName(string name);
        Task<List<Category>> AddCategory(Category category);//post
        Task<List<Category>> UpdateCategory(int id , Category category);//put
        Task<List<Category>>DeleteCategory(int id);//dlt
    }
}
