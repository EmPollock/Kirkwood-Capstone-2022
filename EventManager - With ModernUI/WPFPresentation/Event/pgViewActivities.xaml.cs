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
        DataObjects.EventVM _event;

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/02/03
        /// 
        /// Description:
        /// Initializes component and sets up activity manager with fake or default accessors
        /// </summary>
        /// <param name="eventParam"></param>
        public pgViewActivities(DataObjects.EventVM eventParam)
        {
            // fake accessor
            //_activityManager = new ActivityManager(new ActivityAccessorFake(), new EventDateAccessorFake(), new SublocationAccessorFake(), new ActivityResultAccessorFake());

            _event = eventParam;
            // real accessor
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
                lblActivityEventName.Content = _event.EventName + " Activities";
                datEventActivities.ItemsSource = _activityManager.RetrieveActivitiesByEventID(_event.EventID);                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// Mike Cahow
        /// Created: 2022/02/21
        /// 
        /// Description:
        /// Click event handler to launch pgViewActivityDetails.xaml
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void datEventActivities_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ActivityVM selectedActivity = (ActivityVM)datEventActivities.SelectedItem;
            pgViewActivityDetails activityDetailsPage = new pgViewActivityDetails(selectedActivity, _event);
            this.NavigationService.Navigate(activityDetailsPage);
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/02/24
        /// 
        /// Description:
        /// Click event handler to send a user back to Event Edit Details page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEventDetails_Click(object sender, RoutedEventArgs e)
        {
            pgEventEditDetail pgEventEditDetail = new pgEventEditDetail(_event);
            this.NavigationService.Navigate(pgEventEditDetail);
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/02/24
        /// 
        /// Description:
        /// Click event handler to send a user to the Task list View page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTasks_Click(object sender, RoutedEventArgs e)
        {
            pgTaskListView ViewTaskListPage = new pgTaskListView(_event);
            this.NavigationService.Navigate(ViewTaskListPage);
        }
    }
}
