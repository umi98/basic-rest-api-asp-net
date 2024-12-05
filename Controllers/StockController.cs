using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using learn_api_c_sharp.Data;
using learn_api_c_sharp.DTOs.Stock;
using learn_api_c_sharp.Interfaces;
using learn_api_c_sharp.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;

namespace learn_api_c_sharp.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IStockRepository _stockRepo;

        public StockController(ApplicationDBContext context, IStockRepository repository)
        {
            _stockRepo = repository;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stocks = await _stockRepo.GetAllAsync();
            var stockDto = stocks.Select(s => s.ToStockDto());
            return Ok(stockDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var stock = await _stockRepo.GetByIdAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequest stockDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            var stockModel = stockDto.ToStockFromCreateDTO();
            await _stockRepo.CreateAsync(stockModel);
            return CreatedAtAction(nameof(GetById), new {id = stockModel.Id}, stockModel.ToStockDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromBody] UpdateStockRequest request, [FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var stock = await _stockRepo.UpdateAsync(id, request);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var stock = await _stockRepo.DeleteByIdAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}