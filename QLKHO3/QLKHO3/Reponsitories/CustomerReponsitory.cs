using Microsoft.EntityFrameworkCore;
using QLKHO3.Helper;
using QLKHO3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLKHO3.Reponsitories
{
    public class CustomerReponsitory : BaseRepository, ICustomerReponsitory
    {
        public CustomerReponsitory(AppDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Customer>> ListAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<PagedList<Customer>> GetAllPagingAsync(PagingParams pagingParams)
        {
            IQueryable<Customer> _query = from c in _context.Customers
                                          orderby c.Id
                                          select new Customer { Id = c.Id, Name = c.Name, Diachi = c.Diachi, Sdt = c.Sdt };
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
            return await PagedList<Customer>
                .CreateAsync(_query, pagingParams.PageNumber, pagingParams.PageSize);
        }
            public async Task<Customer> SaveAsync(Customer _obj)
        {
            await _context.Customers.AddAsync(_obj);
            await _context.SaveChangesAsync();
            return _obj;
        }
        public async Task<Customer> DeleteAsync(int id)
        {
            var _obj = await _context.Customers.Where(o => o.Id == id).FirstOrDefaultAsync();
            if (_obj != null)
            {
                _context.Customers.Remove(_obj);
                await _context.SaveChangesAsync();
            }
            return _obj;
        }
        public async Task<Customer> DeleteWithName(string name)
        {
            var _obj = await _context.Customers.Where(o => o.Name == name).FirstOrDefaultAsync();
            if (_obj != null)
            {
                _context.Customers.Remove(_obj);
                await _context.SaveChangesAsync();
            }
            return _obj;
        }
        public async Task<Customer> UpdateAsync(int id, Customer resource)
        {
            var _obj = await _context.Customers.Where(o => o.Id == id).FirstOrDefaultAsync();
            if (_obj != null)
            {
                _obj.Name = resource.Name;
                await _context.SaveChangesAsync();
            }
            return _obj;
        }
    }
}
