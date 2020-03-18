using Microsoft.AspNetCore.Mvc;
using QLKHO3.Helper;
using QLKHO3.Models;
using QLKHO3.Reponsitories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLKHO3.Controllers
{
    [Route("/api/[controller]")]
    public class CustomersController : Controller
    {
        private readonly ICustomerReponsitory _customerRepository;


        public CustomersController(ICustomerReponsitory customerRepository)
        {
            _customerRepository = customerRepository;
        }
        [HttpGet]
        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            var inventories = await _customerRepository.ListAsync();
            return inventories;
        }
        [HttpGet("getAllPaging")]
        public async Task<IActionResult> GetAllPaging([FromQuery]PagingParams pagingParams)
        {
            PagedList<Customer> paged = await _customerRepository.GetAllPagingAsync(pagingParams);
            Response.AddPagination(paged.CurrentPage, paged.PageSize, paged.TotalCount, paged.TotalPages);
            return Ok(paged);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Customer resource)
        {

            var result = await _customerRepository.SaveAsync(resource);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _customerRepository.DeleteAsync(id);


            return Ok(result);
        }
        [HttpDelete("DeleteWithName")]
        public async Task<IActionResult> DeleteWithName([FromBody] Customer resource)
        {
            var result = await _customerRepository.DeleteWithName(resource.Name);


            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] Customer resource)
        {

            var result = await _customerRepository.UpdateAsync(id, resource);


            return Ok(result);
        }
    }
}

