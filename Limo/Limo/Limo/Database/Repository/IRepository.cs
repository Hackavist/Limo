using Limo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Limo.DataBase
{
    public interface IRepository<T> where T : BaseModel
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> InsertAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> SoftDeleteAsync(T entity);
        Task<bool> HardDeleteAsync(int id);
    }
}
