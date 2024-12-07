using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using learn_api_c_sharp.Models;

namespace learn_api_c_sharp.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<List<Stock>> GetUserPortfolio(AppUser? user);
        Task<Portfolio> CreateAsync(Portfolio portfolio);
        Task<Portfolio> DeletePortfolio(AppUser user, string symbol);
    }
}