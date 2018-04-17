namespace NewSudoku.Repository.BaseRepositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NewSudoku.Entities;
    using NewSudoku.Repository.Source;

    public class XMLBaseRepository<TContext, TEntity, TKey> : IRepository<TEntity, TKey>
        where TContext : XMLSet<TEntity> where TEntity : class
    {
        private XMLSet<TEntity> context;

        public XMLBaseRepository(string fileName)
        {
            this.context = new XMLSet<TEntity>(fileName);
        }

        public TEntity Get(TKey id)
        {
            List<IEntity<TKey>> items = this.getCastedItems();
            TEntity item = (TEntity)items.FirstOrDefault(i => i.Id.Equals(id));

            return item;
        }

        public ICollection<TEntity> GetAll()
        {
            return this.context.Data;
        }

        public TKey Insert(TEntity model)
        {
            ICollection<TEntity> list = this.context.Data;
            list.Add(model);
            this.context.Data = list;

            return default(TKey);
        }

        public bool Remove(TKey id)
        {
            try
            {
                List<IEntity<TKey>> items = this.getCastedItems();
                items.Remove(items.First(f => f.Id.Equals(id)));
                this.context.Data = (ICollection<TEntity>)items;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(TEntity model)
        {
            try
            {
                IEntity<TKey> imodel = (IEntity<TKey>)model;
                List<IEntity<TKey>> items = this.getCastedItems();
                items.Remove(items.SingleOrDefault(f => f.Id.Equals(imodel.Id)));
                items.Add(imodel);
                var castedItems = items.Select(e => e as TEntity).ToList();
                this.context.Data = castedItems;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private List<IEntity<TKey>> getCastedItems()
        {
            return this.context.Data
                    .Select(e => e as IEntity<TKey>)
                    .ToList();
        }
    }
}