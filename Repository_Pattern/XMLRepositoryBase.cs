using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Repository_Domain;
using Repository_Source;

namespace Repository_Pattern
{

    // Generic XML-based repository implementation
    public class XMLRepositoryBase<TContext, TEntity, TKey> : IRepository<TEntity, TKey>
        where TContext : XMLSet<TEntity>
        where TEntity : class
    {
        // The XML context to interact with XML data
        protected XMLSet<TEntity> m_context;

        // Constructor to initialize the XML context with the specified file name
        public XMLRepositoryBase(string fileName)
        {
            m_context = new XMLSet<TEntity>(fileName);
        }

        // Delete method to remove an entity by its unique identifier
        public bool Remove(TKey id)
        {
            try
            {
                // Convert data to a list of IEntity to access the CarID property
                List<IEntity<TKey>> items = m_context.Data as List<IEntity<TKey>>;
                // Find and remove the item with the specified id
                items.Remove(items.First(f => f.CarID.Equals(id)));
                // Update the context with the modified list
                m_context.Data = items as ICollection<TEntity>;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Find method to retrieve entities based on a predicate
        public ICollection<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                // Use LINQ to filter the data based on the provided predicate
                var list = m_context.Data.AsQueryable().Where(predicate).ToList();
                return list as ICollection<TEntity>;
            }
            catch (Exception)
            {
                return null;
            }
        }

        // Get method to retrieve an entity by its unique identifier
        public TEntity Get(TKey id)
        {
            try
            {
                // Convert data to a list of IEntity to access the CarID property
                List<IEntity<TKey>> items = m_context.Data as List<IEntity<TKey>>;
                // Find and return the item with the specified id
                return items.FirstOrDefault(f => f.CarID.Equals(id)) as TEntity;
            }
            catch (Exception)
            {
                return null;
            }
        }

        // GetAll method to retrieve all entities
        public ICollection<TEntity> GetAll()
        {
            return m_context.Data;
        }

        // Insert method to add a new entity to the repository
        public TKey Insert(TEntity model)
        {
            var list = m_context.Data;
            list.Add(model);
            // Update the context with the modified list
            m_context.Data = list;
            return default(TKey);
        }

        // Remove method, equivalent to Delete
        public bool Update(TEntity model)
        {
            try
            {
                // Cast the input model to IEntity<TKey> interface
                IEntity<TKey> imodel = model as IEntity<TKey>;

                // Get the collection of entities from the context
                List<IEntity<TKey>> items = m_context.Data as List<IEntity<TKey>>;

                // Remove the existing entity with the same primary key (CarID)
                items.Remove(items.FirstOrDefault(f => f.CarID.Equals(imodel.CarID)));

                // Add the updated entity to the collection
                items.Add(imodel);

                // Update the context's data with the modified collection
                m_context.Data = items as ICollection<TEntity>;

                // Save the changes to the underlying data store
                Save();

                // Return true to indicate a successful update
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public void Save()
        {
            m_context.Save(); // Assuming the XMLSet class has a Save method
        }

        public void Load()
        {
            m_context.Load(); // Assuming the XMLSet class has a Load method
        }
    }
}
