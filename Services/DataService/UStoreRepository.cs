using LiteDB;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.DataService
{
    public class UStoreRepository : IRepository
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        const string DB_NAME = "UStore.db";

        public T Store<T>(T entity) where T : IEntity
        {
            try
            {
                using (var db = new LiteDatabase(DB_NAME))
                {
                    var entities = db.GetCollection<T>(entity.GetType().BaseType.Name);

                    if (entity.Id == Guid.Empty)
                    {
                        entities.Insert(entity);
                    }
                    else
                    {
                        entities.Update(entity);
                    }
                    return entity;
                }
            }
            catch (Exception exp)
            {
                logger.Log(LogLevel.Error, exp.Message);
                throw;
            }
        }

        public IEnumerable<T> GetAll<T>(Expression<Func<T, bool>> predicate = null) where T : IEntity
        {
            try
            {
                using (var db = new LiteDatabase(DB_NAME))
                {
                    var entities = db.GetCollection<T>(typeof(T).BaseType.Name);
                    if (predicate == null)
                    {
                        return entities.FindAll();
                    }
                    else
                    {

                        return entities.Find(predicate);
                    }

                }
            }
            catch (Exception exp)
            {
                logger.Log(LogLevel.Error, exp.Message);
                throw;
            }
        }
    }
}
