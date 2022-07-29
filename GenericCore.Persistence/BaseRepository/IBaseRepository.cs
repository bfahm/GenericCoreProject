using System.Collections.Generic;

namespace GenericCore.Persistance.BaseRepository
{
    public interface IBaseRepository<T>
    {
        T GetById(int Id);
        IEnumerable<T> GetAll();
        T Add(T item);
        T Update(T item);
        void Remove(int Id);
    }
}
