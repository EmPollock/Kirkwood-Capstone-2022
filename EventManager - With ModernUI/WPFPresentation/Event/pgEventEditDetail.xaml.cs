using LogicLayer;
using LogicLayerInterfaces;
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

namespace WPFPresentation.Event
{
    /// <summary>
    /// Interaction logic for pgEventEditDetail.xaml
    /// </summary>
    public partial class pgEventEditDetail : Page
    {
        DataObjects.Event _event = null;
        List<EventDate> _eventDates = null;
        IEventManager _eventManager = null;
        IEventDateManager _eventDateManager = null;
        EventDate _selectedEventDate = null;
        IVolunteerRequestManager _volunteerRequestManager = null;

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/01
        /// 
        /// Description:
        /// Initializes component and sets up event manager with fake and default accessors
        /// </summary>
        /// <param name="selectedEvent">Must be pased an event object to view or edit</param>
        public pgEventEditDetail(DataObjects.Event selectedEvent)
        {
            // use fake accessor
            //_eventManager = new LogicLayer.EventManager(new EventAccessorFake());
            //_eventDateManager = new EventDateManager(new EventDateAccessorFake());
            _volunteerRequestManager = new VolunteerRequestManager(new VolunteerRequestAccessorFake());

            // use default accessor
            _eventManager = new LogicLayer.EventManager();
            _eventDateManager = new EventDateManager();
            //_volunteerRequestManager = new VolunteerRequestManager();
            _event = selectedEvent;

            InitializeComponent();
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/01
        /// 
        /// Description:
        /// Handler for populating page when loaded onto screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            setGeneralTabDetailMode();
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/01
        /// 
        /// Description:
        /// Helper method to handle populating page elements with data
        /// </summary>
        private void populateControls()
        {
            // populate data
            txtBoxEventName.Text = _event.EventName.ToString();
            txtBoxEventDateCreated.Text = _event.EventCreatedDate.ToShortDateString();
            txtBoxEventDescription.Text = _event.EventDescription.ToString();
            txtBoxEventLocation.Text = "Not Available";    // do not have location data available to use yet
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/01
        /// 
        /// Description:
        /// Helper method to handle changing the page to edit mode
        /// </summary>
        private void setGeneralTabEditMode()
        {
            populateControls();

            // Enable editing 
            txtBoxEventName.IsReadOnly = false;
            txtBoxEventDescription.IsReadOnly = false;
            txtBoxEventLocation.IsReadOnly = true; // cannot change until I can add location
            btnDeleteEvent.Visibility = Visibility.Visible;

            // not allowed to change date created
            txtBoxEventDateCreated.IsEnabled = false;
            txtBoxEventLocation.IsEnabled = false;

            // change buttons to Save and Cancel
            btnEventEditSave.Content = "Save";
            btnEventCloseCancel.Content = "Cancel";
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/01
        /// 
        /// Description:
        /// Helper method to handle changing the page to detail mode
        /// </summary>
        private void setGeneralTabDetailMode()
        {
            populateControls();

            // make read only
            txtBoxEventName.IsReadOnly = true;
            txtBoxEventDateCreated.IsReadOnly = true;
            txtBoxEventDescription.IsReadOnly = true;
            txtBoxEventLocation.IsReadOnly = true;
            btnDeleteEvent.Visibility = Visibility.Hidden;

            // make enabled to look nicer
            txtBoxEventDateCreated.IsEnabled = true;
            txtBoxEventLocation.IsEnabled = true;

            // make sure buttons are Edit and Close
            btnEventEditSave.Content = "Edit";
            btnEventCloseCancel.Content = "Close";
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/01
        /// 
        /// Description:
        /// Button click event handler for eduting or saving the event
        /// </summary>
        private void btnEventEditSave_Click(object sender, RoutedEventArgs e)
        {
            if (btnEventEditSave.Content.ToString() == "Edit") // Edit record
            {
                setGeneralTabEditMode();
            }
            else // save record
            {
                if (txtBoxEventName.Text == "") // check no name
                {
                    MessageBox.Show("Please enter an event name");
                }
                else if (txtBoxEventDescription.Text == "") // check no description
                {
                    MessageBox.Show("Please enter an event description");
                }
                else // create new event object
                {
                    DataObjects.Event newEvent = new DataObjects.Event()
                    {
                        EventID = _event.EventID,
                        EventName = txtBoxEventName.Text,
                        EventCreatedDate = _event.EventCreatedDate,
                        EventDescription = txtBoxEventDescription.Text,
                        Active = true
                    };

                    try // try update
                    {
                        if (_eventManager.UpdateEvent(_event, newEvent)) // one event updated
                        {
                            MessageBox.Show("Event updated.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("There was a problem editing the event.\n" + ex.Message);

                    }
                    finally // return to view events page
                    {
                        Uri pageURI = new Uri("Event/pgViewEvents.xaml", UriKind.Relative);
                        this.NavigationService.Navigate(pageURI);
                    }
                }
            }
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/01
        /// 
        /// Description:
        /// Button click event handler for navigating back to previous page or canceling edit
        /// </summary>
        private void btnEventCloseCancel_Click(object sender, RoutedEventArgs e)
        {
            if (btnEventCloseCancel.Content.ToString() == "Close") // return to view events page
            {
                this.NavigationService.GoBack();
            }
            else // Make sure want to close, then return to details view
            {
                var result = MessageBox.Show("Discard changes?",
                               "Canel",
                               MessageBoxButton.YesNo,
                               MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    setGeneralTabDetailMode();
                }
            }

        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/01
        /// 
        /// Description:
        /// Button click event handler for navigating to edit event dates tab
        /// </summary>
        private void btnEditEventDates_Click(object sender, RoutedEventArgs e)
        {
            tabEventDates.Focus();
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/01
        /// 
        /// Description:
        /// Handler for validating Event Name input
        /// </summary>
        private void txtBoxEventName_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox box = (TextBox)sender;
            string textBox = box.Text;
            if (textBox == "")
            {
                box.Text = "";
                txtBlockValidationMessage.Text = "Please enter a valid event name.";
                txtBlockValidationMessage.Visibility = Visibility.Visible;
                txtBoxEventName.Focus();
            }
            else
            {
                txtBlockValidationMessage.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/01
        /// 
        /// Description:
        /// Handler for validating Event Description input
        /// </summary>
        private void txtBoxEventDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox box = (TextBox)sender;
            string textBox = box.Text;
            if (textBox == "")
            {
                box.Text = "";
                txtBlockValidationMessage.Text = "Please enter a valid event description.";
                txtBlockValidationMessage.Visibility = Visibility.Visible;
            }
            else
            {
                txtBlockValidationMessage.Visibility = Visibility.Hidden;
            }
        }


        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/01
        /// 
        /// Description:
        /// Button click handler for deleting an event
        /// </summary>
        private void btnDeleteEvent_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure?\nThis action cannot be undone",
                               "Delete Event.",
                               MessageBoxButton.YesNo,
                               MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try // try update
                {
                    DataObjects.Event newEvent = new DataObjects.Event()
                    { // same Event with event set to false
                        EventID = _event.EventID,
                        EventName = _event.EventName,
                        EventDescription = _event.EventDescription,
                        EventCreatedDate = _event.EventCreatedDate,
                        Active = false
                    };
                    if (_eventManager.UpdateEvent(_event, newEvent)) // one event updated
                    {
                        MessageBox.Show("Event Deleted.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("There was a problem deleting the event.\n" + ex.Message);

                }
                finally // return to view events page
                {
                    this.NavigationService.GoBack();
                }
            }
        }

        private void btnTasks_Click(object sender, RoutedEventArgs e)
        {
            pgTaskListView taskViewPage = new pgTaskListView(_event);
            this.NavigationService.Navigate(taskViewPage);
        }

        // --------------------------------------------------------- End of General Tab -----------------------------------------------------------
        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/08
        /// 
        /// Description:
        /// Helper method for setting Event Dates tab to detail mode
        /// </summary>
        private void setEventDatesTabDetailMode()
        {
            grdAddEventDate.Visibility = Visibility.Hidden; // hide edit dates area
            btnEditEventDateAddSave.Content = "Add Dates";
            btnEditEventDateCloseCancel.Content = "Close";

            try // try to populate event dates data grid
            {
                _eventDates = _eventDateManager.RetrieveEventDatesByEventID(_event.EventID);
            }
            catch (Exception)
            {
                // not actually throwing anything, just putting error message on screen
            }
            if (_eventDates == null)
            {
                txtBlockEventDateValidationMessage.Text = "No dates added";
                txtBlockEventDateValidationMessage.Visibility = Visibility.Visible;
            }
            else
            {
                datEditCurrentEventDates.ItemsSource = _eventDates;
            }
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/08
        /// 
        /// Description:
        /// Helper method for setting Event Dates tab to edit mode
        /// </summary>
        private void setEventDateTabEditMode()
        {
            grdAddEventDate.Visibility = Visibility.Visible;
            datePickerEventDate.SelectedDate = null;
            txtBoxEventStartTimeHour.Text = "";
            txtBoxEventStartTimeMinute.Text = "";
            cmbStartTimeAMPM.SelectedItem = "AM";
            txtBoxEventEndTimeHour.Text = "";
            txtBoxEventEndTimeMinute.Text = "";
            cmbEndTimeAMPM.SelectedItem = "AM";
            txtBlockEventAddValidationMessage.Visibility = Visibility.Hidden;

            btnEditEventDateAddSave.Content = "Add";
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/08
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
        /// Jace Pettinger
        /// Created: 2022/02/08
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
        /// Jace Pettinger
        /// Created: 2022/02/08
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
        /// Jace Pettinger
        /// Created: 2022/02/08
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
        /// Jace Pettinger
        /// Created: 2022/02/08
        /// 
        /// Description:
        /// Click handler for either starting edit mode, adding a date to an event, or updating an en event date
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditEventDateAddSave_Click(object sender, RoutedEventArgs e)
        {
            if (btnEditEventDateAddSave.Content.Equals("Add Dates")) // show add dates grid to add a new date
            {
                setEventDateTabEditMode();
            }
            else // there should be data in grid to either add or update a record
            {
                // validate input first
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
                        MessageBox.Show("There was a problem converting the date.\n" + ex.Message, "Problem Converting Date", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    // check to see that one time comes after the other
                    if (startHour > endHour)
                    {
                        txtBoxEventEndTimeHour.Text = "";
                        txtBoxEventEndTimeMinute.Text = "";
                        txtBoxEventEndTimeHour.Focus();
                        txtBlockEventAddValidationMessage.Text = "The end time is before the start time. Please change.";
                        txtBlockEventAddValidationMessage.Visibility = Visibility.Visible;
                    }
                    else if (startHour == endHour)
                    {
                        if (startMin >= endMin)
                        {
                            txtBoxEventEndTimeMinute.Text = "";
                            txtBoxEventEndTimeMinute.Focus();
                            txtBlockEventAddValidationMessage.Text = "The end time is before the start time. Please change.";
                            txtBlockEventAddValidationMessage.Visibility = Visibility.Visible;
                        }
                    }

                    // end of initial validation checks 

                    EventDate newEventDate = new EventDate() // create event date object
                    {
                        EventDateID = dateTimeToAdd,
                        EventID = _event.EventID,
                        StartTime = new DateTime(year, month, day, startHour, startMin, secconds),
                        EndTime = new DateTime(year, month, day, endHour, endMin, secconds),
                        Active = true
                    };

                    if (btnEditEventDateAddSave.Content.Equals("Add")) // add a new event date
                    {

                        try
                        {
                            _eventDateManager.CreateEventDate(newEventDate);

                            datEditCurrentEventDates.ItemsSource = _eventDateManager.RetrieveEventDatesByEventID(_event.EventID);

                            // prepare form to add another date
                            setEventDateTabEditMode();


                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("There was a problem adding the date to the event.\n" + ex.Message, "Problem Adding Date", MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                    }
                    else // update event date
                    {
                        try
                        {
                            _eventDateManager.UpdateEventDate(_selectedEventDate, newEventDate);
                            setEventDatesTabDetailMode();
                        }
                        catch (Exception ex)
                        {

                          MessageBox.Show("There was a problem updating the date.\n" + ex.Message, "Problem Updating Date", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/06
        /// 
        /// Description:
        /// Button click handler for close / cancel button in event dates tab
        /// </summary>
        private void btnEditEventDateCloseCancel_Click(object sender, RoutedEventArgs e)
        {
            if (btnEditEventDateCloseCancel.Content.ToString() == "Close") // return to view general tab
            {
                tabGeneralEvent.Focus();
            }
            else // Make sure want to close, then return to details view
            {
                var result = MessageBox.Show("Discard unsaved changes?",
                               "Canel",
                               MessageBoxButton.YesNo,
                               MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    setEventDatesTabDetailMode();
                }
            }
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/06
        /// 
        /// Description:
        /// Handler for loading tab when selected
        /// </summary>
        private void tabEventDates_Loaded(object sender, RoutedEventArgs e)
        {
            setEventDatesTabDetailMode();
        }

        private void btnEditEventDatesDatGrid_Click(object sender, RoutedEventArgs e)
        {
            // make sure the add/edit dates grid is visable
            grdAddEventDate.Visibility = Visibility.Visible;

            // get event date object and get its parts
            _selectedEventDate = (EventDate)datEditCurrentEventDates.SelectedItem;
            DateTime selectedDate = (DateTime)_selectedEventDate.EventDateID;
            DateTime startTime = (DateTime)_selectedEventDate.StartTime;
            DateTime endTime = (DateTime)_selectedEventDate.EndTime;
            int startHour = startTime.Hour;
            int startMin = startTime.Minute;
            int endHour = endTime.Hour;
            int endMin = endTime.Minute;
            int startTimeAMPM = 0; // 0 is the index for AM 1 is PM
            int endTimeAMPM = 0;

            // subtract 12 if the start time is in the PM
            if (startHour > 12)
            {
                startHour -= 12;
                startTimeAMPM = 1;
            }
            // subtract 12 if the end time is in the PM
            if (endHour > 12)
            {
                endHour -= 12;
                endTimeAMPM = 1;
            }
            //12 am is 00:00 on 24hr clock
            if (startHour == 0) 
            {
                startHour = 12;
            }
            
            // set the content with the data
            datePickerEventDate.SelectedDate = selectedDate;
            txtBoxEventStartTimeHour.Text = startHour.ToString(); 
            txtBoxEventStartTimeMinute.Text = startMin.ToString("D2"); // "D2" will give number formatting of 00
            txtBoxEventEndTimeHour.Text = endHour.ToString();
            txtBoxEventEndTimeMinute.Text = endMin.ToString("D2");
            cmbStartTimeAMPM.SelectedIndex = startTimeAMPM;
            cmbEndTimeAMPM.SelectedIndex = endTimeAMPM;

            // change buttons
            btnEditEventDateAddSave.Content = "Update";
            btnEditEventDateCloseCancel.Content = "Cancel";

            //focus on first item to set
            datePickerEventDate.Focus();
        }

        /// <summary>
        /// Vinayak Deshpande
        /// 2022/02/16
        /// Logic for viewing requests for currently selected event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabEventVolunteerRequests_Loaded(object sender, RoutedEventArgs e)
        {
            setEventVolunteerRequestTabList();
        }

        private void setEventVolunteerRequestTabList()
        {
            int eventID = _event.EventID;

            try
            {
                List<VolunteerRequest> _requests = _volunteerRequestManager.GetVolunteerRequests(eventID);
                dgRequestList.ItemsSource = _requests;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Vinayak Deshpande
        /// 2022/02/16
        /// To be added logic for accepting and rejecting requests.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAcceptRequest_Click(object sender, RoutedEventArgs e)
        {
            VolunteerRequest currRequest = (VolunteerRequest)dgRequestList.SelectedItem;
            // logic required to accept DNE

        }

        private void btnRejectRequest_Click(object sender, RoutedEventArgs e)
        {
            VolunteerRequest currRequest = (VolunteerRequest)dgRequestList.SelectedItem;
            // Logic required to reject DNE
        }
    }
}
