using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Pattern
{ 
    // Factory class for creating repository instances based on context types
    public static class RepositoryFactory
    {
        // Create method to instantiate repository instances
        public static TRepository Create<TRepository>(ContextTypes ctype) where
       TRepository : class
        {
            // Check if the requested repository type is ICarRepository
            if (typeof(TRepository) == typeof(ICarRepository))
            {
                // If it's ICarRepository, return an instance of CarXMLRepository
                return new CarXMLRepository() as TRepository;
            }
            // If the requested repository type is not recognized, return null
            return null;
        }
    }
}