using QLKHO3.Helper;
using QLKHO3.Models;
using QLKHO3.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLKHO3.Reponsitories
{
    public interface IStockRepository
    {
        Task<IEnumerable<Stock>> ListAsync();
        Task<PagedList<StockViewModel>> GetAllPagingAsync(PagingParams pagingParams);

        Task<Stock> SaveAsync(Stock _obj);

        Task<Stock> DeleteAsync(int id);

        Task<Stock> DeleteWithName(string name);

        Task<Stock> UpdateAsync(int id, Stock resource);

    }
}
