using MyBlogProject.CORE.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyBlogProject.CORE.Service
{
    public interface ICoreService<T>where T : CoreEntity
    {
        bool Add (T entity);
        bool Add (List<T> entity);
        bool Update (T entity);
        bool Remove (T entity);
        bool Remove (Guid id);
        bool RemoveAll (Expression<Func<T,bool>>exp);
        T GetById (Guid id);
        T GetByDefault (Expression<Func<T, bool>> exp);
        List<T> GetDefault (Expression<Func<T, bool>> exp);
        List<T> GetActive ();
        List<T> GetAll ();
        bool Activate(Guid id);
        bool Any(Expression<Func<T, bool>> exp);
        int Save();
    }
}
