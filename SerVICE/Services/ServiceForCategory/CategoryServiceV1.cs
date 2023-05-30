namespace SerVICE.Services.ServiceForCategory
{
    public class CategoryServiceV1 : ICategoryService
    {
        private readonly DataContext _context;
        public CategoryServiceV1(DataContext context)
        {
            _context = context;
        }

        //public async Task<List<Category>> AddCategory(Category category)
        //{
        //    _context.Categories.Add(category);
        //    await _context.SaveChangesAsync();
        //    return await _context.Categories.ToListAsync();
        //}

        //somethings off..
        public async Task<List<Category>> AddCategory(Category category)
        {
            var services = category.Services;
            category.Services = null; // remove services from the category to avoid saving them twice

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            // associate the services with the category and save them
            foreach (var service in services)
            {
                service.Category = (ICollection<Category>?)category;
                _context.Services.Add(service);
            }
            await _context.SaveChangesAsync();

            // reload the category with its associated services and return it
            return await _context.Categories.ToListAsync();
        }

        public async Task<List<Category>> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
            }
            await _context.SaveChangesAsync();
            return await _context.Categories.ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category?> GetCategoryById(int id)
        {
            var category_by_id = await _context.Categories.FindAsync(id);
            if(category_by_id!= null)
            {
                return  category_by_id;
            }
            throw new ArgumentException($"Category with id '{id}' not found.");
        }

        public async Task<Category> GetCategoryByName(string name)
        {
            var category_by_name = await _context.Categories.FirstOrDefaultAsync(c => c.Name.ToUpper().Contains(name.ToUpper())); ;
            if (category_by_name != null)
            {
                return category_by_name;
            }
            throw new ArgumentException($"Category with name '{name}' not found.");
        }

        public async Task<List<Category>> UpdateCategory(int id, Category category)
        {
            var categoryToUpdate = await _context.Categories.FindAsync(id);
            if (categoryToUpdate == null)
            {
                throw new ArgumentException($"Category with id {id} not found");
            }
            categoryToUpdate.Name = category.Name;
            _context.Categories.Update(categoryToUpdate);
            await _context.SaveChangesAsync();
            return await _context.Categories.ToListAsync();
        }

    }
}
