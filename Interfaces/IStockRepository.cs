using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using learn_api_c_sharp.DTOs.Stock;
using learn_api_c_sharp.Helper;
using learn_api_c_sharp.Models;

namespace learn_api_c_sharp.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync(QueryObject query);
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock?> GetBySymbolAsync(string symbol);
        Task<Stock> CreateAsync(Stock stock);
        Task<Stock?> UpdateAsync(int id, UpdateStockRequest request);
        Task<Stock?> DeleteByIdAsync(int id);
        Task<bool> IsExist(int id);
    }
}