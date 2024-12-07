using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using learn_api_c_sharp.Data;
using learn_api_c_sharp.Interfaces;
using learn_api_c_sharp.Models;
using Microsoft.EntityFrameworkCore;

namespace learn_api_c_sharp.Repository
{
    public class PortfolioRepository(ApplicationDBContext context) : IPortfolioRepository
    {
        private readonly ApplicationDBContext _context = context;

        public async Task<Portfolio> CreateAsync(Portfolio portfolio)
        {
            await _context.Portfolios.AddAsync(portfolio);
            await _context.SaveChangesAsync();
            return portfolio;
        }

        public async Task<Portfolio> DeletePortfolio(AppUser user, string symbol)
        {
            var portfolio = await _context.Portfolios.FirstOrDefaultAsync(x => x.AppUserId == user.Id && x.Stock.Symbol.ToLower() == symbol.ToLower());
            if (portfolio == null) return null;
            _context.Portfolios.Remove(portfolio);
            await _context.SaveChangesAsync();
            return portfolio;
        }

        public async Task<List<Stock>> GetUserPortfolio(AppUser user)
        {
            return await _context.Portfolios.Where(u => u.AppUserId == user.Id)
                .Select(stock => new Stock
                {
                    Id = (int)stock.StockId,
                    Symbol = stock.Stock.Symbol,
                    CompanyName = stock.Stock.CompanyName,
                    Price = stock.Stock.Price,
                    LastDive = stock.Stock.LastDive,
                    Industry = stock.Stock.Industry,
                    MarketCap = stock.Stock.MarketCap
                }).ToListAsync();
        }
    }
}