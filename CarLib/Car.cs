using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace CarLib
{
    public class Car
    {
        private string _vinNumber;
        private string _carMake;
        private CarType _type;
        private float _purchasePrice;
        private int _modelYear;
        private int _mileage;
        private static List<string> _vinList = new List<string>();


        [Required(ErrorMessage = "Vin Number is required")]
        public string VinNumber
        {
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ValidationException("Vin Number is required");
                if (_vinList.Contains(value))
                    throw new ValidationException("vin number exists");
                _vinList.Remove(_vinNumber);
                this._vinNumber = value;
                _vinList.Add(_vinNumber);
            }
            get => this._vinNumber;
        }

        public string CarMake
        {
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ValidationException("Car make is required");
                this._carMake = value;
            }
            get => this._carMake;
        }
        public CarType Type
        {
            set => this._type = value;
            get => this._type;
        }

        public float PurchasePrice
        {
            set
            {
                if (value < 0)
                    throw new ValidationException("Purchase price can't be negative");
                this._purchasePrice = value;
            }
            get => this._purchasePrice;
        }


        public int ModelYear
        {
            set
            {
                if (value < 2010 || value > 2020)
                    throw new ValidationException("Model Year is impossible.");
                this._modelYear = value;
            }
            get => this._modelYear;
        }


        public int Mileage
        {
            set
            {
                if (value < 0)
                    throw new ValidationException("Mileage can't be negative");
                this._mileage = value;
            }
            get => this._mileage;
        }
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

        public override string ToString()
        {
            string returnString = String.Format("Car with Vin = {0} details", VinNumber);
            return returnString;
        }
    }
}
