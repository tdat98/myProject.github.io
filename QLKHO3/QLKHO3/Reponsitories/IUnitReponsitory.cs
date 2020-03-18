using QLKHO3.Helper;
using QLKHO3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLKHO3.Reponsitories
{
    public interface IUnitReponsitory
    {
        Task<IEnumerable<Unit>> ListAsync();
        Task<PagedList<Unit>> GetAllPagingAsync(PagingParams pagingParams);

        Task<Unit> SaveAsync(Unit _obj);

        Task<Unit> DeleteAsync(int id);

        Task<Unit> DeleteWithName(string name);

        Task<Unit> UpdateAsync(int id, Unit resource);
    }
}
