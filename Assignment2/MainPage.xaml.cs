using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using CarLib;
using Windows.UI.Xaml.Media.Imaging;
using System.Collections;



namespace Assignment2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private CarRepository _repo;
        public MainPage()
        {
            this.InitializeComponent();

            Car car1 = new Car("12", "Bugatti", CarType.Coupe, 12999.50f, 2016, 15000);
            Car car2 = new Car("13", "Mercedes", CarType.Sedan, 15999.50f, 2018, 20001);
            Car car3 = new Car("14", "Mercedes", CarType.Hatchback, 14999.50f, 2019, 30000);
            _repo = new CarRepository(car1, car2, car3);
            for (int i = 2010; i < 2021; i++)
            {
                ModelYearCB.Items.Add(i);
            }
            for (int i = 0; i < 3; i++)
            {
                CarTypeCB.Items.Add(Enum.GetName(typeof(CarType), i));
            }
            foreach (Car c in _repo.List())
            {
                CarLST.Items.Add(c);
            }
        }
        /// <summary>
        /// Instantiates a car when clicked and adds it to the CarRepository and the car list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnClickAddCar(object sender, RoutedEventArgs e)
        {
            try
            {
                ArrayList values = BoxValidator();
                Car newCar = new Car((string)values[0], (string)values[1], (CarType)values[2], (float)values[3], (int)values[4], (int)values[5]);

                _repo.AddCar(newCar);
                CarLST.Items.Add(newCar);
                ErrorTBlock.Text = "";
            }
            catch (Exception ex)
            {
                ErrorTBlock.Text = ex.Message;
            }

        }

        /// <summary>
        /// Clears boxes when clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnClickClear(object sender, RoutedEventArgs e)
        {
            VinNumberTB.Text = "";
            CarMakeTB.Text = "";
            CarTypeCB.SelectedItem = null;
            PurchasePriceTB.Text = "";
            ModelYearCB.SelectedItem = null;
            MileageTB.Text = "";
        }
        /// <summary>
        /// Displays car's details when selection is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCarSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Car tmpCar = (Car)CarLST.SelectedItem;
            VinNumberTB.Text = tmpCar.VinNumber;
            CarMakeTB.Text = tmpCar.CarMake;
            CarTypeCB.SelectedIndex = (int)tmpCar.Type;
            PurchasePriceTB.Text = tmpCar.PurchasePrice.ToString();
            ModelYearCB.SelectedItem = tmpCar.ModelYear;
            MileageTB.Text = tmpCar.Mileage.ToString();
            CarIMG.Source = new BitmapImage(new Uri(String.Format("ms-appx:///Assets/CarImages/{0}.png", tmpCar.Type.ToString())));
        }
        /// <summary>
        /// Updates the car selected from the list when clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnClickUpdateCar(object sender, RoutedEventArgs e)
        {
            try
            {
                ArrayList toBePassed = BoxValidator();
                _repo.UpdateCar((string)toBePassed[0], 
                    (string)toBePassed[1], 
                    (CarType)toBePassed[2], 
                    (float)toBePassed[3], 
                    (int)toBePassed[4], 
                    (int)toBePassed[5]);
                ErrorTBlock.Text = "";
            }
            catch (Exception ex)
            {
                ErrorTBlock.Text = ex.Message;
            }

        }
        /// <summary>
        /// Validates the text boxes' contents to the point where it can be parsed and used. The rest is on the Car constructor.
        /// </summary>
        /// <returns>
        /// An arrayList that has all of the objects required to create a car in sequence, which can be accessed by their indexes.
        /// index 0  is Vin number, 1 is Car Make, 2 is Car type, 3 is Purchase Price, 4 is Model Year, 5 is Mileage.
        /// </returns>
        private ArrayList BoxValidator()
        {
            ArrayList arr = new ArrayList();
            string vinNumber = VinNumberTB.Text;
            string carMake = CarMakeTB.Text;
            if (CarTypeCB.SelectedItem is null)
            {
                throw new Exception("Please select a car type.");
            }
            CarType type = (CarType)CarTypeCB.SelectedIndex;
            float purchasePrice;
            if (!float.TryParse(PurchasePriceTB.Text, out purchasePrice))
            {
                throw new Exception("Purchase price has to be a number");
            }
            if (ModelYearCB.SelectedItem is null)
                throw new Exception("Please select a model year.");
            int modelYear = (int)ModelYearCB.SelectedItem;
            int mileage;
            if (!int.TryParse(MileageTB.Text, out mileage))
            {
                throw new Exception("Mileage has to be a number");
            }
            arr.Add(vinNumber);
            arr.Add(carMake);
            arr.Add(type);
            arr.Add(purchasePrice);
            arr.Add(modelYear);
            arr.Add(mileage);
            return arr;
        }

    }
}
