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
        DataObjects.Event _event = null;

        // priority values to populate cboPriority
        List<Priority> _priorities = new List<Priority>();

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/01/23
        /// 
        /// Description:
        /// Initializes component and sets up task manager with either fake or default accessors
        /// </summary>
        public pgTaskListCreate()
        {
            // fake accessor
            //_taskManager = new TaskManager(new DataAccessFakes.TaskAccessorFakes());

            // default accessor
            _taskManager = new TaskManager();
            _eventManager = new LogicLayer.EventManager();

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
            // txtBlkEventName.Text = _event.EventName;
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
            string taskName = txtTaskName.Text.ToString();

            string taskDescription = txtTaskDescription.Text.ToString();

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
                Name = taskName,
                Description = taskDescription,
                DueDate = taskDueDate,
                Priority = priority
            };
            try
            {
                _taskManager.AddTask(task);
                MessageBox.Show("Task has been added.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was a problem creating the new task." + "\n\n" + ex.Message);
            }
        }

        
    }
}
