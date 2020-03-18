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
    public class InventoriesController : Controller
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoriesController(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }
        [HttpGet]
        public async Task<IEnumerable<Inventory>> GetAllAsync()
        {
            var inventories = await _inventoryRepository.ListAsync();
            return inventories;
        }
        [HttpGet("getAllPaging")]
        public async Task<IActionResult> GetAllPaging([FromQuery]PagingParams pagingParams)
        {
            PagedList<Inventory> paged = await _inventoryRepository.GetAllPagingAsync(pagingParams);
            Response.AddPagination(paged.CurrentPage, paged.PageSize, paged.TotalCount, paged.TotalPages);
            return Ok(paged);
        }

        [HttpPost]

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Inventory resource)
        {

            var result = await _inventoryRepository.SaveAsync(resource);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _inventoryRepository.DeleteAsync(id);


            return Ok(result);
        }
        [HttpDelete("DeleteWithName")]
        public async Task<IActionResult> DeleteWithName([FromBody] Inventory resource)
        {
            var result = await _inventoryRepository.DeleteWithName(resource.Name);


            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] Inventory resource)
        {

            var result = await _inventoryRepository.UpdateAsync(id, resource);


            return Ok(result);
        }
    }
}
