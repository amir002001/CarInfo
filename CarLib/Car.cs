using System;
using System.Collections.Generic;
using System.Text;


namespace CarLib
{
    /// <summary>
    /// Car class that can instantiate and modify a Car
    /// </summary>
    public class Car
    {
        private string _vinNumber;
        private string _carMake;
        private CarType _type;
        private float _purchasePrice;
        private int _modelYear;
        private int _mileage;
        /// <summary>
        /// A static list to keep track of all Vin numbers created.
        /// </summary>
        private static List<string> _vinList = new List<string>();

        /// <summary>
        /// Property for Vin number. If Vin number exists within _vinList, an exception is thrown, otherwise the new Vin replaces the old one.
        /// </summary>
        public string VinNumber
        {
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("Vin Number is required");
                if (_vinList.Contains(value))
                    throw new Exception("vin number exists");
                _vinList.Remove(_vinNumber);
                this._vinNumber = value;
                _vinList.Add(_vinNumber);
            }
            get => this._vinNumber;
        }
        /// <summary>
        /// property for car make.
        /// </summary>
        public string CarMake
        {
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("Car make is required");
                this._carMake = value;
            }
            get => this._carMake;
        }
        /// <summary>
        /// property for car type.
        /// </summary>
        public CarType Type
        {
            set => this._type = value;
            get => this._type;
        }
        /// <summary>
        /// property for purchase price.
        /// </summary>
        public float PurchasePrice
        {
            set
            {
                if (value < 0)
                    throw new Exception("Purchase price can't be negative");
                this._purchasePrice = value;
            }
            get => this._purchasePrice;
        }

        /// <summary>
        /// property for model year.
        /// </summary>
        public int ModelYear
        {
            set
            {
                if (value < 2010 || value > 2020)
                    throw new Exception("Model Year is impossible.");
                this._modelYear = value;
            }
            get => this._modelYear;
        }

        /// <summary>
        /// property for mileage.
        /// </summary>
        public int Mileage
        {
            set
            {
                if (value < 0)
                    throw new Exception("Mileage can't be negative");
                this._mileage = value;
            }
            get => this._mileage;
        }
        /// <summary>
        /// calculated property for the car's depreciation. Since it depreciates 10 percents yearly and 0.9 percent for each 10k Kms, the formula is :
        /// Depreciation = price * 0.991^ceil((mileage/10000)) * 0.9^years
        /// </summary>
        public float TotalDepreciation
        {
            get
            {
                int yearsSinceProduction = DateTime.Now.Year - ModelYear;
                double mileageDepreciation = Math.Pow(0.991, Mileage / 10000);
                double yearDepreciation = Math.Pow(0.9, yearsSinceProduction);
                return (float)(PurchasePrice * mileageDepreciation * yearDepreciation);
            }
        }

        /// <summary>
        /// The constructor for Car that tries to create the instance of a Car, and if it fails, it removes the Vin from _vinList
        /// </summary>
        /// <param name="vinNumber">
        /// The car's Vin number, has to be unique and provided
        /// </param>
        /// <param name="carMake">
        /// The Car maker's company. Has to be provided
        /// </param>
        /// <param name="type">
        /// The car's type, found in CarType.cs
        /// </param>
        /// <param name="purchasePrice">
        /// The initial purchase price. Has to be 0 or bigger.
        /// </param>
        /// <param name="modelYear">
        /// Car's model year. Must be between 2010 and 2020
        /// </param>
        /// <param name="mileage">
        /// Car's mileage. Can't be negative.
        /// </param>
        public Car(string vinNumber, string carMake, CarType type, float purchasePrice, int modelYear, int mileage)
        {
            try
            {
                VinNumber = vinNumber;
                CarMake = carMake;
                Type = type;
                PurchasePrice = purchasePrice;
                ModelYear = modelYear;
                Mileage = mileage;
            }
            catch
            {
                _vinList.Remove(VinNumber);
                throw;
            }

        }
        /// <summary>
        /// Provides a string representation of the car.
        /// </summary>
        /// <returns>
        /// The string to be used as it's representation.
        /// </returns>
        public override string ToString()
        {
            string returnString = String.Format("Car with Vin = {0} details", VinNumber);
            return returnString;
        }
    }
}
