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

namespace WPFPresentation.Event
{
    /// <summary>
    /// Mike Cahow
    /// Created: 2022/01/31
    /// 
    /// Description:
    /// Interaction logic for pgTaskListView.xaml
    /// </summary>
    public partial class pgTaskListView : Page
    {
        /// <summary>
        /// Mike Cahow
        /// Created: 2022/01/31
        /// 
        /// Description:
        /// Setting up and Initializing default/fake accessors
        /// 
        /// Update:
        /// Austin Timmerman
        /// Updated: 2022/02/27
        /// 
        /// Description:
        /// Added the ManagerProvider instance variable and modified page parameters
        /// </summary>

        ITaskManager _taskManager = null;
        IEventManager _eventManager = null;
        ManagerProvider _managerProvider = null;
        ISublocationManager _sublocationManager = null;
        DataObjects.EventVM _event = null;
        User _user = null;
        bool _canAddEditDelete = false;

        internal pgTaskListView(DataObjects.EventVM selectedEvent, ManagerProvider managerProvider, User user)
        {
            // fake accessor
            //_taskManager = new TaskManager(new DataAccessFakes.TaskAccessorFakes());

            // default accessor
            _managerProvider = managerProvider;
            _taskManager = managerProvider.TaskManager;
            _eventManager = managerProvider.EventManager;
            _event = selectedEvent;
            _user = user;

            try
            {
                _canAddEditDelete = _managerProvider.TaskManager.UserCanEditDeleteTask(_user.UserID);
            }
            catch (Exception ex)
            {

                MessageBox.Show("There was a problem checking to see if you are allowed to edit or delete a task." + ex.Message,
                                    "Edit/Delete Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            _sublocationManager = managerProvider.SublocationManager;

            InitializeComponent();
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/02/01
        /// 
        /// Description:
        /// Load event to populate the DataGrid. Loops through the columns to find the column
        /// with the header of "Name" and collapse all the other columns
        /// 
        /// Mike Cahow
        /// Updated: 2022/03/25
        /// 
        /// Descripiton:
        /// Added a check to see if the user is able to Add tasks or not
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            lblEventName.Text = _event.EventName;

            if (!_canAddEditDelete)
            {
                lblTaskListCreate.Visibility = Visibility.Hidden;
                btnTaskListCreate.Visibility = Visibility.Hidden;
            }


            updateTaskList();

        }

        private void updateTaskList()
        {
            try
            {
                datViewAllTasksForEvent.ItemsSource = _taskManager.RetrieveAllActiveTasksByEventID(_event.EventID);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            datViewAllTasksForEvent.RowHeaderWidth = 0;


            foreach (DataGridColumn column in datViewAllTasksForEvent.Columns)
            {
                if (column.Header.ToString() == "Name")
                {
                    column.Header = "Task Name";
                    column.Visibility = Visibility.Visible;
                }
                else if (column.Header.ToString() == "Description")
                {
                    column.Visibility = Visibility.Visible;
                }
                else if (column.Header.ToString() == "DueDate")
                {
                    column.Header = "Due By";
                    column.Visibility = Visibility.Visible;
                }
                else
                {
                    column.Visibility = Visibility.Hidden;
                }
            }

            if(datViewAllTasksForEvent.Items.Count == 0)
            {
                lblEmptyTaskList.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/02/02
        /// 
        /// Description:
        /// Add task button brings up pgTaskListCreate which will update the list upon 
        /// psTaskListCreate's closing
        /// 
        /// Christopher Repko
        /// Updated: 2022/02/25
        /// 
        /// Description: Added sublocation manager to navigated page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTaskListCreate_Click(object sender, RoutedEventArgs e)
        {
            pgTaskListCreate createTaskPage = new pgTaskListCreate(_event, _managerProvider, _user);
            this.NavigationService.Navigate(createTaskPage);
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/02/03
        /// 
        /// Description:
        /// View details of task. To be changed to Edit Task
        /// 
        /// Christopher Repko
        /// Updated: 2022/02/25
        /// 
        /// Description: Added sublocation manager to navigated page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void datViewAllTasksForEvent_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TasksVM selectedTask = (TasksVM)datViewAllTasksForEvent.SelectedItem;
            pgTaskListEdit taskEditPage = new pgTaskListEdit(selectedTask, _event, _managerProvider, _user);
            this.NavigationService.Navigate(taskEditPage);

            updateTaskList();
        }

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/03/10
        /// 
        /// Description:
        /// shows and populates list of volunteers for the selected task
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void datViewAllTasksForEvent_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TasksVM selectedTask = (TasksVM)datViewAllTasksForEvent.SelectedItem;
            lblVolunteers.Content = "Volunteers assigned to " + selectedTask.Name + ":";
            datTaskVolunteers.ItemsSource = _taskManager.RetrieveTaskAssignmentsByTaskID(selectedTask.TaskID);
        }

        // --------------------------------------------------- Vertical Buttons Click Events --------------------------------------------------------//

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/02/18
        /// 
        /// Description:
        /// Click event handler to view event details.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEventDetails_Click(object sender, RoutedEventArgs e)
        {
            pgEventEditDetail editEventPage = new pgEventEditDetail(_event, _managerProvider, _user);
            this.NavigationService.Navigate(editEventPage);
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/02/25
        /// 
        /// Description:
        /// Click event handler to take a user to the View Activities page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItinerary_Click(object sender, RoutedEventArgs e)
        {
            pgViewActivities viewActivitiesPage = new pgViewActivities(_event, _managerProvider);
            this.NavigationService.Navigate(viewActivitiesPage);
        }
        // ---------------------------------------------------- End Vertical Buttons Handlers --------------------------------------------------------//
    }
}
