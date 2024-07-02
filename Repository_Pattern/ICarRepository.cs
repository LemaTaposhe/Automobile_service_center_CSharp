using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository_Domain;

namespace Repository_Pattern
{
    /*
     The interface ICarRepository extends the generic IRepository<Car, int>. 
    This means it is defining methods for managing cars where Car is the entity type
    and int is the type of the unique identifier (key) for cars.
     */
    public interface ICarRepository : IRepository<Car, int>
    {
        // Custom method to retrieve a specific car by its ID
        Car GetCarById(int caRId);
        // Custom method to retrieve a specific car by its name
        Car GetCarByName(string carName);
        // Custom method to retrieve a list of cars based on license plate
        List<Car> GetCarByLicensePlate(string licensePlate);
        // Custom method to update a car's information by ID
        bool Update(int courseId, Car updatedCar);
        // Custom method to remove cars by license plate
        bool RemoveByLicensePlate(string licensePlate);
        // Custom method to remove all cars from the repository
        bool RemoveAll();
      
    }
}

