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

namespace WPFPresentation
{
    public partial class pgViewEvents : Page
    {
        IEventManager _eventManager = null;

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/01/23
        /// 
        /// Description:
        /// Initializes component and sets up event manager with fake and default accessors
        /// </summary>
        public pgViewEvents()
        {
            // use fake accessor
            // _eventManager = new LogicLayer.EventManager(new EventAccessorFake());

            // use default accessor
            _eventManager = new LogicLayer.EventManager();

            InitializeComponent();
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/01/23
        /// 
        /// Description:
        /// Handler for loading active events
        /// </summary>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                datActiveEvents.ItemsSource = _eventManager.RetreieveActiveEvents();
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
        /// </summary>
        private void btnCreateEvent_Click(object sender, RoutedEventArgs e)
        {
            Uri pageURI = new Uri("Event/pgCreateEvent.xaml", UriKind.Relative);
            this.NavigationService.Navigate(pageURI);
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
            DataObjects.Event selectedEvent = (DataObjects.Event)datActiveEvents.SelectedItem;
            pgEventEditDetail viewEditPage = new pgEventEditDetail(selectedEvent);
            this.NavigationService.Navigate(viewEditPage);
        }
    }
}
