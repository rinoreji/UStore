using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Services.DataService
{
    public interface IRepository
    {
        T Store<T>(T entity) where T : IEntity;
        IEnumerable<T> GetAll<T>(Expression<Func<T, bool>> predicate = null) where T : IEntity;
    }
}