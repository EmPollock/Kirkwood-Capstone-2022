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
using DataAccessFakes;
using System.Text.RegularExpressions;


namespace WPFPresentation
{
    public partial class pgCreateEvent : Page
    {

        IEventManager _eventManager = null;
        IEventDateManager _eventDateManager = null;
        ILocationManager _locationManager = null;
        ISublocationManager _sublocationManager = null;
        DataObjects.Event newEvent = null;

        User _user = null;

        EventVM newEventVM = new EventVM();


        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/22
        /// 
        /// Description:
        /// Initializes component and sets up event manager with fake and default accessors
        /// 
        /// Update
        /// Vinayak Deshpande
        /// 2022/02/02
        /// Added some logic for requesting volunteers
        /// 
        /// Update
        /// Jace Pettinger
        /// 2022/02/17
        /// Added user parameter for constructor
        /// 
        /// Christopher Repko
        /// Updated: 2022/02/25
        /// 
        /// Description: Added sublocation manager
        /// </summary>
        /// <param name="sender"></param>
        public pgCreateEvent(User user, ISublocationManager sublocationManager)
        {
            // use fake accessor
            //_eventManager = new LogicLayer.EventManager(new EventAccessorFake());
            //_eventDateManager = new EventDateManager(new EventDateAccessorFake());
            // _locationManager = new LocationManager(new LocationAccessorFake());

            // use default accessor
            _eventManager = new LogicLayer.EventManager();
            _eventDateManager = new EventDateManager();
            _locationManager = new LocationManager();

            _sublocationManager = sublocationManager;
            _user = user;

            InitializeComponent();

            // Looked up how to set the calendar to not display past dates
            // https://stackoverflow.com/questions/17401488/how-to-disable-past-days-in-calender-in-wpf/45780931
            datePickerEventDate.DisplayDateStart = DateTime.Today;
            sldrNumVolunteers.Value = 25;
            

            //disable tabs that should not be viewed
            tabAddEventDate.IsEnabled = false;
            tabsetAddEventLocation.IsEnabled = false;
            tabAddEventVolunteer.IsEnabled = false;

        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/22
        /// 
        /// Description:
        /// Click event handler for creating a new event
        /// </summary>
        /// Derrick Nagy
        /// Updated: 2022/01/30
        /// 
        /// Description:
        /// Got rid of message box that was there for testing purposes and changed focus to next tab. Disabled Event tab.
        /// 
        /// Update:
        /// Vinayak Deshpande
        /// 2022/02/03
        /// Description:
        /// Changed the next button to cycle through the tabs in the set rather than just swtiching to a set tab.
        /// 
        /// Update:
        /// Vinayak Deshpande
        /// 2022/02/04
        /// Description:
        /// Moved event creation back to first tab of event creation page.
        /// 
        /// Update:
        /// Derrick Nagy
        /// 2022/02/17
        /// Description:
        /// Changed the manager method that creates the event to CreateEventReturnsEventID from CreateEvent
        private void btnEventNext_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtBoxEventName.Text == "" || txtBoxEventDescription.Text == "")
                {
                    MessageBox.Show("Please enter all fields for the event.");
                    txtBoxEventName.Focus();
                }
                else
                {
                    newEvent = new DataObjects.Event()
                    {
                        EventID = _eventManager.CreateEventReturnsEventID(txtBoxEventName.Text, txtBoxEventDescription.Text),
                        EventName = txtBoxEventName.Text,
                        EventDescription = txtBoxEventDescription.Text
                    };

                    tabAddEventDate.IsEnabled = true;
                    tabAddEventDate.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was a problem creating a new event.\n" + ex.Message);
            }
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/22
        /// 
        /// Description:
        /// Click event handler for canceling creating an event
        /// 
        /// Christopher Repko
        /// Updated: 2022/02/25
        /// 
        /// Description: Added sublocation manager to navigated page.
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEventCancel_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure?\n Unsaved changes will be discarded.",
                               "Cancel",
                               MessageBoxButton.YesNo,
                               MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                Page page = new pgViewEvents(_user, _sublocationManager);
                this.NavigationService.Navigate(page);
            }
            
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/30
        /// 
        /// Description:
        /// Text changed handler for validating time input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBoxEventStartTimeHour_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox box = (TextBox)sender;
            string textBox = box.Text;

            if (textBox.IsValidHour())
            {
                if (textBox.Length == 2)
                {
                    txtBoxEventStartTimeMinute.Focus();
                    txtBlockEventAddValidationMessage.Visibility = Visibility.Hidden;
                } 
            }
            else
            {
                box.Text = "";
                txtBlockEventAddValidationMessage.Text = "Please enter a valid time.";
                txtBlockEventAddValidationMessage.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/30
        /// 
        /// Description:
        /// Text changed handler for validating time input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBoxEventStartTimeMinute_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox box = (TextBox)sender;
            string textBox = box.Text;

            if (textBox.IsValidMinute())
            {
                if (textBox.Length == 2)
                {
                    cmbStartTimeAMPM.Focus();
                    txtBlockEventAddValidationMessage.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                box.Text = "";
                txtBlockEventAddValidationMessage.Text = "Please enter a valid time.";
                txtBlockEventAddValidationMessage.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/30
        /// 
        /// Description:
        /// Text changed handler for validating time input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBoxEventEndTimeHour_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox box = (TextBox)sender;
            string textBox = box.Text;

            if (textBox.IsValidHour())
            {
                if (textBox.Length == 2)
                {
                    txtBoxEventEndTimeMinute.Focus();
                    txtBlockEventAddValidationMessage.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                box.Text = "";
                txtBlockEventAddValidationMessage.Text = "Please enter a valid time.";
                txtBlockEventAddValidationMessage.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/30
        /// 
        /// Description:
        /// Text changed handler for validating time input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBoxEventEndTimeMinute_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox box = (TextBox)sender;
            string textBox = box.Text;

            if (textBox.IsValidMinute())
            {
                if (textBox.Length == 2)
                {
                    cmbEndTimeAMPM.Focus();
                    txtBlockEventAddValidationMessage.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                box.Text = "";
                txtBlockEventAddValidationMessage.Text = "Please enter a valid time.";
                txtBlockEventAddValidationMessage.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/31
        /// 
        /// Description:
        /// Click handler for adding a date to an event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEventDateAdd_Click(object sender, RoutedEventArgs e)
        {
            if (datePickerEventDate.SelectedDate == null)
            {
                txtBlockEventAddValidationMessage.Text = "Please enter a date.";
                txtBlockEventAddValidationMessage.Visibility = Visibility.Visible;
            }
            else if (txtBoxEventStartTimeHour.Text == "" || 
                            txtBoxEventStartTimeMinute.Text == "" || 
                            txtBoxEventEndTimeHour.Text == "" || 
                            txtBoxEventEndTimeMinute.Text == "")
            {
                txtBlockEventAddValidationMessage.Text = "Please enter times for your event to start and end.";
                txtBlockEventAddValidationMessage.Visibility = Visibility.Visible;
            }
            else
            {
                DateTime dateTimeToAdd = new DateTime();
                int year = 0;
                int month = 0;
                int day = 0;
                int secconds = 0;
                // am pm logic
                int startHour = 0;
                int startMin = 0;
                int endHour = 0;
                int endMin = 0;

                try
                {
                    dateTimeToAdd = (DateTime)datePickerEventDate.SelectedDate;
                    year = dateTimeToAdd.Year;
                    month = dateTimeToAdd.Month;
                    day = dateTimeToAdd.Day;
                    secconds = 0;
                    bool isAMHour;

                    // add 12 if the start time is in the PM
                    isAMHour = cmbStartTimeAMPM.Text == "AM";
                    startHour = Int32.Parse(txtBoxEventStartTimeHour.Text).ConvertTo24HourTime(isAMHour);
                    startMin = Int32.Parse(txtBoxEventStartTimeMinute.Text);

                    // add 12 if the end time is in the PM
                    isAMHour = cmbEndTimeAMPM.Text == "AM";
                    endHour = Int32.Parse(txtBoxEventEndTimeHour.Text).ConvertTo24HourTime(isAMHour);
                    endMin = Int32.Parse(txtBoxEventEndTimeMinute.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("There was a problem adding the date to the event.\n" + ex.Message, "Problem Adding Date", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                // check to see that one time comes after the other
                if (startHour > endHour)
                {
                    txtBoxEventEndTimeHour.Focus();
                    txtBlockEventAddValidationMessage.Text = "The end time is before the start time. Please change.";
                    txtBlockEventAddValidationMessage.Visibility = Visibility.Visible;
                }
                else if (startHour == endHour && startMin >= endMin)
                {   
                    txtBoxEventEndTimeMinute.Focus();
                    txtBlockEventAddValidationMessage.Text = "The end time is before the start time or at the same time. Please change.";
                    txtBlockEventAddValidationMessage.Visibility = Visibility.Visible;
                    
                }
                else
                {
                    try
                    {
                        EventDate eventDate = new EventDate()
                        {
                            EventDateID = dateTimeToAdd,
                            EventID = newEvent.EventID,
                            StartTime = new DateTime(year, month, day, startHour, startMin, secconds),
                            EndTime = new DateTime(year, month, day, endHour, endMin, secconds),
                            Active = true
                        };

                        //datCurrentEventDates.ItemsSource = _eventDateManager.RetrieveEventDatesByEventID(eventID);
                        newEventVM.EventDates.Add(eventDate);

                        datCurrentEventDates.ItemsSource = null;
                        datCurrentEventDates.ItemsSource = newEventVM.EventDates;
                        
                        
                        datCurrentEventDates.Visibility = Visibility.Visible;
                        txtBlkCurrentEventDates.Visibility = Visibility.Visible;

                        // prepare form to add another date
                        datePickerEventDate.SelectedDate = null;
                        datePickerEventDate.BlackoutDates.Add(new CalendarDateRange(dateTimeToAdd));
                        txtBoxEventStartTimeHour.Text = "";
                        txtBoxEventStartTimeMinute.Text = "";
                        cmbStartTimeAMPM.SelectedItem = "AM";
                        txtBoxEventEndTimeHour.Text = "";
                        txtBoxEventEndTimeMinute.Text = "";
                        cmbEndTimeAMPM.SelectedItem = "AM";

                        txtBlockEventAddValidationMessage.Visibility = Visibility.Hidden;

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("There was a problem adding the date to the event.\n" + ex.Message, "Problem Adding Date", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/17
        /// 
        /// Description:
        /// Click handler for continuing on from event dates tab
        /// 
        /// Update
        /// Derrick Nagy
        /// 2022/02/22
        /// 
        /// Desription:
        /// Added call to database to add the dates
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEventDateNext_Click(object sender, RoutedEventArgs e)
        {
            if (datCurrentEventDates.Items.Count == 0) // no dates were added, confirm want to continue
            {
                var result = MessageBox.Show("Continue without adding a date?",
                               "No Dates",
                               MessageBoxButton.YesNo,
                               MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    tabsetAddEventLocation.IsEnabled = true;
                    tabsetAddEventLocation.Focus();
                }
            }
            else { 

                // add dates
                try
                {
                    foreach (EventDate date in newEventVM.EventDates)
                    {
                        _eventDateManager.CreateEventDate(date);
                    }

                    String message = (newEventVM.EventDates.Count == 1) ? "The event date was successfully added." : "The " + newEventVM.EventDates.Count + " event dates were successfully added.";

                    MessageBox.Show(message, "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                    tabsetAddEventLocation.IsEnabled = true;
                    tabsetAddEventLocation.Focus();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("There was a problem adding the date to the event.\n" + ex.Message, "Problem Adding Date", MessageBoxButton.OK, MessageBoxImage.Error);
                }               
            }
        }

        private void chkBxNeedVolunteers_Checked(object sender, RoutedEventArgs e)
        {
            txtBlkNumVolunteers.Visibility = Visibility.Visible;
            dcPnlNumVolunteers.Visibility = Visibility.Visible;
            sldrNumVolunteers.Value = 25;
            txtBlkNumVolunteerDescription.Visibility = Visibility.Visible;
            txtBxNumVolunteerDescription.Visibility = Visibility.Visible;

        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/17
        /// 
        /// Description:
        /// Click event handler for navigating to next tab from volunteers tab
        /// 
        /// Christopher Repko
        /// Updated: 2022/02/25
        /// 
        /// Description: Added sublocation manager to navigated page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnVolunteersNext_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Event details added");
            pgViewEvents viewEventsPage = new pgViewEvents(_user, _sublocationManager);
            this.NavigationService.Navigate(viewEventsPage);
        }

        /// <summary>
        /// Logan Baccam
        /// Created: 2022/01/24
        /// 
        /// Description:
        /// Click event handler for adding a location to an event and inserting a new event
        /// 
        /// Christopher Repko
        /// Updated: 2022/02/09
        /// Removed call to create event and just set event location ID.
        /// 
        /// Jace Pettinger
        /// Updated: 2022/02/15
        /// Updated code to use variables that were assigned and never accessed.
        /// Removed creation of an event object to access a location object
        /// Made it optional to add a location
        /// Added validation
        /// 
        /// </summary>
        private void btnEventLocationAdd_Click(object sender, RoutedEventArgs e)
        {
            DataObjects.Location eventLocation = new DataObjects.Location();

            string eventName = txtBoxEventName.Text;
            string eventDescription = txtBoxEventDescription.Text;
            string locationName = txtBoxLocationName.Text; // added variable for repeated code -jp
            string locationStreet = txtBoxStreet.Text; // added variable for repeated code -jp
            string locationCity = txtBoxCity.Text; // added variable for repeated code -jp
            object locationState = cboState.SelectedItem; // added variable for repeated code -jp
            string locationZip = txtBoxZip.Text; // added variable for repeated code -jp

            if (locationName == "" && locationStreet == "" && locationCity == "" && locationState == null && locationZip == "")
            {
                //MessageBox.Show("Please enter a value for all location fields.");
                //txtBoxLocationName.Focus();
                var result = MessageBox.Show("Continue without adding a location?", // added -jp
                               "No location",
                               MessageBoxButton.YesNo,
                               MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    tabAddEventVolunteer.IsEnabled = true;
                    tabAddEventVolunteer.Focus();
                }
            }
            else if (!ValidationHelpers.IsValidName(locationName)) // added validation -jp
            {
                txtBlockLocationValidationMessage.Text = "Please enter a name for the location";
                txtBlockLocationValidationMessage.Visibility = Visibility.Visible;
                txtBoxLocationName.Focus();
            }
            else if (!ValidationHelpers.IsValidName(locationStreet)) // added validation -jp
            {
                txtBlockLocationValidationMessage.Text = "Please enter a name for the location";
                txtBlockLocationValidationMessage.Visibility = Visibility.Visible;
                txtBoxStreet.Focus();
            }
            else if (!ValidationHelpers.IsValidCityName(locationCity)) // added validation -jp
            { 
                txtBlockLocationValidationMessage.Text = "Please enter a valid city name";
                txtBlockLocationValidationMessage.Visibility = Visibility.Visible;
                txtBoxCity.Focus();
            }
            else if (locationState == null) // added validation -jp
            {
                txtBlockLocationValidationMessage.Text = "Please select a state";
                txtBlockLocationValidationMessage.Visibility = Visibility.Visible;
                cboState.Focus();
            }
            else if (!ValidationHelpers.IsValidZipCode(locationZip)) // added validation -jp
            {
                txtBlockLocationValidationMessage.Text = "Please enter a valid zip code";
                txtBlockLocationValidationMessage.Visibility = Visibility.Visible;
                txtBoxZip.Focus();
            } 
            else
            {
                txtBlockLocationValidationMessage.Visibility = Visibility.Hidden;
                try
                {

                    eventLocation = _locationManager.RetrieveLocationByNameAndAddress(locationName, locationStreet);
                    if (eventLocation is null || eventLocation.LocationID == 0)
                    {
                        _locationManager.CreateLocation(locationName, locationStreet, locationCity, ((ComboBoxItem)locationState).Tag.ToString(), locationZip);
                        eventLocation = _locationManager.RetrieveLocationByNameAndAddress(locationName, locationStreet);
                        DataObjects.EventVM eventObj = _eventManager.RetrieveEventByEventNameAndDescription(eventName/*txtBoxEventName.Text*/, eventDescription/*txtBoxEventDescription.Text*/);
                        _eventManager.UpdateEventLocationByEventID(eventObj.EventID, null, eventLocation.LocationID);
                        locationName = "";
                        locationStreet = "";
                        locationCity = "";
                        locationState = null;
                        locationZip = "";
                    }
                    else if (eventLocation != null)
                    {
                        //int? eventLocationID = null; if the event location is not null that means there is a locationID
                        int newEventLocationID = eventLocation.LocationID;
                        EventVM eventObj = _eventManager.RetrieveEventByEventNameAndDescription(eventName/*txtBoxEventName.Text*/, eventDescription /*txtBoxEventDescription.Text*/);
                        int? oldEventLocationID = null; // added

                        if (eventObj.LocationID != null)
                        {
                            oldEventLocationID = eventObj/*.Location*/.LocationID;
                        }
                        _eventManager.UpdateEventLocationByEventID(eventObj.EventID, oldEventLocationID /*eventLocationID*/, newEventLocationID /*eventLocation.LocationID*/);
                        //locationName = ""; // cannot add multiple locations, this step is not needed 
                        //locationStreet = "";
                        //locationCity = "";
                        //locationState = null;
                        //locationZip = "";
                    }

                    MessageBox.Show("Event Location Added");
                    tabAddEventVolunteer.IsEnabled = true;
                    tabAddEventVolunteer.Focus();
                    // do not allow user to go back
                    tabsetAddEventLocation.IsEnabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("There was a problem adding this location to the event.\n\n" + ex.Message + "\n\n\n" + ex.InnerException.Message);
                }
            }
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/02/17
        /// 
        /// Description:
        /// Added click event for Event date. Adds the list of event dates to the database
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEventDateNext_Helper(object sender, RoutedEventArgs e)
        {

            if (newEventVM.EventDates.Count > 0)
            {
                try
                {
                    foreach (EventDate date in newEventVM.EventDates)
                    {
                        _eventDateManager.CreateEventDate(date);
                    }

                    String message = (newEventVM.EventDates.Count == 1) ? "The event date was successfully added." : "The " + newEventVM.EventDates.Count + " event dates were successfully added.";

                    MessageBox.Show(message, "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                    Page page = new pgViewEvents(_user, _sublocationManager);
                    this.NavigationService.Navigate(page);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("There was a problem adding the date to the event.\n" + ex.Message, "Problem Adding Date", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you are finished with this tab?", "Finished?", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

                switch (result)
                {
                    case MessageBoxResult.Cancel:
                        break;
                    case MessageBoxResult.Yes:

                        newEventVM.EventDates = new List<EventDate>();
                        Page page = new pgViewEvents(_user, _sublocationManager);
                        this.NavigationService.Navigate(page);

                        break;
                    case MessageBoxResult.No:
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
