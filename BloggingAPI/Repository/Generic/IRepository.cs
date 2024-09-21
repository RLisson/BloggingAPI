using BloggingAPI.Model.Base;

namespace BloggingAPI.Repository.Generic
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Create(T item);
        T FindByID(int id);
        List<T> FindAll();
        T Update(T item);
        void Delete(int id);
    }
}
