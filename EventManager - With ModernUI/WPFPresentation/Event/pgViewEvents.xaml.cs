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
using WPFPresentation.Event;
using System.Collections.ObjectModel;

namespace WPFPresentation
{
    public partial class pgViewEvents : Page
    {
        private IEventManager _eventManager = null;
        private ISublocationManager _sublocationManager = null;
        private ManagerProvider _managerProvider = null;
        private List<DataObjects.EventVM> _eventList = null;
        private List<DataObjects.Event> _searchResults = null;
        private DataObjects.EventVM selectedEvent = null;
        
        private User _user = null;
        private EventFilter eventFilter = EventFilter.AllUpcomingEvents;

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/01/23
        /// 
        /// Description:
        /// Initializes component and sets up event manager with fake and default accessors
        /// 
        /// Christopher Repko
        /// Updated: 2022/02/25
        /// 
        /// Description: Added sublocation manager
        /// 
        /// Update:
        /// Austin Timmerman
        /// Updated: 2022/02/27
        /// 
        /// Description:
        /// Added the ManagerProvider instance variable and modified page parameters
        /// </summary>
        /// <param name="managerProvider"></param>
        internal pgViewEvents(ManagerProvider managerProvider)
        {
            _managerProvider = managerProvider;
            _eventManager = managerProvider.EventManager;

            _sublocationManager = managerProvider.SublocationManager;

            InitializeComponent();

            // hide if no user
            sepEventFilter.Visibility = Visibility.Collapsed;
            cmbItemsChooseAllOrUserEvents.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/02/08
        /// 
        /// Description:
        /// Overloaded constructor for pgViewEvents that takes a user object
        /// 
        /// Christopher Repko
        /// Updated: 2022/02/25
        /// 
        /// Description: Added sublocation manager
        /// 
        /// Update:
        /// Austin Timmerman
        /// Updated: 2022/02/27
        /// 
        /// Description:
        /// Added the ManagerProvider instance variable and modified page parameters
        /// </summary>
        /// <param name="user"></param>
        /// <param name="managerProvider"></param>
        internal pgViewEvents(User user, ManagerProvider managerProvider)
        {
            _managerProvider = managerProvider;
            _eventManager = managerProvider.EventManager;
            _sublocationManager = managerProvider.SublocationManager;

            InitializeComponent();

            _user = user;


            // hide if no user
            if (_user == null)
            {
                sepEventFilter.Visibility = Visibility.Collapsed;
                cmbItemsChooseAllOrUserEvents.Visibility = Visibility.Collapsed;
            }

        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/01/23
        /// 
        /// Description:
        /// Handler for loading active events
        /// </summary>
        /// Derrick Nagy
        /// Updated: 2022/02/06
        /// 
        /// Description:
        ///  Had the _eventManager.RetreieveActiveEvents() add a list of events into the field _eventList
        ///  Changed what the data grid "datActiveEvents" had for an items source to the view model
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            try
            {
                // Update 2022/02/05
                //datActiveEvents.ItemsSource = _eventManager.RetreieveActiveEvents();
                //_eventList = _eventManager.RetreieveActiveEvents();

                if (_user != null)
                {
                    eventFilter = EventFilter.UserUpcomingEvents;
                    _eventList = _eventManager.RetrieveEventListForPastAndUpcomingDatesForUser(_user.UserID);
                }
                else
                {                    
                    _eventList = _eventManager.RetrieveEventListForUpcomingDates();
                    
                }
                

                datActiveEvents.ItemsSource = _eventList;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/01
        /// 
        /// Description:
        /// Button click event handler for navigating to create events page
        /// 
        /// Christopher Repko
        /// Updated: 2022/02/25
        /// 
        /// Description: Added sublocation manager to navigated page.
        /// </summary>
        private void btnCreateEvent_Click(object sender, RoutedEventArgs e)
        {
            if (_user == null)
            {
                MessageBox.Show("Please log in to create an event");
            } else
            {
                pgCreateEvent createEventPage = new pgCreateEvent(_user, _managerProvider);
                this.NavigationService.Navigate(createEventPage);
            }
           
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/01
        /// 
        /// Description:
        /// click event handler for navigating to Edit/Detail page for events
        /// 
        /// solution for how to pass an event object to a new page found at 
        /// https://social.msdn.microsoft.com/Forums/vstudio/en-US/f1ea74ce-fd91-46c6-83bd-90cba1528b04/how-to-get-values-from-one-wpf-page-to-another-wpf-page?forum=wpf
        /// </summary>
        private void datActiveEvents_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataObjects.EventVM selectedEvent = (DataObjects.EventVM)datActiveEvents.SelectedItem;
            pgEventEditDetail viewEditPage = new pgEventEditDetail(selectedEvent, _managerProvider, _user);
            this.NavigationService.Navigate(viewEditPage);
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/02/05
        /// 
        /// Description:
        /// Control the text context for the drop down box that filters events
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbSearchEvents_DropDownClosed(object sender, EventArgs e)
        {
            cmbSearchEvents.Text = "Event Filter";
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/02/05
        /// 
        /// Description:
        /// Text changed event for searching the current list of events for key words
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBoxEventSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtBoxEventSearch.Text == "")
            {
                datActiveEvents.ItemsSource = _eventList;
            }
            else
            {
                _searchResults = new List<DataObjects.Event>();

                foreach (DataObjects.Event myEvent in _eventList)
                {
                    if (myEvent.EventName.ToLower().Contains(txtBoxEventSearch.Text.ToLower()) 
                        || myEvent.EventDescription.ToLower().Contains(txtBoxEventSearch.Text.ToLower()))
                    {
                        _searchResults.Add(myEvent);
                    }
                }
                datActiveEvents.ItemsSource = _searchResults;
            }
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/02/09
        /// 
        /// Description:
        /// Click event for filtering the current list of events
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radUpcomingEvents_Click(object sender, RoutedEventArgs e)
        {
            setEventFilterEnum();

            try
            {
                switch (eventFilter)
                {
                    case EventFilter.AllUpcomingEvents:
                        _eventList = _eventManager.RetrieveEventListForUpcomingDates();
                        datActiveEvents.ItemsSource = _eventList;
                        break;

                    case EventFilter.AllPastEvents:
                        _eventList = _eventManager.RetrieveEventListForPastDates();
                        datActiveEvents.ItemsSource = _eventList;
                        break;

                    case EventFilter.AllUpcomingAndPastEvents:
                        _eventList = _eventManager.RetrieveEventListForUpcomingAndPastDates();
                        datActiveEvents.ItemsSource = _eventList;
                        break;

                    case EventFilter.UserUpcomingEvents:
                        _eventList = _eventManager.RetrieveEventListForUpcomingDatesForUser(_user.UserID);
                        datActiveEvents.ItemsSource = _eventList;
                        break;

                    case EventFilter.UserPastEvents:
                        _eventList = _eventManager.RetrieveEventListForPastDatesForUser(_user.UserID);
                        datActiveEvents.ItemsSource = _eventList;
                        break;

                    case EventFilter.UserUpcomingandPastEvents:
                        _eventList = _eventManager.RetrieveEventListForPastAndUpcomingDatesForUser(_user.UserID);
                        datActiveEvents.ItemsSource = _eventList;
                        break;

                    default:
                        break;
                }
                txtBoxEventSearch.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }


        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/02/09
        /// 
        /// Description:
        /// Private helper method that sets the enum based on the currently selected radio buttons for filtering an event.
        /// 
        /// </summary>
        private void setEventFilterEnum()
        {
            if (radAllEvents.IsChecked == true && radUpcomingEvents.IsChecked == true)
            {
                eventFilter = EventFilter.AllUpcomingEvents;
            }
            else if (radAllEvents.IsChecked == true && radPastEvents.IsChecked == true)
            {
                eventFilter = EventFilter.AllPastEvents;
            }
            else if (radAllEvents.IsChecked == true && radPastAndUpcomingEvents.IsChecked == true)
            {
                eventFilter = EventFilter.AllUpcomingAndPastEvents;
            }
            else if (radUserEvents.IsChecked == true && radUpcomingEvents.IsChecked == true)
            {
                eventFilter = EventFilter.UserUpcomingEvents;
            }
            else if (radUserEvents.IsChecked == true && radPastEvents.IsChecked == true)
            {
                eventFilter = EventFilter.UserPastEvents;
            }
            else if (radUserEvents.IsChecked == true && radPastAndUpcomingEvents.IsChecked == true)
            {
                eventFilter = EventFilter.UserUpcomingandPastEvents;
            }
            else
            {
                radAllEvents.IsChecked = true;
                radUpcomingEvents.IsChecked = true;
                eventFilter = EventFilter.AllUpcomingEvents;
            }
        }
        /// <summary>
        /// Logan Baccam
        /// Created: 2022/02/18
        /// 
        /// Description:
        /// handler for navigating to the view activities page
        /// 
        /// </summary>
        private void btnActivity_Click(object sender, RoutedEventArgs e)
        {
            selectedEvent = (DataObjects.EventVM)datActiveEvents.SelectedItem;
            Event.pgViewActivities page;
            Uri pageURI = new Uri("Event/pgViewActivities.xaml", UriKind.Relative);

            if (_user != null)
            {
                if (selectedEvent is null)
                {
                    page = new Event.pgViewActivities(_user, _managerProvider);
                    this.NavigationService.Navigate(page, pageURI);
                }
                else
                {
                    page = new Event.pgViewActivities(selectedEvent, _managerProvider);
                    this.NavigationService.Navigate(page, pageURI);
                }
            }
            else
            {
                if (selectedEvent is null)
                {
                    page = new Event.pgViewActivities(_managerProvider);
                    this.NavigationService.Navigate(page, pageURI);
                }
                else 
                {
                    page = new Event.pgViewActivities(selectedEvent, _managerProvider);
                    this.NavigationService.Navigate(page, pageURI);
                }
            }
        }
    }

    /// <summary>
    /// Derrick Nagy
    /// Created: 2022/02/09
    /// 
    /// Description:
    /// Enum for filtering the current list of events
    /// 
    /// </summary>
    enum EventFilter
    {
        AllUpcomingEvents,
        AllPastEvents,
        AllUpcomingAndPastEvents,
        UserUpcomingEvents,
        UserPastEvents,
        UserUpcomingandPastEvents
    }
}
