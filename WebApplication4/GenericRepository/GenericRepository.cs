using Microsoft.Build.Construction;
using System.Data.Entity;
using WebApplication4.Models.Context;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace WebApplication4.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public MeetingSchedulerContext _context=null;
        public Microsoft.EntityFrameworkCore.DbSet<T> table = null;
        public GenericRepository()
        {
            this._context = new MeetingSchedulerContext();
            
            table = _context.Set<T>();
        }

        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }
        public T GetById(object id) {
            return table.Find(id);
        }

        public void Insert(T obj)
        {
            table.Add(obj);
        
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(T obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
