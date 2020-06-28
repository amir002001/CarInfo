using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace CarLib
{
    /// <summary>
    /// An instantiable class with a list that contains Cars and relative helper methods.
    /// </summary>
    public class CarRepository
    {
        /// <summary>
        /// A list containing cars
        /// </summary>
        private List<Car> _carList;
        /// <summary>
        /// Constructor that only initializes Car list.
        /// </summary>
        public CarRepository()
        {
            _carList = new List<Car>();
        }
        /// <summary>
        /// Constructor that initializes the Car list based on hard coded Cars.
        /// </summary>
        /// <param name="cars">
        /// Hard coded cars used to populate the list.
        /// </param>
        public CarRepository(params Car[] cars)
        {
            _carList = new List<Car>(cars);
        }
        /// <summary>
        /// Adds a car to the Car list.
        /// </summary>
        /// <param name="c">
        /// Car object to be added to the Car list.
        /// </param>
        public void AddCar(Car c)
        {
            _carList.Add(c);
        }
        /// <summary>
        /// Returns a reference to a car object with the provided Vin number, returns null if none is found.
        /// </summary>
        /// <param name="vinNumber">
        /// The vin number the method is supposed to look for.
        /// </param>
        /// <returns>
        /// A reference to the Car object with that Vin Number.
        /// </returns>
        public Car GetByVin(string vinNumber)
        {
            foreach (Car c in _carList)
            {
                if (c.VinNumber == vinNumber)
                {
                    return c;
                }
            }
            return null;
        }
        /// <summary>
        /// A method that returns a deep copy of Car list 
        /// </summary>
        /// <returns>
        /// A deep copy of Car list.
        /// </returns>
        public List<Car> List()
        {
            
            return new List<Car>(_carList);
        }
        /// <summary>
        /// Updates a car based on a vin number. Throws exception if none is found.
        /// </summary>
        /// <param name="vinNumber">
        /// vin number to look for
        /// </param>
        /// <param name="carMake">
        /// car make to be updated
        /// </param>
        /// <param name="type">
        /// car type to be updated
        /// </param>
        /// <param name="purchasePrice">
        /// purchase price to be updated
        /// </param>
        /// <param name="modelYear">
        /// model year to be updated.
        /// </param>
        /// <param name="mileage">
        /// mileage to be updated
        /// </param>
        public void UpdateCar(string vinNumber, string carMake, CarType type, float purchasePrice, int modelYear, int mileage)
        {
            Car toBeUpdated = GetByVin(vinNumber);
            if (toBeUpdated is null)
                throw new Exception("Car with selected Vin doesn't exist and therefore, can't be be updated.");

            toBeUpdated.CarMake = carMake;
            toBeUpdated.Type = type;
            toBeUpdated.PurchasePrice = purchasePrice;
            toBeUpdated.ModelYear = modelYear;
            toBeUpdated.Mileage = mileage;
        }
    }
}
