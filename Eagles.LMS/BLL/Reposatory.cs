using Eagles.LMS.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Eagles.LMS.BLL
{
    public class Reposatory<TEntity> : IReposatory<TEntity> where TEntity : class
    {

        protected EmcNewsContext ctx;
        DbSet<TEntity> Set;
        public Reposatory(EmcNewsContext _ctx)
        {
            ctx = _ctx;
            Set = ctx.Set<TEntity>();
        }

        public TEntity GetBy(int key)
        {
            try
            {
                return Set.Find(key);
            }
            catch { return null; }
        }
        
        public TEntity Add(TEntity entity)
        {
            TEntity result;
            Set.Add(entity);
            result = ctx.SaveChanges() > 0 ? entity : null;


            return result;
        }

        public IEnumerable<TEntity> Add(ICollection<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                Set.Add(entity);
            }
            return ctx.SaveChanges() > 0 ? entities : null;
        }

        public bool Delete(TEntity entity)
        {


            if (!Set.Local.Contains(entity))
            {
                Set.Attach(entity);
            }
            Set.Remove(entity);
            return ctx.SaveChanges() > 0;

        }

        public bool Delete(int id)
        {
            Set.Remove(GetBy(id));
            return ctx.SaveChanges() > 0;

        }
        public bool Delete(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                Set.Remove(entity);
            }
            return ctx.SaveChanges() > 0;

        }

        public IEnumerable<TEntity> GetAll()
        {
            return Set;
        }

        public List<TEntity> GetAllBind()
        {
            return Set.ToList();
        }

        public bool UpdateCollectionWithSave(ICollection<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                Set.Attach(entity);
                ctx.Entry(entity).State = EntityState.Modified;
            }
            return ctx.SaveChanges() > 0;
        }

        public void UpdateWithoutSave(TEntity entity)
        {
            Set.Attach(entity);
            ctx.Entry(entity).State = EntityState.Modified;
        }

        public bool UpdateWithSave(TEntity entity)
        {

            Set.Attach(entity);
            ctx.Entry(entity).State = EntityState.Modified;
            return ctx.SaveChanges() > 0;

        }
        public bool SaveChanges()
        {
            return ctx.SaveChanges() > 0;
        }
        public bool Edit(TEntity entity)
        {
            Set.Attach(entity);
            ctx.Entry(entity).State = EntityState.Modified;
            return SaveChanges();
        }
    }
}