using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SerVICE.Models;
using SerVICE.Services.ServiceForCategory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SerVICE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetAllCategories()
        {
            return await _categoryService.GetAllCategories() as List<Category>;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategoryById(int id)
        {
            try
            {
                var category = await _categoryService.GetCategoryById(id);
                return Ok(category);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<Category>> GetCategoryByName(string name)
        {
            try
            {
                var category = await _categoryService.GetCategoryByName(name);
                return Ok(category);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<Category>>> AddCategory(Category category)
        {
            var result = await _categoryService.AddCategory(category);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Category>>> UpdateCategory(int id, Category category)
        {
            try
            {
                var result = await _categoryService.UpdateCategory(id, category);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Category>>> DeleteCategory(int id)
        {
            try
            {
                var result = await _categoryService.DeleteCategory(id);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}