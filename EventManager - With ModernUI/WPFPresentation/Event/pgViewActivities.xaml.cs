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
using DataObjects;
using LogicLayer;
using LogicLayerInterfaces;
using DataAccessInterfaces;
using DataAccessFakes;

namespace WPFPresentation.Event
{
    /// <summary>
    /// Interaction logic for pgViewActivities.xaml
    /// </summary>
    public partial class pgViewActivities : Page
    {
        IActivityManager _activityManager = null;
        DataObjects.Event _event;
        List<ActivityVM> activities;
        User _user;
        ActivityFilter activityFilter;

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/02/03
        /// 
        /// Description:
        /// Initializes component and sets up activity manager with fake or default accessors
        /// </summary>
        /// <param name="eventParam"></param>
        public pgViewActivities(DataObjects.Event eventParam)
        {
            // fake accessor
            //_activityManager = new ActivityManager(new ActivityAccessorFake(), new EventDateAccessorFake(), new SublocationAccessorFake(), new ActivityResultAccessorFake());

            _event = eventParam;
            // real accessor
            _activityManager = new ActivityManager();

            InitializeComponent();
        }
        public pgViewActivities(User user)
        {
            _activityManager = new ActivityManager();
            _user = user;

            InitializeComponent();
        }
        public pgViewActivities()
        {
            _activityManager = new ActivityManager();

            InitializeComponent();
        }

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/02/03
        /// 
        /// Description:
        /// Handler for loading activities
        /// </summary>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                setActivityFilterEnum();
                // null user gets a list of all public activities if no event is selected
                if (_event is null && _user is null)
                {
                    lblActivityEventName.Content = "All Activities";
                    activities = _activityManager.RetreiveActivitiesPastAndUpcomingDates();
                    datEventActivities.ItemsSource = activities;
                }
                // logged in user gets a list of all their activities if no event selected
                else if (_event is null && _user != null)
                {
                    lblActivityEventName.Content = "All Activities";
                    activities = _activityManager.RetreiveUserActivitiesPastAndUpcomingDates(_user.UserID);
                    datEventActivities.ItemsSource = activities;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Logan Baccam
        /// Created: 2022/02/18
        /// 
        /// Description:
        /// Handler for when changing filter from the radio buttons
        /// </summary>
        private void radActivityFilterClick(object sender, RoutedEventArgs e)
        {
            setActivityFilterEnum();
            switch (activityFilter)
            {
                case ActivityFilter.ActivityName:
                    txtSearch.Visibility = Visibility.Visible;
                    btnFind.Visibility = Visibility.Visible;
                    datePickerActivityDate.Visibility = Visibility.Hidden;
                    lblSearch.Visibility = Visibility.Visible;
                    activities = _activityManager.RetreiveActivitiesPastAndUpcomingDates();
                    datEventActivities.ItemsSource = activities;
                    txtSearch.Focus();
                    break;
                case ActivityFilter.ActivityDate:
                    lblSearch.Visibility = Visibility.Visible;
                    txtSearch.Visibility = Visibility.Hidden;
                    datePickerActivityDate.Visibility = Visibility.Visible;
                    btnFind.Visibility = Visibility.Visible;
                    activities = _activityManager.RetreiveActivitiesPastAndUpcomingDates();
                    datEventActivities.ItemsSource = activities;
                    datePickerActivityDate.Focus();
                    break;
                case ActivityFilter.ActivitySublocation:
                    lblSearch.Visibility = Visibility.Visible;
                    txtSearch.Visibility = Visibility.Visible;
                    btnFind.Visibility = Visibility.Visible;
                    datePickerActivityDate.Visibility = Visibility.Hidden;
                    activities = _activityManager.RetreiveActivitiesPastAndUpcomingDates();
                    datEventActivities.ItemsSource = activities;
                    txtSearch.Focus();
                    break;
                case ActivityFilter.AllActivities:
                    txtSearch.Visibility = Visibility.Hidden;
                    lblSearch.Visibility = Visibility.Hidden;
                    datePickerActivityDate.Visibility = Visibility.Hidden;
                    btnFind.Visibility = Visibility.Hidden;
                    activities = _activityManager.RetreiveActivitiesPastAndUpcomingDates();
                    datEventActivities.ItemsSource = activities;
                    break;
            }
        }



        /// <summary>
        /// Logan Baccam
        /// Created: 2022/02/18
        /// 
        /// Description:
        /// method for setting the activity filter enum
        /// </summary>
        public void setActivityFilterEnum()
        {
            txtSearch.Focus();
            if (radActivityDateFilter.IsChecked == true)
            {
                txtSearch.Visibility = Visibility.Hidden;
                datePickerActivityDate.Visibility = Visibility.Visible;
                activityFilter = ActivityFilter.ActivityDate;
            }
            if (radActivtyNameFilter.IsChecked == true)
            {

                activityFilter = ActivityFilter.ActivityName;
            }
            if (radActivitySublocationFilter.IsChecked == true)
            {
                activityFilter = ActivityFilter.ActivitySublocation;
            }
            if (radAllActivities.IsChecked == true)
            {
                activityFilter = ActivityFilter.AllActivities;

            }
        }
        /// <summary>
        /// Logan Baccam
        /// Created: 2022/02/18
        /// 
        /// Description:
        /// Enumerations for activity filter
        /// </summary>
        enum ActivityFilter
        {
            ActivityDate,
            ActivityName,
            ActivitySublocation,
            AllActivities
        }
        /// <summary>
        /// Logan Baccam
        /// Created: 2022/02/18
        /// 
        /// Description:
        /// Handler for returning a filtered list to the data grid
        /// </summary>
        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            List<ActivityVM> filiteredActivities = new List<ActivityVM>();

            switch (activityFilter)
            {
                case ActivityFilter.ActivityDate:
                    try
                    {
                        foreach (ActivityVM activity in activities)
                        {
                            if (datePickerActivityDate.SelectedDate.Value.CompareTo(activity.EventDateID) == 0)
                            {
                                filiteredActivities.Add(activity);
                            }
                        }
                        datEventActivities.ItemsSource = filiteredActivities;

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Please make sure the date you entered is in the format \ndd-mm-yyyy with no leading zeros.");
                        datePickerActivityDate.Focus();
                    }
                    break;
                case ActivityFilter.ActivityName:
                    try
                    {
                        foreach (ActivityVM activity in activities)
                        {
                            if (txtSearch.Text.ToString().ToLower().CompareTo(activity.ActivityName.ToLower()) == 0)
                            {
                                filiteredActivities.Add(activity);
                            }
                        }
                        datEventActivities.ItemsSource = filiteredActivities;
                        txtSearch.Text = "";

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Invalid Name.");
                        txtSearch.Focus();
                    }
                    break;

                case ActivityFilter.ActivitySublocation:
                    try
                    {
                        foreach (ActivityVM activity in activities)
                        {
                            if (txtSearch.Text.ToString().ToLower().CompareTo(activity.SublocationName.ToLower()) == 0)
                            {
                                filiteredActivities.Add(activity);
                            }
                        }
                        datEventActivities.ItemsSource = filiteredActivities;
                        txtSearch.Text = "";

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Invalid Sublocation.");
                        txtSearch.Focus();
                    }
                    break;
            }
        }
    }
}
