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

            // use default accessor
            _eventManager = new LogicLayer.EventManager();
            _eventDateManager = new EventDateManager();

            _event = selectedEvent;
            try
            {
                _eventDates = _eventDateManager.RetrieveEventDatesByEventID(_event.EventID);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

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
            setDetailMode();
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
            chkActive.IsChecked = _event.Active;

            datEventDates.ItemsSource = _eventDates;

        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/01
        /// 
        /// Description:
        /// Helper method to handle changing the page to edit mode
        /// </summary>
        private void setEditMode()
        {
            populateControls();

            // Enable editing 
            txtBoxEventName.IsReadOnly = false;
            txtBoxEventDescription.IsReadOnly = false;
            txtBoxEventLocation.IsReadOnly = true; // cannot change until I can add location
            chkActive.IsEnabled = true;

            // not allowed to change date created
            txtBoxEventDateCreated.IsEnabled = false;
            txtBoxEventLocation.IsEnabled = false;

            // show edit event dates button
            btnEditEventDates.Visibility = Visibility.Visible;

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
        private void setDetailMode()
        {
            populateControls();

            // make read only
            txtBoxEventName.IsReadOnly = true;
            txtBoxEventDateCreated.IsReadOnly = true;
            txtBoxEventDescription.IsReadOnly = true;
            txtBoxEventLocation.IsReadOnly = true;
            chkActive.IsEnabled = false;

            // Hide edit dates button
            btnEditEventDates.Visibility = Visibility.Hidden;

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
                setEditMode();
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
                        Active = (bool)chkActive.IsChecked
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
                Uri pageURI = new Uri("Event/pgViewEvents.xaml", UriKind.Relative);
                this.NavigationService.Navigate(pageURI);
            }
            else // return to details view
            {
                setDetailMode();
            }

        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/01
        /// 
        /// Description:
        /// Button click event handler for navigating to edit event dates page
        /// </summary>
        private void btnEditEventDates_Click(object sender, RoutedEventArgs e)
        {
            // not implemented yet
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

        private void btnTasks_Click(object sender, RoutedEventArgs e)
        {
            pgTaskListView taskViewPage = new pgTaskListView(_event);
            this.NavigationService.Navigate(taskViewPage);
        }
    }
}
