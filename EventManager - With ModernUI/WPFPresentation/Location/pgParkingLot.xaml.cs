using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LogicLayerInterfaces;
using DataObjects;
using LogicLayer;
using DataAccessFakes;
using Microsoft.Win32;

namespace WPFPresentation.Location
{
    public partial class pgParkingLot : Page
    {        
        DataObjects.Location _location = null;
        User _user = null;
        ManagerProvider _managerProvider = null;
        Dictionary<ParkingLotVM, BitmapImage> parkingLotAndImage = new Dictionary<ParkingLotVM, BitmapImage>();
        bool _isAddingNewLot = false;
        string _originalImagePath = "";
        string _oldFileName = "";
        string _newFileName = "";

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/02
        /// 
        /// Description:
        /// Page constructor for ParkingLot
        /// </summary>
        /// <param name="managerProvider">The manager provider object</param>
        /// <param name="location">Location of Parking Lot</param>
        /// <param name="user">Current user</param>
        internal pgParkingLot(ManagerProvider managerProvider, DataObjects.Location location, User user)
        {
            _managerProvider = managerProvider;
            _location = location;
            _user = user;

            InitializeComponent();

            setupParkingLotImageDictionary();
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/02
        /// 
        /// Description:
        /// Helper method for creating the Dictionary that holds the ParkingLot object and the image associated
        /// </summary>
        private void setupParkingLotImageDictionary()
        {
            try
            {
                List<ParkingLotVM> parkingLotVMs = _managerProvider.ParkingLotManager.RetrieveParkingLotByLocationID(_location.LocationID);
                txtLocationName.Text = _location.Name;

                foreach (ParkingLotVM parkingLot in parkingLotVMs)
                {
                    if (parkingLot.ImageName == null || parkingLot.ImageName == "")
                    {
                        parkingLotAndImage.Add(parkingLot, new BitmapImage());
                    }
                    else
                    {
                        Uri source = _managerProvider.ImageHelper.FindImageSource(parkingLot.ImageName);

                        parkingLotAndImage.Add(parkingLot, new BitmapImage(source));
                    }
                }

                icParkingLots.ItemsSource = parkingLotAndImage;

            }
            catch (Exception ex)
            {
                MessageBox.Show("There was a problem finding the parking lots for this location.\n" + ex.Message, "Parking Lot Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/02
        /// 
        /// Description:
        /// Click event handler for creating a new parking lot
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddParkingLot_Click(object sender, RoutedEventArgs e)
        {
            // set up ParkingLotVM
            ParkingLotVM newParkingLot = new ParkingLotVM()
            {
                LocationID = _location.LocationID,
                Name = "",
                Description = "",
                ImageName = null,
                Active = true,
                LocationName = _location.Name
            };

            if (parkingLotAndImage.Count == 0)
            {
                parkingLotAndImage.Add(newParkingLot, new BitmapImage());
                _isAddingNewLot = true;
            }
            else if (_isAddingNewLot)
            {
                MessageBox.Show("Already adding a new parking lot.", "Already Adding", MessageBoxButton.OK, MessageBoxImage.Information);                
            }
            else
            {
                _isAddingNewLot = true;
                parkingLotAndImage = new Dictionary<ParkingLotVM, BitmapImage>();
                parkingLotAndImage.Add(newParkingLot, new BitmapImage());
                setupParkingLotImageDictionary();
            }

            icParkingLots.ItemsSource = parkingLotAndImage;

        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/02
        /// 
        /// Description:
        /// Initialization handler for text boxes in the icParkingLot items control.
        /// Changes the text box from read only true to read only false.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_Initialized(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Text == "")
            {
                textBox.IsReadOnly = false;
            }
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/02
        /// Initialization handler for buttons "Save" and "Cancel" in the icParkingLot items control.
        /// Changes the buttons so that they are visible.
        /// 
        /// Description:
        /// Inserts a parking lot object into the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Initialized(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            //if (button.Tag.ToString() == "" || button.Tag.ToString() == "System.Windows.Media.Imaging.BitmapImage")
            if (_isAddingNewLot && button.Tag.ToString() == "")
            {
                button.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/02
        /// Description:
        /// Save button click event handler. Creates a new parking lot object and updates the list
        /// </summary>        
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Save button click
            ParkingLotVM lotToCreate = parkingLotAndImage.First(l => l.Key.LotID == 0).Key;

            if (lotToCreate.Name == null || lotToCreate.Name == "")
            {
                MessageBox.Show("Please enter a name for the parking lot in order to add a location.", "Add Lot Name", MessageBoxButton.OK, MessageBoxImage.Information);

                if (_originalImagePath != "")
                {
                    try
                    {
                        _newFileName = _managerProvider.ImageHelper.SaveImageReturnsNewImageName(_oldFileName, _originalImagePath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Problem saving this image.\n" + ex.Message, "Image Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

                // send off to get added to db
                try
                {
                    lotToCreate.ImageName = _newFileName;
                    _managerProvider.ParkingLotManager.CreateParkingLot(lotToCreate);
                    parkingLotAndImage = new Dictionary<ParkingLotVM, BitmapImage>();
                    setupParkingLotImageDictionary();
                    _isAddingNewLot = false;
                    _newFileName = "";
                    _oldFileName = "";
                    _originalImagePath = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("There was an issue adding the parking lot information.\n" + ex.Message, "Problem Adding Parking Lot", MessageBoxButton.OK, MessageBoxImage.Error);
                }


            }


        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/02
        /// 
        /// Description:
        /// Cancel handler for "Cancel" in the icParkingLot items control.
        /// Cancels adding a new parking lot
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you would like to cancel? Any unsaved work will be lost.", "Are you sure you would like to cancel?", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

            switch (result)
            {                
                case MessageBoxResult.Cancel:
                    break;
                case MessageBoxResult.Yes:
                    _isAddingNewLot = false;
                    parkingLotAndImage = new Dictionary<ParkingLotVM, BitmapImage>();
                    setupParkingLotImageDictionary();
                    break;
                case MessageBoxResult.No:
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/03
        /// 
        /// Description:
        /// Click event handler for stagging an image to be saved
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddImage(object sender, RoutedEventArgs e)
        {
            // sources
            // https://wpf-tutorial.com/dialogs/the-openfiledialog/
            //https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/file-system/how-to-copy-delete-and-move-files-and-folders

            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Image Files|*.jpg;*.jpeg;*.png";

            if (openFile.ShowDialog() == true)
            {
                _originalImagePath = openFile.FileName;
                _oldFileName = openFile.SafeFileName;
                
                // preview image would be awesome
                Button button = (Button)sender;
                button.Width = 300;
                button.Content = "Added! Save the image or hit cancel.";
                button.IsEnabled = false;
            }
        }
    }
}
