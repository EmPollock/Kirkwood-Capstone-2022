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
        DataObjects.Event _event = null;
        ITaskManager _taskManager = null;
        IEventManager _eventManager = null;

        // create a priority list to populate in a bit
        List<Priority> _priorities = new List<Priority>();

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/02/08
        /// 
        /// Description:
        /// Initializes components and sets up task/event managers as well
        /// as the selectedTask object along with a list of priorities
        /// </summary>
        /// <param name="selectedTask"></param>
        public pgTaskListEdit(DataObjects.TasksVM selectedTask, DataObjects.Event selectedEvent)
        {
            // fake accessors for testing purposes
            //_taskManager = new TaskManager(new TaskAccessorFakes());
            //_eventManager = new LogicLayer.EventManager(new EventAccessorFake());

            // default accessors for running program
            _taskManager = new TaskManager();
            _eventManager = new LogicLayer.EventManager();

            _task = selectedTask;
            _event = selectedEvent;

            _task.TaskEventName = _event.EventName;
            _task.EventID = _event.EventID;

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
        /// </summary>
        private void populateControls()
        {
            txtBlkEventName.Text = _task.TaskEventName; // pass up event name from view
            txtTaskName.Text = _task.Name.ToString();
            txtTaskName.IsReadOnly = true;
            txtTaskName.IsEnabled = false;
            txtTaskDescription.Text = _task.Description.ToString();
            cboAssign.SelectedItem = "Unavailable"; // pass up volunteer when available
            dtpTaskDueDate.SelectedDate = _task.DueDate;
            cboPriority.SelectedItem = _task.TaskPriority;
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/02/08
        /// 
        /// Description:
        /// Checks if the user wants to leave the page without saving update
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
                pgTaskListView viewTasksPage = new pgTaskListView(_event);
                this.NavigationService.Navigate(viewTasksPage);
            }
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/02/08
        /// 
        /// Description:
        /// Click event handler to save the updated task
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

            try
            {
                if(_taskManager.EditTask(_task, task))
                {
                    MessageBox.Show("Task updated");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was a problem updating the task." + "\n\n" + ex.Message);
            }
            finally
            {
                pgTaskListView viewTasksPage = new pgTaskListView(_event);
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
    }
}
