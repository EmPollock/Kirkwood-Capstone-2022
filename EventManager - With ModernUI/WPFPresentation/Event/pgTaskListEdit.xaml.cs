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
    /// Interaction logic for pgTaskListEdit.xaml
    /// </summary>
    public partial class pgTaskListEdit : Page
    {
        DataObjects.TasksVM _task = null;
        DataObjects.EventVM _event = null;
        DataObjects.VolunteerNeed _need = null;
        ITaskManager _taskManager = null;
        IEventManager _eventManager = null;
        ISublocationManager _sublocationManager = null;
        IVolunteerNeedManager _needManager = null;
        ManagerProvider _managerProvider = null;

        User _user = null;

        // create a priority list to populate in a bit
        List<Priority> _priorities = new List<Priority>();

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/02/08
        /// 
        /// Description:
        /// Initializes components and sets up task/event managers as well
        /// as the selectedTask object along with a list of priorities
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
        /// <param name="selectedTask"></param>
        /// <param name="selectedEvent"></param>
        /// <param name="managerProvider"></param>
        /// <param name="user"></param>
        internal pgTaskListEdit(DataObjects.TasksVM selectedTask, DataObjects.EventVM selectedEvent, 
            ManagerProvider managerProvider, User user)
        {
            _taskManager = managerProvider.TaskManager;
            _eventManager = managerProvider.EventManager;
            _managerProvider = managerProvider;
            _needManager = managerProvider.NeedManager;

            _task = selectedTask;
            _event = selectedEvent;
            _sublocationManager = managerProvider.SublocationManager;
            _task.TaskEventName = _event.EventName;
            _task.EventID = _event.EventID;
            _user = user;
            _need = _needManager.RetrieveVolunteerNeedByTaskID(_task.TaskID);


            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            populateControls();
            try
            {
                _priorities = _taskManager.RetrieveAllPriorities();
                cboPriority.ItemsSource = from p in _priorities
                                          orderby p.PriorityID
                                          select p.Description;
            }
            catch (Exception ex)
            {

                MessageBox.Show("There was a problem retrieving the Priority list." + "\n\n" + ex.Message);
            }
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/02/08
        /// 
        /// Description:
        /// Void method to populate the edit page with the selected Tasks
        /// values. To be called when needed.
        /// 
        /// Derrick Nagy
        /// Upadate: 2022/03/27
        /// 
        /// Description:
        /// Set default date picker time to DateTime.Now if the date was not previously choosen
        /// 
        /// </summary>
        private void populateControls()
        {
            txtBlkEventName.Text = _task.TaskEventName; // pass up event name from view
            txtTaskName.Text = _task.Name.ToString();
            txtTaskName.IsReadOnly = true;
            txtTaskName.IsEnabled = false;
            txtTaskDescription.Text = _task.Description.ToString();
            cboAssign.SelectedItem = "Unavailable"; // pass up volunteer when available

            if (_task.DueDate == DateTime.MinValue)
            {
                dtpTaskDueDate.SelectedDate = DateTime.Now;
            }
            else
            {
                dtpTaskDueDate.SelectedDate = _task.DueDate;
            }
            


            cboPriority.SelectedItem = _task.TaskPriority;
            sldrNumVolunteers.Value = _need.NumTotalVolunteers;
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/02/08
        /// 
        /// Description:
        /// Checks if the user wants to leave the page without saving update
        /// 
        /// Christopher Repko
        /// Updated: 2022/02/25
        /// 
        /// Description: Added sublocation manager to navigated page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelTask_Click(object sender, RoutedEventArgs e)
        {
            string message = "Task will not be saved if you stop now.";
            string title = "Stop creating Task?";
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxImage image = MessageBoxImage.Warning;
            var result = MessageBox.Show(message, title, buttons, image);

            if (result == MessageBoxResult.No)
            {
                return;
            }
            else
            {
                pgTaskListView viewTasksPage = new pgTaskListView(_event, _managerProvider, _user);
                this.NavigationService.Navigate(viewTasksPage);
            }
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/02/08
        /// 
        /// Description:
        /// Click event handler to save the updated task
        /// 
        /// Christopher Repko
        /// Updated: 2022/02/25
        /// 
        /// Description: Added sublocation manager to navigated page.
        /// 
        /// Vinayak Deshpande
        /// Updated: 2022/03/05
        /// 
        /// Description: Added logic to handle requesting a certain number of volunteers
        /// 
        /// Derrick Nagy
        /// Upadate: 2022/03/27
        /// 
        /// Description:
        /// Handled errors that occur for an unsuccessful update
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveTask_Click(object sender, RoutedEventArgs e)
        {
            if(txtTaskDescription.Text == "" || txtTaskDescription.Text == null)
            {
                MessageBox.Show("Please enter a description field.");
                txtTaskDescription.Focus();
                return;
            }
            int priority = _priorities.First(p => p.Description == cboPriority.Text.ToString()).PriorityID;

            var task = new TasksVM()
            {
                EventID = _task.EventID,
                TaskEventName = _task.TaskEventName,
                TaskID = _task.TaskID,
                Name = _task.Name,
                Description = txtTaskDescription.Text,
                // cboAssign variable,
                DueDate = (DateTime)dtpTaskDueDate.SelectedDate,
                Priority = priority,
                TaskPriority = cboPriority.Text.ToString(),
                Active = true
            };
            int numTotalVolunteers = (int)sldrNumVolunteers.Value;
            try
            {
                if (_taskManager.EditTask(_task, task) && _needManager.UpdateVolunteerNeed(_need, numTotalVolunteers))
                {
                    MessageBox.Show("Task updated");
                }
                else
                {
                    MessageBox.Show("There was a problem updating the task.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was a problem updating the task." + "\n\n" + ex.Message);
            }
            finally
            {
                pgTaskListView viewTasksPage = new pgTaskListView(_event, _managerProvider, _user);
                this.NavigationService.Navigate(viewTasksPage);
            }
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/02/10
        /// 
        /// Description:
        /// Click event handler for delete button
        /// feature not implemented yet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteTask_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This feature has not been implemented yet.");
        }

        // --------------------------------------------------- Vertical Buttons Click Events --------------------------------------------------------//

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/02/25
        /// 
        /// Description:
        /// Click event handler to take users back to event details page
        /// Works like cancel button for editing
        /// 
        /// Update:
        /// Derrick Nagy
        /// Updated: 2022/02/26
        /// 
        /// Description:
        /// Added _user to be passed with page constructor
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEventDetails_Click(object sender, RoutedEventArgs e)
        {
            string message = "Task will not be saved if you stop now.";
            string title = "Stop creating Task?";
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxImage image = MessageBoxImage.Warning;
            var result = MessageBox.Show(message, title, buttons, image);

            if (result == MessageBoxResult.No)
            {
                return;
            }
            else
            {
                pgEventEditDetail eventEditDetailPage = new pgEventEditDetail(_event, _managerProvider, _user);
                this.NavigationService.Navigate(eventEditDetailPage);
            }
            
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/02/25
        /// 
        /// Description:
        /// Click event handler to take users back to View Task List page
        /// Works like cancel button for editing
        /// 
        /// Update:
        /// Derrick Nagy
        /// Updated: 2022/02/26
        /// 
        /// Description:
        /// Added _user to be passed with page constructor
        /// 
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTasks_Click(object sender, RoutedEventArgs e)
        {

            string message = "Task will not be saved if you stop now.";
            string title = "Stop creating Task?";
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxImage image = MessageBoxImage.Warning;
            var result = MessageBox.Show(message, title, buttons, image);

            if (result == MessageBoxResult.No)
            {
                return;
            }
            else
            {
                pgTaskListView viewTasksPage = new pgTaskListView(_event, _managerProvider, _user);
                this.NavigationService.Navigate(viewTasksPage);
            }
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/02/25
        /// 
        /// Description:
        /// Click event handler to take users to View Activities page
        /// Works like cancel button for editing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItinerary_Click(object sender, RoutedEventArgs e)
        {
            string message = "Task will not be saved if you stop now.";
            string title = "Stop creating Task?";
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxImage image = MessageBoxImage.Warning;
            var result = MessageBox.Show(message, title, buttons, image);

            if (result == MessageBoxResult.No)
            {
                return;
            }
            else
            {
                pgViewActivities viewActivitiesPage = new pgViewActivities(_event, _managerProvider);
                this.NavigationService.Navigate(viewActivitiesPage);
            }
        }

        // ---------------------------------------------------- End Vertical Buttons Handlers --------------------------------------------------------//
    }
}
