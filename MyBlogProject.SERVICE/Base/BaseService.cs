using MyBlogProject.CORE.Entity;
using MyBlogProject.CORE.Service;
using MyBlogProject.MODEL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace MyBlogProject.SERVICE.Base
{
    public class BaseService<T> : ICoreService<T> where T : CoreEntity
    {
        private readonly BlogContext _context;

        public BaseService(BlogContext context)
        {
            _context = context;
        }
        public bool Activate(Guid id)
        {
            T activated = GetById(id);
            activated.Status = CORE.Entity.Enums.Status.Active;
            return Update(activated);
        }

        public bool Add(T entity)
        {
            try
            {
                _context.Set<T>().Add(entity);
                return Save() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Add(List<T> entity)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    _context.Set<T>().AddRange(entity);
                    ts.Complete();
                    return Save() > 0;
                }
            }
            catch (Exception)
            {

                return false;
            }
        }

        //public bool Any(Expression<Func<T, bool>> exp)
        //{
        //    return _context.Set<T>().Any(exp);
        //}

        public bool Any(Expression<Func<T, bool>> exp) => _context.Set<T>().Any(exp);
        public List<T> GetActive() => _context.Set<T>().Where(x => x.Status != CORE.Entity.Enums.Status.Deleted).ToList();

        public List<T> GetAll() => _context.Set<T>().ToList();


        public T GetByDefault(Expression<Func<T, bool>> exp) => _context.Set<T>().FirstOrDefault(exp);

        public T GetById(Guid id)
        {
            return _context.Set<T>().Find(id);//Generic kullanılmak istendiğinde Set<T> kullanılıyor. 
        }

        public List<T> GetDefault(Expression<Func<T, bool>> exp)=>_context.Set<T>().Where(exp).ToList();

        public bool Remove(T entity)
        {
            entity.Status = CORE.Entity.Enums.Status.Deleted;
            return Update(entity);
        }

        public bool Remove(Guid id)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    T removedItem = GetById(id);
                    removedItem.Status = CORE.Entity.Enums.Status.Deleted;
                    ts.Complete();
                    return Update(removedItem);
                }
            }
            catch (Exception)
            {
                    return false;
            }
        }

        public bool RemoveAll(Expression<Func<T, bool>> exp)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    var collection = GetDefault(exp);
                    int count = 0;

                    foreach (var item in collection)
                    {
                        item.Status = CORE.Entity.Enums.Status.Deleted;
                        bool opResult = Update(item);
                        if (opResult) count++;
                    }
                    if (collection.Count()==count)
                    {
                        ts.Complete();
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public bool Update(T entity)
        {
            try
            {
                _context.Set<T>().Update(entity);
                return Save() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
