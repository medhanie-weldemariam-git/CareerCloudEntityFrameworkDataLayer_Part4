using CareerCloud.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.EntityFrameworkDataAccess
{
    public class EFGenericRepository<T> : IDataRepository<T> where T: class
    {
        private CareerCloudContext _context;

        public EFGenericRepository()
        {
            _context = new CareerCloudContext();
        }

        public void Add(params T[] items)
        {
            foreach(T item in items)
            {
                _context.Entry<T>(item).State = EntityState.Added; 
            }
            _context.SaveChanges();
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
        {
            /*My code is here*/

            //IQueryable<T> query = _context.Set<T>();

            //foreach (Expression<Func<T, object>> navPro in navigationProperties)
            //{
            //    query = query.Include<T, object>(navPro);
            //}

            //return query.ToList();
            throw new NotImplementedException();
        }

        public IList<T> GetList(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            /*My code is here*/

            //IQueryable<T> query = _context.Set<T>();
            //foreach (Expression<Func<T, object>> navPro in navigationProperties)
            //{
            //    query = query.Include<T, object>(navPro);
            //}

            //return query.Where(where).ToList();
            throw new NotImplementedException();
        }

        public T GetSingle(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (Expression<Func<T, object>> navPro in navigationProperties)
            {
                query = query.Include<T, object>(navPro);
            }

            return query.FirstOrDefault(where);

            //throw new NotImplementedException();
        }

        public void Remove(params T[] items)
        {
            foreach (T item in items)
            {
                _context.Entry<T>(item).State = EntityState.Deleted;
            }
            _context.SaveChanges();
        }

        public void Update(params T[] items)
        {
            foreach (T item in items)
            {
                _context.Entry<T>(item).State = EntityState.Modified;
            }
            _context.SaveChanges();
        }
    }
}
