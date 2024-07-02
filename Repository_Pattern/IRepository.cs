using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Pattern
{
    /*
     The use of the IRepository interface indicates that the system is following 
    the repository pattern. This pattern provides an abstraction layer between 
    the application code and the data storage, making it easier to switch between 
    different data sources (e.g., databases, XML files)
    without affecting the rest of the application.
     */
    public interface IRepository<TEntity, TKey> where TEntity : class
    {
        // Method to retrieve all entities from the repository
        ICollection<TEntity> GetAll();
        // Method to find entities based on a specified predicate
        ICollection<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        // Method to retrieve a single entity by its primary key
        TEntity Get(TKey id);
        // Method to insert a new entity into the repository and return its primary key
        TKey Insert(TEntity model);
        // Method to update an existing entity in the repository
        bool Update(TEntity model);
        // Method to remove an entity from the repository based on its primary key
        bool Remove(TKey id);
        
    }
}