using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository_Domain;
using Repository_Pattern;

namespace CarCare_Project
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create an instance of ICarRepository using RepositoryFactory
            ICarRepository source = RepositoryFactory.Create<ICarRepository>(ContextTypes.XMLSource);
            // Flag to control the main application loop
            bool isRun = true;
            // Main application loop
            while (isRun)
            {
                Console.Clear();
                Console.WriteLine("------------ Select Here -----------");
                Console.WriteLine("Press 1 :  Get a Car Information");
                Console.WriteLine("Press 2 :  Create a New Car Information");
                Console.WriteLine("Press 3 : Update Car Information");
                Console.WriteLine("Press 4 : Delete Car Information");
                Console.WriteLine("Press 5 : Exit The Application");
                // Read user input for menu selection
                string inputKey = Console.ReadLine();
                Console.Clear();
                // Menu options
                if (inputKey == "1")
                {
                    // Sub-menu for getting car information
                    Console.WriteLine("Select one");
                    Console.WriteLine("1. Get car information by ID");
                    Console.WriteLine("2. Get car information by CarName");
                    Console.WriteLine("3. Get car information by LicensePlate");
                    Console.WriteLine("4. Get all car information");
                    // Read user input for sub-menu selection
                    string option = Console.ReadLine();
                    // Get car information by ID
                    if (option == "1")
                    {
                        Console.Write("Enter Car ID: ");
                        if (int.TryParse(Console.ReadLine(), out int carId))
                        {
                            var car = source.GetCarById(carId);
                            if (car != null)
                            {
                                DisplayCarInformation(car);
                            }
                            else
                            {
                                Console.WriteLine("Car not found.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid input for Car ID.");
                        }
                    }
                    // Similar logic for other options (Get car information by CarName, LicensePlate, and all cars)
                    else if (option == "2")
                    {
                        Console.Write("Enter Car Name: ");
                        string carName = Console.ReadLine();
                        var car = source.GetCarByName(carName);
                        if (car != null)
                        {
                            DisplayCarInformation(car);
                        }
                        else
                        {
                            Console.WriteLine("Car not found.");
                        }
                    }
                    else if (option == "3")
                    {
                        Console.Write("Enter Car Type: ");
                        string carLicensePlate = Console.ReadLine();
                        var allcars = source.GetCarByLicensePlate(carLicensePlate);
                        if (allcars.Count > 0)
                        {
                            foreach (var car in allcars)
                            {
                                DisplayCarInformation(car);
                                Console.WriteLine();
                            }
                        }
                        else
                        {
                            Console.WriteLine("No cars found for the specified type.");
                        }
                    }
                    else if (option == "4")
                    {
                        var cars = source.GetAll();
                        foreach (var car in cars)
                        {
                            DisplayCarInformation(car);
                            Console.WriteLine();
                        }
                    }

                    Console.Write("Press any key to continue...");
                    Console.ReadKey();
                }
                else if (inputKey == "2")
                {
                    // Option to create a new car
                    bool createAnotherCar = true;
                    while (createAnotherCar)
                    {
                        // Create a new Car object and populate its properties
                        Car car = new Car();
                        Console.Write("Enter Car ID: ");
                        car.CarID = int.Parse(Console.ReadLine());
                        // Similar logic for other properties (CarName, LicensePlate, CustomerName, CellPhoneNo)
                        Console.Write("Enter Car Name: ");
                        car.CarName = Console.ReadLine();

                        Console.Write("Enter License Plate: ");
                        car.LicensePlate = Console.ReadLine();

                        Console.Write("Enter Customer Name: ");
                        car.CustomerName = Console.ReadLine();

                        Console.Write("Enter CellPhoneNo: ");
                        car.CellPhoneNo = Console.ReadLine();
                        // Insert the new car into the repository
                        try
                        {
                            source.Insert(car);
                            Console.WriteLine($"Car {car.CarID} created successfully!");
                            // Ask if the user wants to create another car
                            Console.Write("Do you want to create another car? (yes/no): ");
                            string response = Console.ReadLine().ToLower();
                            createAnotherCar = response == "yes";
                        }
                        catch (Exception ex)
                        {
                            Console.Write(ex);
                            Console.ReadKey();
                        }
                    }
                }
                // Similar logic for updating and deleting car information (options 3 and 4)
                else if (inputKey == "3")
                {
                  
                    Console.Write("Enter Car ID to update: ");
                    if (int.TryParse(Console.ReadLine(), out int carIdToUpdate))
                    {

                        var carToUpdate = new Car();
                        carToUpdate.CarID = carIdToUpdate;
                        Console.WriteLine("Updated Car ID : ");
                        Console.Write("Updated Car Name : ");
                        carToUpdate.CarName = Console.ReadLine();
                        Console.Write("Updated License Plate  : ");
                        carToUpdate.LicensePlate = Console.ReadLine();
                        Console.Write("Updated Customer Name. : ");
                        carToUpdate.CustomerName = Console.ReadLine();
                        Console.Write("Updated CellPhone No : ");
                        carToUpdate.CellPhoneNo = (Console.ReadLine());

                        // Use the existing 'source' variable declared outside the block
                        if (source.Update(carIdToUpdate, carToUpdate))
                        {
                            Console.WriteLine("Car updated successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Failed to update car. Car not found.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input for Car ID.");
                    }
                }
                else if (inputKey == "4")
                {
                    Console.WriteLine("Select option:");
                    Console.WriteLine("1. Remove car by ID");
                    Console.WriteLine("2. Remove cars by LicensePlate");
                    Console.WriteLine("3. Remove all cars");
                    string removeOption = Console.ReadLine();

                    if (removeOption == "1")
                    {
                        Console.Write("Enter Car ID to delete: ");
                        if (int.TryParse(Console.ReadLine(), out int carIdToDelete))
                        {
                            if (source.Remove(carIdToDelete))
                            {
                                Console.WriteLine("Car deleted successfully!");
                            }
                            else
                            {
                                Console.WriteLine("Car not found.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid input for Car ID.");
                        }
                    }
                    else if (removeOption == "2")
                    {
                        Console.Write("Enter Car LicensePlate to delete: ");
                        string carLicensePlateToDelete = Console.ReadLine();
                        if (source.RemoveByLicensePlate(carLicensePlateToDelete))
                        {
                            Console.WriteLine($"Cars of type '{carLicensePlateToDelete}' deleted successfully!");
                        }
                        else
                        {
                            Console.WriteLine($"No Cars found with type '{carLicensePlateToDelete}'.");
                        }
                    }
                    else if (removeOption == "3")
                    {
                        if (source.RemoveAll())
                        {
                            Console.WriteLine("All Cars deleted successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Failed to delete all Car.");
                        }
                    }
                    Console.Write("Press any key to continue...");
                    Console.ReadKey();
                }
                // Exit the application in option 5
                else if (inputKey == "5")
                {
                    isRun = false;
                }
            }
        }
        // Method to display car information
        public static void DisplayCarInformation(Car car)
        {
            Console.WriteLine("-------------Car Information -----------");
            Console.WriteLine($"Car ID: {car.CarID}");
            Console.WriteLine($"Car Name: {car.CarName}");
            Console.WriteLine($" LicensePlate: {car.LicensePlate}");
            Console.WriteLine($"CustomerName: {car.CustomerName} ");
            Console.WriteLine($"CellPhoneNo: {car.CellPhoneNo} ");
        }
    }
}



                