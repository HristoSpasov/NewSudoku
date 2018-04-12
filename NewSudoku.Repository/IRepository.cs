namespace NewSudoku.Repository
{
    using System.Collections.Generic;

    public interface IRepository<TEntity, TKey> where TEntity : class
    {
        ICollection<TEntity> GetAll();

        TEntity Get(TKey id);

        TKey Insert(TEntity model);

        bool Update(TEntity model);

        bool Remove(TKey id);
    }
}