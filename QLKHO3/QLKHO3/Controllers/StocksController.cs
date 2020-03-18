using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QLKHO3.Helper;
using QLKHO3.Models;
using QLKHO3.Reponsitories;
using QLKHO3.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLKHO3.Controllers
{
    [Route("/api/[controller]")]
    public class StocksController : Controller
    {
        private readonly IStockRepository _stockRepository;

        private readonly IMapper _mapper;

        public StocksController(IStockRepository stockRepository,
            IMapper mapper)
        {
            _stockRepository = stockRepository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IEnumerable<Stock>> GetAllAsync()
        {
            var stocks = await _stockRepository.ListAsync();
            return stocks;
        }

        [HttpGet("getAllPaging")]
        public async Task<IActionResult> GetAllPaging([FromQuery]PagingParams pagingParams)
        {
            PagedList<StockViewModel> paged = await _stockRepository.GetAllPagingAsync(pagingParams);
            Response.AddPagination(paged.CurrentPage, paged.PageSize, paged.TotalCount, paged.TotalPages);
            return Ok(paged);
        }



        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Stock resource)
        {

            var result = await _stockRepository.SaveAsync(resource);


            return Ok(result);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _stockRepository.DeleteAsync(id);


            return Ok(result);
        }

        [HttpDelete("DeleteWithName")]
        public async Task<IActionResult> DeleteWithName([FromBody] Stock resource)
        {
            var result = await _stockRepository.DeleteWithName(resource.Name);


            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] Stock resource)
        {

            var result = await _stockRepository.UpdateAsync(id, resource);


            return Ok(result);
        }
    }
}
