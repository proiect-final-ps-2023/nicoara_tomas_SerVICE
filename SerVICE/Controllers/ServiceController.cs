using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SerVICE.Models;
using SerVICE.Services.ServiceForService;

namespace SerVICE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IService _serviceService;
        public ServiceController(IService serviceService)
        {
            _serviceService = serviceService;
        }


        [HttpGet] //used for swagger
        public async Task<ActionResult<List<Service>>> GetAllServices()
        {            
            return await _serviceService.GetAllServices();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Service>>? GetSingleService(int id)
        {
            var result = await _serviceService.GetSingleService(id);
            if (result == null)
                return NotFound("ERROR 404");

            return Ok(result);
        }

        [HttpPost("{categoryId}/{categoryName}")]
        public async Task<ActionResult<List<Service>>> AddService(Service service, int categoryId, string categoryName)//[FromBody]Service service
        {
            var result = await _serviceService.AddService(service, categoryId, categoryName);
            return Ok(result);
        }

        [HttpPut("update/{serviceId}")]
        public async Task<ActionResult<List<Service>>> UpdateService(int serviceId, Service request_service)
        {
            var result = await  _serviceService.UpdateService(serviceId, request_service);
            if (result == null)
                return NotFound("ERROR 404");

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Service>>> DeleteService(int id)
        {
            var result =await  _serviceService.DeleteService(id);
            if (result == null)
                return NotFound("ERROR 404");

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Service>>> UpdateCategoriesForService(int id, List<Category> categories)
        {
            try
            {
                var result = await _serviceService.UpdateCategoriesForService(id, categories);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }

        }
    }
}
