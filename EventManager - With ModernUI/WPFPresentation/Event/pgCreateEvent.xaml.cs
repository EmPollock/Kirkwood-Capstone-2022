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
        /// </summary>
        public pgCreateEvent()
        {
            // use fake accessor
            //_eventManager = new LogicLayer.EventManager(new EventAccessorFake());
            //_eventDateManager = new EventDateManager(new EventDateAccessorFake());
            // _locationManager = new LocationManager(new LocationAccessorFake());

            // use default accessor
            _eventManager = new LogicLayer.EventManager();
            _eventDateManager = new EventDateManager();
            _locationManager = new LocationManager();

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
        private void btnEventNext_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                //MessageBox.Show("Added event.");

                if (txtBoxEventName.Text == "" || txtBoxEventDescription.Text == "")
                {
                    MessageBox.Show("Please enter all fields for the event.");
                    txtBoxEventName.Focus();
                }
                else
                {
                    _eventManager.CreateEvent(txtBoxEventName.Text, txtBoxEventDescription.Text);
                    tabsetAddEventLocation.IsEnabled = true;
                    tabsetAddEventLocation.Focus();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("There was a problem creating a new event.\n" + ex.Message);
            }
        }

        /// <summary>
        /// Jace Pettiger
        /// Created: 2022/01/22
        /// 
        /// Description:
        /// Click event handler for canceling creating an event
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEventCancel_Click(object sender, RoutedEventArgs e)
        {
            Page page = new pgViewEvents();
            this.NavigationService.Navigate(page);
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
                else if (startHour == endHour)
                {
                    if (startMin >= endMin)
                    {
                        txtBoxEventEndTimeMinute.Focus();
                        txtBlockEventAddValidationMessage.Text = "The end time is before the start time. Please change.";
                        txtBlockEventAddValidationMessage.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    try
                    {
                        int eventID = _eventManager.RetrieveEventByEventNameAndDescription(txtBoxEventName.Text, txtBoxEventDescription.Text).EventID;

                        EventDate eventDate = new EventDate()
                        {
                            EventDateID = dateTimeToAdd,
                            EventID = eventID,
                            StartTime = new DateTime(year, month, day, startHour, startMin, secconds),
                            EndTime = new DateTime(year, month, day, endHour, endMin, secconds),
                            Active = true
                        };

                        _eventDateManager.CreateEventDate(eventDate);                        

                        datCurrentEventDates.ItemsSource = _eventDateManager.RetrieveEventDatesByEventID(eventID);
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

        private void chkBxNeedVolunteers_Checked(object sender, RoutedEventArgs e)
        {
            txtBlkNumVolunteers.Visibility = Visibility.Visible;
            dcPnlNumVolunteers.Visibility = Visibility.Visible;
            sldrNumVolunteers.Value = 25;
            txtBlkNumVolunteerDescription.Visibility = Visibility.Visible;
            txtBxNumVolunteerDescription.Visibility = Visibility.Visible;

        }

        /// <summary>
        /// Logan Baccam
        /// Created: 2022/01/24
        /// 
        /// Description:
        /// Click event handler for adding a location to an event and inserting a new event
        /// 
        /// *******************************************************************************
        /// Christopher Repko
        /// Updated: 2022/02/09
        /// Removed call to create event and just set event location ID.
        /// </summary>
        private void btnEventLocationAdd_Click(object sender, RoutedEventArgs e)
        {
            DataObjects.Location eventLocation = new DataObjects.Location();

            string eventName = txtBoxEventName.Text;
            string eventDescription = txtBoxEventDescription.Text;
            if (txtBoxLocationName.Text == "" || txtBoxStreet.Text == "" || txtBoxCity.Text == "" || cboState.SelectedItem == null || txtBoxZip.Text == "")
            {
                MessageBox.Show("Please enter a value for all location fields.");
                txtBoxLocationName.Focus();
            }
            else
            {
                try
                {
                    eventLocation = _locationManager.RetrieveLocationByNameAndAddress(txtBoxLocationName.Text, txtBoxStreet.Text);
                    if (eventLocation is null || eventLocation.LocationID == 0)
                    {
                        _locationManager.CreateLocation(txtBoxLocationName.Text, txtBoxStreet.Text, txtBoxCity.Text, ((ComboBoxItem)cboState.SelectedItem).Tag.ToString(), txtBoxZip.Text);
                        eventLocation = _locationManager.RetrieveLocationByNameAndAddress(txtBoxLocationName.Text, txtBoxStreet.Text);
                        DataObjects.Event eventObj = _eventManager.RetrieveEventByEventNameAndDescription(txtBoxEventName.Text, txtBoxEventDescription.Text);
                        _eventManager.UpdateEventLocationByEventID(eventObj.EventID, null, eventLocation.LocationID);
                        txtBoxLocationName.Text = "";
                        txtBoxStreet.Text = "";
                        txtBoxCity.Text = "";
                        cboState.SelectedItem = null;
                        txtBoxZip.Text = "";
                    } else if(eventLocation != null)
                    {
                        int? eventLocationID = null;
                        DataObjects.Event eventObj = _eventManager.RetrieveEventByEventNameAndDescription(txtBoxEventName.Text, txtBoxEventDescription.Text);
                        if (eventObj.Location != null)
                        {
                            eventLocationID = eventObj.Location.LocationID;
                        }
                        _eventManager.UpdateEventLocationByEventID(eventObj.EventID, eventLocationID, eventLocation.LocationID);
                        txtBoxLocationName.Text = "";
                        txtBoxStreet.Text = "";
                        txtBoxCity.Text = "";
                        cboState.SelectedItem = null;
                        txtBoxZip.Text = "";
                    }

                    MessageBox.Show("Event Location Added");
                    tabAddEventDate.IsEnabled = true;
                    tabAddEventDate.Focus();
                }
                catch (Exception ex )
                {
                    MessageBox.Show("There was a problem adding this location to the event.\n\n"  + ex.Message + "\n\n\n" + ex.InnerException.Message);
                }
            }
        }
    }
}
