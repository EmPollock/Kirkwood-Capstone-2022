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
    }
}
