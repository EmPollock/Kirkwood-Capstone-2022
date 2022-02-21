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
    /// <summary>
    /// Mike Cahow
    /// Created: 2022/01/23
    /// 
    /// Description:
    /// Interaction logic for pgTaskListCreate.xamls
    /// </summary
    public partial class pgTaskListCreate : Page
    {
        
        ITaskManager _taskManager = null;
        IEventManager _eventManager = null;
        DataObjects.EventVM _event = null;

        // priority values to populate cboPriority
        List<Priority> _priorities = new List<Priority>();

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/01/23
        /// 
        /// Description:
        /// Initializes component and sets up task manager with either fake or default accessors
        /// </summary>
        public pgTaskListCreate(DataObjects.EventVM selectedEvent)
        {
            // fake accessor
            //_taskManager = new TaskManager(new DataAccessFakes.TaskAccessorFakes());

            // default accessor
            _taskManager = new TaskManager();
            _eventManager = new LogicLayer.EventManager();
            _event = selectedEvent;

            InitializeComponent();
        }

        /// <summary>
        /// Mike Cahow
        /// Created 2022/01/24
        /// 
        /// Description:
        /// Using a load event to populate the priority drop down with it's string values of Description
        /// rather than the int values that are used in the Tasks data object
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            txtBlkEventName.Text = _event.EventName;
            try
            {
                _priorities = _taskManager.RetrieveAllPriorities();
                cboPriority.ItemsSource = from p in _priorities
                                          orderby p.PriorityID
                                          select p.Description;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Priority list was not retrieved." + "\n\n" + ex.Message);
            }
        }   

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/01/24
        /// 
        /// Description:
        /// Click event handler to create a task
        /// </summary>
        private void btnSaveTask_Click(object sender, RoutedEventArgs e)
        {
            int eventID = _event.EventID;

            if (txtTaskName.Text == "" || txtTaskName.Text == null)
            {
                MessageBox.Show("Task name cannot be blank.");
                txtTaskName.Focus();
                return;
            }

            string taskName = txtTaskName.Text.ToString();

            if (txtTaskDescription.Text == "" || txtTaskDescription.Text == null)
            {
                MessageBox.Show("Task description cannot be blank.");
                txtTaskDescription.Focus();
                return;
            }

            string taskDescription = txtTaskDescription.Text.ToString();

            if(dtpTaskDueDate.SelectedDate == null)
            {
                MessageBox.Show("Please set a due date before continuing");
                dtpTaskDueDate.Focus();
                return;
            }

            DateTime taskDueDate = (DateTime)dtpTaskDueDate.SelectedDate;

            if (cboPriority.SelectedItem == null)
            {
                MessageBox.Show("Please set a priority before continuing.");
                cboPriority.Focus();
                return;
            }
            int priority = _priorities.First(p => p.Description == cboPriority.Text.ToString()).PriorityID;

            var task = new Tasks()
            {
                EventID = eventID,
                Name = taskName,
                Description = taskDescription,
                // cboAssign variable,
                DueDate = taskDueDate,
                Priority = priority
            };
            try
            {
                _taskManager.AddTask(task);
                MessageBox.Show("Task has been added.");
                pgTaskListView viewTasksPage = new pgTaskListView(_event);
                this.NavigationService.Navigate(viewTasksPage);
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was a problem creating the new task." + "\n\n" + ex.Message);
            }
        }

        /// <summary>
        /// Mike Cahow
        /// Create: 2022/02/04
        /// 
        /// Description:
        /// Event handler for the cancel button. If the yes is clicked in the dialog box
        /// it sends user back to pgTaskListView. If no is clicked then user remains on 
        /// current page
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

            if(result == MessageBoxResult.No)
            {
                return;
            }
            else
            {
                pgTaskListView viewTasksPage = new pgTaskListView(_event);
                this.NavigationService.Navigate(viewTasksPage);
            }
        }
    }
}
