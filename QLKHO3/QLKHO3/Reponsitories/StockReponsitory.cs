using Microsoft.EntityFrameworkCore;
using QLKHO3.Helper;
using QLKHO3.Models;
using QLKHO3.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLKHO3.Reponsitories
{
    public class StockReponsitory : BaseRepository, IStockRepository
    {

        public StockReponsitory(AppDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Stock>> ListAsync()
        {
            return await _context.Stocks.ToListAsync();
        }
        public async Task<Stock> SaveAsync(Stock _obj)
        {
            await _context.Stocks.AddAsync(_obj);
            await _context.SaveChangesAsync();
            return _obj;
        }
        //phan trang 

        public async Task<PagedList<StockViewModel>> GetAllPagingAsync(PagingParams pagingParams)
        {
            IQueryable<StockViewModel> _query = from sto in _context.Stocks
                                                join uni in _context.Units on sto.UnitId equals uni.Id
                                                join inv in _context.Inventories on sto.InventoryId equals inv.Id
                                                select new StockViewModel
                                                {
                                                    Id = sto.Id,
                                                    Name = sto.Name,
                                                    Soluong = sto.Soluong,
                                                    Noisx = sto.Noisx,
                                                    InventoryId = inv.Id,
                                                    InventoryName = inv.Name,
                                                    UnitId = uni.Id,
                                                    UnitName = uni.Name,

                                                };
            if (pagingParams.SearchKey == "name")
            {
                if (string.IsNullOrEmpty(pagingParams.SearchValue) == false)
                {
                    _query = _query.Where(o => o.Name.Contains(pagingParams.SearchValue));
                }
            }
            // tim kiem all
            if (string.IsNullOrEmpty(pagingParams.Keyword) == false)
            {
                _query = _query.Where(o => o.Name.Contains(pagingParams.SearchKey));
            }

            if (pagingParams.SearchKey == "id")
            {
                if (string.IsNullOrEmpty(pagingParams.SortValue) == false)
                {
                    int _id = Convert.ToInt32(pagingParams.SortValue);
                    _query = _query.Where(o => o.Id == _id);
                }
            }
            //sắp xếp id
            if (pagingParams.SortKey == "id")
            {
                if (pagingParams.SortValue == "ascend")
                    _query = _query.OrderBy(o => o.Id);
                else
                    _query = _query.OrderByDescending(o => o.Id);
            }
            //sắp xếp name
            if (pagingParams.SortKey == "name")
            {
                if (pagingParams.SortValue == "ascend")
                    _query = _query.OrderBy(o => o.Name);
                else
                    _query = _query.OrderByDescending(o => o.Name);
            }
            return await PagedList<StockViewModel>
                .CreateAsync(_query, pagingParams.PageNumber, pagingParams.PageSize);



        }
        public async Task<Stock> DeleteAsync(int id)
        {
            var _obj = await _context.Stocks.Where(o => o.Id == id).FirstOrDefaultAsync();
            if (_obj != null)
            {
                _context.Stocks.Remove(_obj);
                await _context.SaveChangesAsync();
            }
            return _obj;
        }
        public async Task<Stock> DeleteWithName(string name)
        {
            var _obj = await _context.Stocks.Where(o => o.Name == name).FirstOrDefaultAsync();
            if (_obj != null)
            {
                _context.Stocks.Remove(_obj);
                await _context.SaveChangesAsync();
            }
            return _obj;
        }
        public async Task<Stock> UpdateAsync(int id, Stock resource)
        {
            var _obj = await _context.Stocks.Where(o => o.Id == id).FirstOrDefaultAsync();

            if (_obj != null)
            {
                _obj.Name = resource.Name;
                _obj.Soluong = resource.Soluong;
                _obj.Noisx = resource.Noisx;
                _obj.UnitId = resource.UnitId;
                _obj.InventoryId = resource.InventoryId;

                await _context.SaveChangesAsync();
            }
            return _obj;
        }


    }
}
