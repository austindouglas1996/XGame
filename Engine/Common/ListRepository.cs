using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using XGameEngine;

namespace XGameEngine
{
    public class ListRepository<TEntity> : IRepository<TEntity>
        where TEntity : GameObject
    {
        /// <summary>
        /// Private List<T> to use for reference.
        /// </summary>
        private List<TEntity> _Entities = new List<TEntity>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ListRepository{TEntity}"/> class.
        /// </summary>
        /// <param name="entities"></param>
        public ListRepository(List<TEntity> entities, GameObject owner = null)
        {
            _Entities = entities;
            Owner = owner;
        }

        /// <summary>
        /// Returns an item based on index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public TEntity this[int index]
        {
            get
            {
                return _Entities[index];
            }

            set
            {
                _Entities[index] = value;
            }
        }

        /// <summary>
        /// Automatically remove the inactive entities.
        /// </summary>
        public bool RemoveInActive
        {
            get { return _RemoveInActive; }
            set { _RemoveInActive = value; }
        }
        private bool _RemoveInActive = true;

        /// <summary>
        /// Intializes a new instance of the <see cref="ListRepository{TEntity}"/> class.
        /// </summary>
        /// <remarks></remarks>
        public ListRepository()
        {

        }

        /// <summary>
        /// When this value is not null. Sets any added items <see cref="GameObject.Parent"/> value 
        /// to this object.
        /// </summary>
        public GameObject? Owner { get; set; }

        /// <summary>
        /// Returns the amount of entities.
        /// </summary>
        public int Count()
        {
            return this._Entities.Count();
        }

        /// <summary>
        /// Adds a new child to the repository.
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Add(TEntity entity)
        {
            if (Owner != null)
                entity.Parent = Owner;

            _Entities.Add(entity);
        }

        /// <summary>
        /// Adds a collection of entities to the repository.
        /// </summary>
        /// <param name="entities"></param>
        public void AddRange(params TEntity[] entities)
        {
            _Entities.AddRange(entities);
        }

        /// <summary>
        /// Edit a item within the repository.
        /// </summary>
        /// <param name="newEntity"></param>
        /// <remarks>Important: This is not supported in a List repository.</remarks>
        public virtual void Update(GameTime gameTime)
        {
            List<TEntity> ToRemove = new List<TEntity>();

            for (int i = 0; i < _Entities.Count; i++)
            {
                TEntity entity = _Entities[i];

                if (entity.IsActive == false && this.RemoveInActive)
                {
                    ToRemove.Add(entity);
                    continue;
                }

                entity.Update(gameTime);
            }

            // Remove the inactive entities.
            foreach (var entity in ToRemove)
                this._Entities.Remove(entity);
        }

        /// <summary>
        /// Draw the entities in the list.
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Draw(GameTime gameTime)
        {
            foreach (var entity in this._Entities)
                entity.Draw(gameTime);
        }

        /// <summary>
        /// Finds a group of entities based on a predicate function.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> Find(Func<TEntity, bool> predicate)
        {
            return _Entities.FindAll(new Predicate<TEntity>(predicate));
        }

        /// <summary>
        /// Returns the first or default value of a <see cref="TEntity"/> based on a predicate function.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public TEntity? FirstOrDefault(Func<TEntity, bool> predicate)
        {
            IEnumerable<TEntity> val = Find(predicate);

            if (val == null)
                return default(TEntity);
            else
                return ((List<TEntity>)val)[0];
        }

        /// <summary>
        /// Gets an entity based on ID.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public TEntity Get(int ID)
        {
            return FirstOrDefault(g => ((TEntity)g).ID == ID);
        }

        /// <summary>
        /// Gets all entities within the repository.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TEntity> GetAll()
        {
            return _Entities;
        }

        /// <summary>
        /// Removes a certain entity from the repository.
        /// </summary>
        /// <param name="entity"></param>
        public void Remove(TEntity entity)
        {
            _Entities.Remove(entity);
        }

        /// <summary>
        /// Removes all entities from the repository.
        /// </summary>
        public void RemoveAll()
        {
            _Entities.Clear();
        }

        /// <summary>
        /// Removes a range of entities from the repository.
        /// </summary>
        /// <param name="entities"></param>
        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            foreach (var val in entities)
                _Entities.Remove(val);
        }

        /// <summary>
        /// Returns one entity or default value based on a predicate function.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public TEntity? SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return FirstOrDefault(new Func<TEntity, bool>(predicate.Compile()));
        }
    }
}
