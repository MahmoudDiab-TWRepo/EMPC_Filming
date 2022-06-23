using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.LMS.BLL
{
  public  interface IReposatory<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        List<TEntity> GetAllBind();
        TEntity GetBy(int key);
        TEntity Add(TEntity entity);
        IEnumerable<TEntity> Add(ICollection<TEntity> entities);
        bool Delete(TEntity entity);
        bool Delete(int id);

        bool UpdateWithSave(TEntity entity);
        void UpdateWithoutSave(TEntity entity);
        bool Delete(IEnumerable<TEntity> entities);
        bool UpdateCollectionWithSave(ICollection<TEntity> entities);
    }
}
