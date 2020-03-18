using Microsoft.EntityFrameworkCore;
using QLKHO3.Helper;
using QLKHO3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLKHO3.Reponsitories
{
    public class UnitReponsitory : BaseRepository, IUnitReponsitory
    {
        public UnitReponsitory(AppDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Unit>> ListAsync()
        {
            return await _context.Units.ToListAsync();
        }
        //phan trang 
        public async Task<PagedList<Unit>> GetAllPagingAsync(PagingParams pagingParams)
        {
            IQueryable<Unit> _query = from u in _context.Units
                                      orderby u.Id
                                      select new Unit { Id = u.Id, Name = u.Name, Des = u.Des };
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
                _query = _query.Where(o => o.Name.Contains(pagingParams.SearchKey) ||
                o.Des.Contains(pagingParams.Keyword));
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
            return await PagedList<Unit>
                .CreateAsync(_query, pagingParams.PageNumber, pagingParams.PageSize);
        }

        //post
        public async Task<Unit> SaveAsync(Unit _obj)
        {
            await _context.Units.AddAsync(_obj);
            await _context.SaveChangesAsync();
            return _obj;
        }
        //xóa bằng id
        public async Task<Unit> DeleteAsync(int id)
        {
            var _obj = await _context.Units.Where(o => o.Id == id).FirstOrDefaultAsync();
            if (_obj != null)
            {
                _context.Units.Remove(_obj);
                await _context.SaveChangesAsync();
            }
            return _obj;
        }
        //xóa bằng name
        public async Task<Unit> DeleteWithName(string name)
        {
            var _obj = await _context.Units.Where(o => o.Name == name).FirstOrDefaultAsync();
            if (_obj != null)
            {
                _context.Units.Remove(_obj);
                await _context.SaveChangesAsync();
            }
            return _obj;
        }
        //update
        public async Task<Unit> UpdateAsync(int id, Unit resource)
        {
            var _obj = await _context.Units.Where(o => o.Id == id).FirstOrDefaultAsync();
            if (_obj != null)
            {
                _obj.Name = resource.Name;
                _obj.Des = resource.Des;
                await _context.SaveChangesAsync();
            }
            return _obj;
        }
    }

}
