using Microsoft.EntityFrameworkCore;
using QLKHO3.Helper;
using QLKHO3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLKHO3.Reponsitories
{
    public class InventoryRepository : BaseRepository, IInventoryRepository
    {
        public InventoryRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Inventory>> ListAsync()
        {
            return await _context.Inventories.ToListAsync();
        }
        //phan trang 
        public async Task<PagedList<Inventory>> GetAllPagingAsync(PagingParams pagingParams)
        {
            IQueryable<Inventory> _query = from s in _context.Inventories
                                           orderby s.Id
                                           select new Inventory { Id = s.Id, Name = s.Name };
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
            return await PagedList<Inventory>
                .CreateAsync(_query, pagingParams.PageNumber, pagingParams.PageSize);
        }
        public async Task<Inventory> SaveAsync(Inventory _obj)
        {
            await _context.Inventories.AddAsync(_obj);
            await _context.SaveChangesAsync();
            return _obj;
        }
        public async Task<Inventory> DeleteAsync(int id)
        {
            var _obj = await _context.Inventories.Where(o => o.Id == id).FirstOrDefaultAsync();
            if (_obj != null)
            {
                _context.Inventories.Remove(_obj);
                await _context.SaveChangesAsync();
            }
            return _obj;
        }
        public async Task<Inventory> DeleteWithName(string name)
        {
            var _obj = await _context.Inventories.Where(o => o.Name == name).FirstOrDefaultAsync();
            if (_obj != null)
            {
                _context.Inventories.Remove(_obj);
                await _context.SaveChangesAsync();
            }
            return _obj;
        }
        public async Task<Inventory> UpdateAsync(int id, Inventory resource)
        {
            var _obj = await _context.Inventories.Where(o => o.Id == id).FirstOrDefaultAsync();
            if (_obj != null)
            {
                _obj.Name = resource.Name;
                await _context.SaveChangesAsync();
            }
            return _obj;
        }
    }
}
