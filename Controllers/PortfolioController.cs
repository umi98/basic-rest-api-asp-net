using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using learn_api_c_sharp.Extensions;
using learn_api_c_sharp.Interfaces;
using learn_api_c_sharp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace learn_api_c_sharp.Controllers
{
    [Route("api/portfolio")]
    [ApiController]
    public class PortfolioController(
            UserManager<AppUser> userManager,
            IStockRepository stockRepository,
            IPortfolioRepository portfolioRepository
        ) : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly IStockRepository _stockRepository = stockRepository;
        private readonly IPortfolioRepository _portfolioRepository = portfolioRepository;

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);
            return Ok(userPortfolio);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPortfolio(string symbol)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var stock = await _stockRepository.GetBySymbolAsync(symbol);

            if (stock == null) return BadRequest("Stock not found");

            var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);

            if (userPortfolio.Any(e => e.Symbol.ToLower() == symbol.ToLower())) return BadRequest("Stock has been bought");

            var portfolioModel = new Portfolio
            {
                StockId = stock.Id,
                AppUserId =  appUser.Id
            };

            await _portfolioRepository.CreateAsync(portfolioModel);

            if (portfolioModel == null) return StatusCode(500, "Could not create portfolio");
            else return Ok("Created");
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeletePortfolio(string symbol)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);

            var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);

            var filteredStock = userPortfolio.Where(s => s.Symbol.ToLower() == symbol.ToLower()).ToList();

            if (filteredStock.Count() == 1)
            {
                await _portfolioRepository.DeletePortfolio(appUser, symbol);
            }
            else
            {
                return BadRequest("Stock is not in your portfolio");
            }

            return Ok("Data deleted");
        }
    }
}