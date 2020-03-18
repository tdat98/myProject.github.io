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
    public class UnitsController : Controller
    {
        private readonly IUnitReponsitory _unitRepository;

        public UnitsController(IUnitReponsitory unitRepository)
        {
            _unitRepository = unitRepository;
        }
        [HttpGet]
        public async Task<IEnumerable<Unit>> GetAllAsync()
        {
            var units = await _unitRepository.ListAsync();
            return units;
        }
        [HttpGet("getAllPaging")]
        public async Task<IActionResult> GetAllPaging([FromQuery]PagingParams pagingParams)
        {
            PagedList<Unit> paged = await _unitRepository.GetAllPagingAsync(pagingParams);
            Response.AddPagination(paged.CurrentPage, paged.PageSize, paged.TotalCount, paged.TotalPages);
            return Ok(paged);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Unit resource)
        {

            var result = await _unitRepository.SaveAsync(resource);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _unitRepository.DeleteAsync(id);


            return Ok(result);
        }
        [HttpDelete("DeleteWithName")]
        public async Task<IActionResult> DeleteWithName([FromBody] Unit resource)
        {
            var result = await _unitRepository.DeleteWithName(resource.Name);


            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] Unit resource)
        {

            var result = await _unitRepository.UpdateAsync(id, resource);


            return Ok(result);
        }
    }
}

