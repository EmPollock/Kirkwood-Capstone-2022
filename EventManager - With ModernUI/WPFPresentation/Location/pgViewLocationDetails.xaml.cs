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
using LogicLayer;
using LogicLayerInterfaces;
using DataObjects;
using WPFPresentation.Location;
using DataAccessFakes;

namespace WPFPresentation
{
    /// <summary>
    /// Austin Timmerman
    /// Created: 2022/02/03
    /// 
    /// Description:
    /// Interaction logic for pgTaskListCreate.xamls
    /// </summary>
    public partial class pgViewLocationDetails : Page
    {
        ILocationManager _locationManager;
        IEventDateManager _eventDateManager;
        ISublocationManager _sublocationManager;
        IActivityManager _activityManager;
        IEntranceManager _entranceManager;
        
        DataObjects.Location _location;
        int _locationID;
        int _sublocationID;
        List<Reviews> _locationReviews;
        List<LocationImage> _locationImages;
        List<EventDateVM> _eventDatesForLocation;
        List<Sublocation> _sublocations;
        List<Activity> _activitiesForSublocation;

        Uri _src;
        int _imageNumber = 0;

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/03
        /// 
        /// Description:
        /// The custom constructor for the ViewAllVolunteersPage
        /// </summary>
        /// <param name="locationID"></param>
        /// <param name="locationManager"></param>
        public pgViewLocationDetails(int locationID, ILocationManager locationManager, ISublocationManager sublocationManager)
        {
            // use fake accessor
            //_locationManager = new LocationManager(new LocationAccessorFake());
            //_eventDateManager = new EventDateManager(new EventDateAccessorFake());
            //_sublocationManager = new SublocationManager(new SublocationAccessorFake());
            //_activityManager = new ActivityManager(new ActivityAccessorFake());

            // use default accessor
            //_locationManager = new LocationManager();
            _eventDateManager = new EventDateManager();
            _sublocationManager = new SublocationManager();
            _activityManager = new ActivityManager();
            _entranceManager = new EntranceManager();

            _locationManager = locationManager;
            _sublocationManager = sublocationManager;

            _locationID = locationID;
            _location = _locationManager.RetrieveLocationByLocationID(locationID);
            _locationReviews = _locationManager.RetrieveLocationReviews(locationID);
            _locationImages = _locationManager.RetrieveLocationImagesByLocationID(locationID);
            _sublocations = _sublocationManager.RetrieveSublocationsByLocationID(locationID);

            InitializeComponent();
            AppData.DataPath = System.AppDomain.CurrentDomain.BaseDirectory + @"\" + @"Images\LocationImages";
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/09
        /// 
        /// Description:
        /// The helper method that fills the text boxes, text blocks, images, and reviews for the
        /// Location Details page
        /// </summary>
        private void loadLocationDetails()
        {
            hideDetails();
            hideEntrances();
            hideSublocations();
            btnSiteDetails.Background = new SolidColorBrush(Colors.Gray);
            scrLocationDetails.Visibility = Visibility.Visible;

            //scrLocationDetails.Visibility = Visibility.Visible;
            txtLocationName.Text = _location.Name;
            txtAboutLocationName.Text = "About " + _location.Name + ":";
            txtBoxAboutLocation.Text = _location.Description;
            txtPhoneNumber.Text = _location.Phone;
            txtEmail.Text = _location.Email;
            txtAddressOne.Text = _location.Address1;
            txtAddressTwo.Text = _location.Address2;
            txtBoxPricing.Text = _location.PricingInfo;
            txtBoxReviews.Text = "";
            txtBoxReviewsSecond.Text = "";

            if (_locationReviews.Count == 0)
            {
                txtNoReviewsYet.Visibility = Visibility.Visible;
                txtBoxReviews.Visibility = Visibility.Collapsed;
                txtBoxReviewsSecond.Visibility = Visibility.Collapsed;
                btnMoreReviews.Visibility = Visibility.Collapsed;
            }
            else
            {
                try
                {
                    int avg = 0;
                    int total = 0;

                    foreach (Reviews locationReview in _locationReviews)
                    {
                        avg += locationReview.Rating;
                        total++;
                    }

                    int sum = avg / total;

                    switch (sum)
                    {
                        case 1:
                            pathStarOne.Stroke = new SolidColorBrush(Colors.Purple);
                            pathStarOne.Fill = new SolidColorBrush(Colors.Gold);
                            pathStarTwo.Stroke = new SolidColorBrush(Colors.DarkGray);
                            pathStarTwo.Fill = new SolidColorBrush(Colors.Gray);
                            pathStarThree.Stroke = new SolidColorBrush(Colors.DarkGray);
                            pathStarThree.Fill = new SolidColorBrush(Colors.Gray);
                            pathStarFour.Stroke = new SolidColorBrush(Colors.DarkGray);
                            pathStarFour.Fill = new SolidColorBrush(Colors.Gray);
                            pathStarFive.Stroke = new SolidColorBrush(Colors.DarkGray);
                            pathStarFive.Fill = new SolidColorBrush(Colors.Gray);
                            break;
                        case 2:
                            pathStarOne.Stroke = new SolidColorBrush(Colors.Purple);
                            pathStarOne.Fill = new SolidColorBrush(Colors.Gold);
                            pathStarTwo.Stroke = new SolidColorBrush(Colors.Purple);
                            pathStarTwo.Fill = new SolidColorBrush(Colors.Gold);
                            pathStarThree.Stroke = new SolidColorBrush(Colors.DarkGray);
                            pathStarThree.Fill = new SolidColorBrush(Colors.Gray);
                            pathStarFour.Stroke = new SolidColorBrush(Colors.DarkGray);
                            pathStarFour.Fill = new SolidColorBrush(Colors.Gray);
                            pathStarFive.Stroke = new SolidColorBrush(Colors.DarkGray);
                            pathStarFive.Fill = new SolidColorBrush(Colors.Gray);
                            break;
                        case 3:
                            pathStarOne.Stroke = new SolidColorBrush(Colors.Purple);
                            pathStarOne.Fill = new SolidColorBrush(Colors.Gold);
                            pathStarTwo.Stroke = new SolidColorBrush(Colors.Purple);
                            pathStarTwo.Fill = new SolidColorBrush(Colors.Gold);
                            pathStarThree.Stroke = new SolidColorBrush(Colors.Purple);
                            pathStarThree.Fill = new SolidColorBrush(Colors.Gold);
                            pathStarFour.Stroke = new SolidColorBrush(Colors.DarkGray);
                            pathStarFour.Fill = new SolidColorBrush(Colors.Gray);
                            pathStarFive.Stroke = new SolidColorBrush(Colors.DarkGray);
                            pathStarFive.Fill = new SolidColorBrush(Colors.Gray);
                            break;
                        case 4:
                            pathStarOne.Stroke = new SolidColorBrush(Colors.Purple);
                            pathStarOne.Fill = new SolidColorBrush(Colors.Gold);
                            pathStarTwo.Stroke = new SolidColorBrush(Colors.Purple);
                            pathStarTwo.Fill = new SolidColorBrush(Colors.Gold);
                            pathStarThree.Stroke = new SolidColorBrush(Colors.Purple);
                            pathStarThree.Fill = new SolidColorBrush(Colors.Gold);
                            pathStarFour.Stroke = new SolidColorBrush(Colors.Purple);
                            pathStarFour.Fill = new SolidColorBrush(Colors.Gold);
                            pathStarFive.Stroke = new SolidColorBrush(Colors.DarkGray);
                            pathStarFive.Fill = new SolidColorBrush(Colors.Gray);
                            break;
                        case 5:
                            pathStarOne.Stroke = new SolidColorBrush(Colors.Purple);
                            pathStarOne.Fill = new SolidColorBrush(Colors.Gold);
                            pathStarTwo.Stroke = new SolidColorBrush(Colors.Purple);
                            pathStarTwo.Fill = new SolidColorBrush(Colors.Gold);
                            pathStarThree.Stroke = new SolidColorBrush(Colors.Purple);
                            pathStarThree.Fill = new SolidColorBrush(Colors.Gold);
                            pathStarFour.Stroke = new SolidColorBrush(Colors.Purple);
                            pathStarFour.Fill = new SolidColorBrush(Colors.Gold);
                            pathStarFive.Stroke = new SolidColorBrush(Colors.Purple);
                            pathStarFive.Fill = new SolidColorBrush(Colors.Gold);
                            break;
                        default:
                            pathStarOne.Stroke = new SolidColorBrush(Colors.DarkGray);
                            pathStarOne.Fill = new SolidColorBrush(Colors.Gray);
                            pathStarTwo.Stroke = new SolidColorBrush(Colors.DarkGray);
                            pathStarTwo.Fill = new SolidColorBrush(Colors.Gray);
                            pathStarThree.Stroke = new SolidColorBrush(Colors.DarkGray);
                            pathStarThree.Fill = new SolidColorBrush(Colors.Gray);
                            pathStarFour.Stroke = new SolidColorBrush(Colors.DarkGray);
                            pathStarFour.Fill = new SolidColorBrush(Colors.Gray);
                            pathStarFive.Stroke = new SolidColorBrush(Colors.DarkGray);
                            pathStarFive.Fill = new SolidColorBrush(Colors.Gray);
                            txtNoReviewsYet.Visibility = Visibility.Visible;
                            break;
                    }
                }
                catch (Exception)
                {
                    return;
                }

                var fullStar = "\u2605";
                var emptyStar = "\u2606";



                switch (_locationReviews.Count)
                {
                    case 1:
                        for (int i = 0; i < 5; i++)
                        {
                            if (_locationReviews[0].Rating > i)
                            {
                                txtBoxReviews.Text += fullStar;
                            }
                            else
                            {
                                txtBoxReviews.Text += emptyStar;
                            }
                        }
                        txtBoxReviews.Text += "     " + _locationReviews[0].FullName;
                        txtBoxReviews.Text += "\n" + _locationReviews[0].Review + "\n";
                        txtBoxReviewsSecond.Visibility = Visibility.Collapsed;
                        btnMoreReviews.Visibility = Visibility.Collapsed;
                        break;
                    case 2:
                        for (int i = 0; i < 5; i++)
                        {
                            if (_locationReviews[0].Rating > i)
                            {
                                txtBoxReviews.Text += fullStar;
                            }
                            else
                            {
                                txtBoxReviews.Text += emptyStar;
                            }
                        }
                        txtBoxReviews.Text += "     " + _locationReviews[0].FullName;
                        txtBoxReviews.Text += "\n" + _locationReviews[0].Review + "\n";
                        btnMoreReviews.Visibility = Visibility.Collapsed;
                        for (int i = 0; i < 5; i++)
                        {
                            if (_locationReviews[1].Rating > i)
                            {
                                txtBoxReviewsSecond.Text += fullStar;
                            }
                            else
                            {
                                txtBoxReviewsSecond.Text += emptyStar;
                            }
                        }
                        txtBoxReviewsSecond.Text += "     " + _locationReviews[1].FullName;
                        txtBoxReviewsSecond.Text += "\n" + _locationReviews[1].Review + "\n";
                        break;
                    default:
                        for (int i = 0; i < 5; i++)
                        {
                            if (_locationReviews[0].Rating > i)
                            {
                                txtBoxReviews.Text += fullStar;
                            }
                            else
                            {
                                txtBoxReviews.Text += emptyStar;
                            }
                        }
                        txtBoxReviews.Text += "     " + _locationReviews[0].FullName;
                        txtBoxReviews.Text += "\n" + _locationReviews[0].Review + "\n";

                        for (int i = 0; i < 5; i++)
                        {
                            if (_locationReviews[1].Rating > i)
                            {
                                txtBoxReviewsSecond.Text += fullStar;
                            }
                            else
                            {
                                txtBoxReviewsSecond.Text += emptyStar;
                            }
                        }
                        txtBoxReviewsSecond.Text += "     " + _locationReviews[1].FullName;
                        txtBoxReviewsSecond.Text += "\n" + _locationReviews[1].Review + "\n";
                        break;
                }
            }


            //if (_locationImages.Count == 0)
            //{
            //    imgLocationImage.Visibility = Visibility.Collapsed;
            //    btnNext.Visibility = Visibility.Collapsed;
            //    btnBack.Visibility = Visibility.Collapsed;
            //    return;
            //}
            try
            {
                _src = new Uri(AppData.DataPath + @"\" + _locationImages[_imageNumber].ImageName, UriKind.Absolute);
                BitmapImage img = new BitmapImage(_src);
                imgLocationImage.Source = img;
            }
            catch (Exception)
            {
                btnNext.Visibility = Visibility.Collapsed;
                btnBack.Visibility = Visibility.Collapsed;
                //return;
            }
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/09
        /// 
        /// Description:
        /// The helper method that loads the specific location's schedule
        /// </summary>
        private void loadLocationSchedule()
        {

            hideDetails();
            hideEntrances();
            hideSublocations();
            btnSiteSchedule.Background = new SolidColorBrush(Colors.Gray);
            scrLocationSchedule.Visibility = Visibility.Visible;

            txtLocationNamesSchedule.Text = _location.Name + "'s Schedule";
            _eventDatesForLocation = _eventDateManager.RetrieveEventDatesByLocationID(_locationID);
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/23
        /// 
        /// Description:
        /// The helper method that loads the specific sublocation's schedule
        /// </summary>
        private void loadSublocationSchedule()
        {
            txtLocationNamesSchedule.Text = cboSchedulePicker.SelectedItem + "'s Schedule";
            _activitiesForSublocation = _activityManager.RetrieveActivitiesBySublocationID(_sublocationID);
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/23
        /// 
        /// Description:
        /// The helper method that loads the calendars data
        /// </summary>
        private void loadCalendarData()
        {
            if (!calLocationCalendar.SelectedDate.HasValue)
            {
                calLocationCalendar.SelectedDate = DateTime.Today;
            }

            if (cboSchedulePicker.SelectedItem.Equals(_location.Name))
            {
                List<EventDateVM> eventDatesForDataGrid = new List<EventDateVM>();

                foreach (EventDateVM eventDate in _eventDatesForLocation)
                {
                    if (eventDate.EventDateID == calLocationCalendar.SelectedDate)
                    {
                        eventDatesForDataGrid.Add(eventDate);
                    }
                }

                datLocationSchedule.ItemsSource = eventDatesForDataGrid;
                DateTime date = (DateTime)calLocationCalendar.SelectedDate;

                lblLocationDate.Text = date.ToString("MMMM dd, yyyy");
            }
            else
            {
                List<Activity> activitiesForDataGrid = new List<Activity>();
                foreach (Activity activity in _activitiesForSublocation)
                {
                    if (activity.EventDateID == calLocationCalendar.SelectedDate)
                    {
                        activitiesForDataGrid.Add(activity);
                    }
                }

                datLocationSchedule.ItemsSource = activitiesForDataGrid;
                DateTime date = (DateTime)calLocationCalendar.SelectedDate;

                lblLocationDate.Text = date.ToString("MMMM dd, yyyy");
            }

        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/09
        /// 
        /// Description:
        /// The helper method that hides all details when switching to a different detail page
        /// </summary>
        private void hideDetails()
        {
            scrLocationDetails.Visibility = Visibility.Collapsed;
            scrLocationSchedule.Visibility = Visibility.Collapsed;

            btnSiteDetails.Background = new SolidColorBrush(Color.FromArgb(50, 0, 0, 0));
            btnSiteSchedule.Background = new SolidColorBrush(Color.FromArgb(50, 0, 0, 0));
            btnSiteAreas.Background = new SolidColorBrush(Color.FromArgb(50, 0, 0, 0));
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/03
        /// 
        /// Description:
        /// The page loaded event handler that populates the controls on the page with the 
        /// location passed to the page
        /// </summary>
        /// <param name="e"></param>
        /// <param name="sender"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            loadLocationDetails();
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/05
        /// 
        /// Description:
        /// When the next button is clicked the next image for locations is loaded. If its at the end
        /// of the list, goes back to the beginning
        /// </summary>
        /// <param name="e"></param>
        /// <param name="sender"></param>
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            _imageNumber++;
            if (_imageNumber >= _locationImages.Count)
            {
                _imageNumber = 0;
                try
                {
                    _src = new Uri(AppData.DataPath + @"\" + _locationImages[_imageNumber].ImageName, UriKind.Absolute);
                    BitmapImage img = new BitmapImage(_src);
                    imgLocationImage.Source = img;
                }
                catch (Exception)
                {
                    return;
                }

            }
            else
            {
                try
                {
                    _src = new Uri(AppData.DataPath + @"\" + _locationImages[_imageNumber].ImageName, UriKind.Absolute);
                    BitmapImage img = new BitmapImage(_src);
                    imgLocationImage.Source = img;
                }
                catch (Exception)
                {
                    return;
                }

            }
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/05
        /// 
        /// Description:
        /// When the back button is clicked the previous image for locations is loaded. If its past the beginning
        /// of the list, goes to the end
        /// </summary>
        /// <param name="e"></param>
        /// <param name="sender"></param>
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            _imageNumber--;
            if (_imageNumber < 0)
            {
                _imageNumber = _locationImages.Count - 1;
                try
                {
                    _src = new Uri(AppData.DataPath + @"\" + _locationImages[_imageNumber].ImageName, UriKind.Absolute);
                    BitmapImage img = new BitmapImage(_src);
                    imgLocationImage.Source = img;
                }
                catch (Exception)
                {
                    return;
                }

            }
            else
            {
                try
                {
                    _src = new Uri(AppData.DataPath + @"\" + _locationImages[_imageNumber].ImageName, UriKind.Absolute);
                    BitmapImage img = new BitmapImage(_src);
                    imgLocationImage.Source = img;
                }
                catch (Exception)
                {
                    return;
                }

            }
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/09
        /// 
        /// Description:
        /// When the "Site Details" button is clicked, the location's details will populate the screen
        /// </summary>
        /// <param name="e"></param>
        /// <param name="sender"></param>
        private void btnSiteDetails_Click(object sender, RoutedEventArgs e)
        {
            loadLocationDetails();
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/09
        /// 
        /// Description:
        /// When the "Site Schedule" button is clicked, the location's schedule will populate the screen
        /// </summary>
        /// <param name="e"></param>
        /// <param name="sender"></param>
        private void btnSiteSchedule_Click(object sender, RoutedEventArgs e)
        {


            hideDetails();
            hideEntrances();
            btnSiteSchedule.Background = new SolidColorBrush(Colors.Gray);
            scrLocationSchedule.Visibility = Visibility.Visible;

            if (cboSchedulePicker.Items.Count == 0)
            {
                cboSchedulePicker.Items.Add(_location.Name);

                foreach (Sublocation sublocation in _sublocations)
                {
                    cboSchedulePicker.Items.Add(sublocation.SublocationName);
                }
            }

            // PUT CODE HERE TO CALL WHEN SELECTION CHANGES.
            if (!calLocationCalendar.SelectedDate.HasValue)
            {
                calLocationCalendar.SelectedDate = DateTime.Today;
            }
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/09
        /// 
        /// Description:
        /// When the user selects a date on the calendar, the data grid below will shows the location's
        /// schedule (events planned for that day).
        /// </summary>
        /// <param name="e"></param>
        /// <param name="sender"></param>
        private void calLocationCalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            loadCalendarData();
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/23
        /// 
        /// Description:
        /// When the user selects a location or sublocation from the combo box drop down, 
        /// the data grid below will shows the location's / sublocation's schedule (events planned for that day).
        /// </summary>
        /// <param name="e"></param>
        /// <param name="sender"></param>
        private void cboSchedulePicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cboSchedulePicker.SelectedItem.Equals(_location.Name))
                {
                    colActivityName.Visibility = Visibility.Collapsed;
                    colEventName.Visibility = Visibility.Visible;
                    loadLocationSchedule();
                    //loadCalendarData();
                }
                else
                {
                    colActivityName.Visibility = Visibility.Visible;
                    colEventName.Visibility = Visibility.Collapsed;
                    _sublocationID = _sublocations.First(m => m.SublocationName == cboSchedulePicker.SelectedItem.ToString()).SublocationID;
                    loadSublocationSchedule();
                    //loadCalendarData();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error");
            }
            //MessageBox.Show(cboSchedulePicker.SelectedItem.ToString());
            loadCalendarData();
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/24
        /// 
        /// Description:
        /// Click event handler for deactivating a location
        /// </summary>
        /// <param name="e"></param>
        /// <param name="sender"></param>
        private void btnDeleteLocation_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to delete this location?",
                               "Delete",
                               MessageBoxButton.YesNo,
                               MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    int rowsAffected = _locationManager.DeactivateLocationByLocationID(_locationID);
                    if (rowsAffected == 1) 
                    {
                        pgViewLocations viewLocations = new pgViewLocations(_locationManager, _sublocationManager);
                        this.NavigationService.Navigate(viewLocations);
                    }
                } catch (Exception ex)
                {
                    MessageBox.Show("There was an error deleting this location." + ex.Message);
                }
            }
        }

        /// <summary>
        /// Christopher Repko
        /// Created 2022/02/24
        /// 
        /// Description
        /// Event handler for the "Site Areas" button. Grabs a list of location sublocations and populates the screen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSiteAreas_Click(object sender, RoutedEventArgs e)
        {
            this.hideDetails();
            this.hideEntrances();
            btnSiteAreas.Background = new SolidColorBrush(Colors.Gray);
            try
            {
                _sublocations = _sublocationManager.RetrieveSublocationsByLocationID(_locationID);
            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n\n" + ex.InnerException.Message);
            }

            scrSublocations.Visibility = Visibility.Visible;
            scrLocationDetails.Visibility = Visibility.Collapsed;
            scrLocationSchedule.Visibility = Visibility.Collapsed;

            // redraw sublocations
            grdSublocations.RowDefinitions.Clear();
            RowDefinition row = new RowDefinition();
            row.Height = new GridLength(150);
            grdSublocations.RowDefinitions.Add(row);
            grdSublocations.RowDefinitions.Add(new RowDefinition());

            this.populateSublocations();
        }

        /// <summary>
        /// Christopher Repko
        /// Created 2022/02/24
        /// 
        /// Description:
        /// Populates the sublocations screen from a list of saved sublocations.
        /// </summary>
        private void populateSublocations()
        {
            if(_sublocations.Count > 0)
            {
                lblSublocationName.Content = _sublocations[0].SublocationName;
                txtSublocationDescription.Text = _sublocations[0].SublocationDescription;

                for(int i = 1; i < _sublocations.Count; i++)
                {
                    RowDefinition row = new RowDefinition();
                    row.Height = new GridLength(150);
                    grdSublocations.RowDefinitions.Insert(i, row);

                    Label nameLabel = new Label();
                    nameLabel.Content = _sublocations[i].SublocationName;
                    nameLabel.Margin = new Thickness(30, 10, 0, 110);
                    nameLabel.FontSize = 18.0d;
                    nameLabel.FontWeight = FontWeights.Bold;

                    TextBox descriptionText = new TextBox();
                    descriptionText.Text = _sublocations[i].SublocationDescription;
                    descriptionText.Margin = new Thickness(30, 40, 30, 10);
                    descriptionText.IsReadOnly = true;

                    Grid.SetRow(nameLabel, i); 
                    Grid.SetRow(descriptionText, i);
                    grdSublocations.Children.Add(nameLabel);
                    grdSublocations.Children.Add(descriptionText);
                }
            } else
            {
                lblSublocationName.Content = "No sublocations found.";
            }
        }
        /// <summary>
        /// Christopher Repko
        /// Created 2022/02/24
        /// 
        /// Description:
        /// Small helper method to hide sublocation screen.
        /// </summary>
        private void hideSublocations()
        {
            scrSublocations.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/03/04
        /// 
        /// Description:
        /// Helper to hide View Entrances screen
        /// </summary>
        private void hideEntrances()
        {
            scrViewEntrance.Visibility = Visibility.Collapsed;
            btnSiteEntrances.Background = new SolidColorBrush(Color.FromArgb(50, 0, 0, 0));
        }


        /// <summary>
        /// Mike Cahow
        /// Created: 2022/03/04
        /// 
        /// Description:
        /// Click event handler to send a user to the view Entrances page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSiteEntrances_Click(object sender, RoutedEventArgs e)
        {
            hideDetails();
            hideSublocations();
            btnSiteEntrances.Background = new SolidColorBrush(Colors.Gray);
            scrViewEntrance.Visibility = Visibility.Visible;
            try
            {
                this.lblLocationName.Text = _location.Name + " Entrances";
                List<Entrance> entrances = _entranceManager.RetrieveEntranceByLocationID(_locationID);
                if(entrances.Count == 0)
                {
                    lblNoEntrances.Content = "No entrances for this location yet. Use the Add button to add entrances.";
                }
                datViewEntrances.ItemsSource = entrances;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
