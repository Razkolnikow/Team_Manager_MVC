using System.Linq;
using Team_Manager.Data.Common.Models;

namespace Team_Manager.Services.Data.Contracts
{
    public interface IBaseDataService<T>
       where T : class, IDeletableEntity, IAuditInfo
    {
        void Add(T item);

        void Delete(object id);

        IQueryable<T> GetAll();

        T GetById(object id);

        void Save();

        void Dispose();
    }
}
