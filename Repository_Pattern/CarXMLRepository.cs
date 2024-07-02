using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using Repository_Domain;
using Repository_Source;

namespace Repository_Pattern

{
    //Implementation of a car repository using XML as the data source
    public class CarXMLRepository : XMLRepositoryBase<XMLSet<Car>, Car, int>, ICarRepository
    {
        // Constructor to initialize the repository with the XML data source
        public CarXMLRepository() : base("CarInformation.xml")
        {

        }
        // Custom method to retrieve a specific car by its ID
        public Car GetCarById(int carId)
        {
            // Use LINQ to find the first car matching the specified CarID
            return GetAll().FirstOrDefault(c => c.CarID == carId);
        }
        // Custom method to retrieve a specific car by its name
        public Car GetCarByName(string carName)
        {
            return GetAll().FirstOrDefault(c => c.CarName == carName);
        }
        // Custom method to retrieve a list of cars based on license plate
        public List<Car> GetCarByLicensePlate(string licensePlate)
        {
            var allCourses = GetAll();
            return allCourses
                .Where(c => c.LicensePlate.Equals(licensePlate, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
        // Custom method to remove cars by license plate
        public bool RemoveByLicensePlate(string licensePlate)
        {
            try
            {
                var carToRemove = m_context.Data.Where(c => c.LicensePlate == licensePlate).ToList();
                foreach (var car in carToRemove)
                {
                    m_context.Data.Remove(car);
                }
                m_context.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        // Custom method to remove all cars from the repository
        public bool RemoveAll()
        {
            try
            {
                m_context.Data.Clear();
                m_context.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Custom method to update a car's information by ID
        public bool Update(int carId, Car updatedCar)
        {
            try
            {
                var car = m_context.Data.FirstOrDefault(c => c.CarID == carId);
                if (car != null)
                {
                    // Update the car properties with the values from updatedCourse
                    car.CarID = carId;
                    car.CarName = updatedCar.CarName;
                    car.LicensePlate = updatedCar.LicensePlate;
                    car.CustomerName = updatedCar.CustomerName;
                    car.CellPhoneNo = updatedCar.CellPhoneNo;

                    // Save changes to the XML file
                    m_context.Save();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
       
    }
}
