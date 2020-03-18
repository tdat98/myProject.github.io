using QLKHO3.Helper;
using QLKHO3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLKHO3.Reponsitories
{

    public interface IInventoryRepository
    {
        Task<IEnumerable<Inventory>> ListAsync();

        Task<PagedList<Inventory>> GetAllPagingAsync(PagingParams pagingParams);

        Task<Inventory> SaveAsync(Inventory _obj);

        Task<Inventory> DeleteAsync(int id);

        Task<Inventory> DeleteWithName(string name);

        Task<Inventory> UpdateAsync(int id, Inventory resource);

    }

}
