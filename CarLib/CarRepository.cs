using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace CarLib
{
    public class CarRepository
    {
        private List<Car> _carList;
        public CarRepository()
        {
            _carList = new List<Car>();
        }
        public CarRepository(params Car[] cars)
        {
            _carList = new List<Car>(cars);
        }
        public void AddCar(Car c)
        {
            _carList.Add(c);
        }
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
        public List<Car> List()
        {
            return _carList;
        }
        public void UpdateCar(ArrayList arr)
        {
            ArrayList values = arr;
            Car toBeUpdated = GetByVin((string)values[0]);
            if (toBeUpdated is null)
                throw new Exception("Car with selected Vin doesn't exist and therefore, can't be be updated.");
            
            toBeUpdated.CarMake = (string)values[1];
            toBeUpdated.Type = (CarType)values[2];
            toBeUpdated.PurchasePrice = (float)values[3];
            toBeUpdated.ModelYear = (int)values[4];
            toBeUpdated.Mileage = (int)values[5];
            
        }
    }
}
