using QLKHO3.Helper;
using QLKHO3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLKHO3.Reponsitories
{
    public interface ICustomerReponsitory
    {
        Task<IEnumerable<Customer>> ListAsync();

        Task<PagedList<Customer>> GetAllPagingAsync(PagingParams pagingParams);

        Task<Customer> SaveAsync(Customer _obj);

        Task<Customer> DeleteAsync(int id);

        Task<Customer> DeleteWithName(string name);

        Task<Customer> UpdateAsync(int id, Customer resource);
    }
}
