using BloggingAPI.Model.Base;
using BloggingAPI.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace BloggingAPI.Repository.Generic
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        private MySQLContext _context;
        private DbSet<T> dataset;

        public GenericRepository(MySQLContext context)
        {
            _context = context;
            dataset = _context.Set<T>();
        }

        public T Create(T item)
        {
            try
            {
                dataset.Add(item);
                _context.SaveChanges();
                return item;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred");
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public void Delete(int id)
        {
            var result = dataset.SingleOrDefault(p => p.Id == id);
            if (result != null)
            {
                try
                {
                    dataset.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred");
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }
        }

        public List<T> FindAll()
        {
            return dataset.ToList();
        }

        public T FindByID(int id)
        {
            return dataset.SingleOrDefault(p => p.Id == id);
        }

        public T Update(T item)
        {
            if (!Exists(item.Id)) return null;

            var result = dataset.SingleOrDefault(p => p.Id == item.Id);

            if (result != null)
            {
                try
                {
                    dataset.Entry(result).CurrentValues.SetValues(item);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred");
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }

            return item;
        }

        private bool Exists(int id)
        {
            return dataset.Any(p => p.Id == id);
        }
    }
}
