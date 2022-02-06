﻿using System;
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
        /// </summary>

        ITaskManager _taskManager = null;
        IEventManager _eventManager = null;
        DataObjects.Event _event = null;

        public pgTaskListView()
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
        /// Created: 2022/02/01
        /// 
        /// Description:
        /// Load event to populate the DataGrid. Loops through the columns to find the column
        /// with the header of "Name" and collapse all the other columns
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            datViewAllTasksForEvent.ItemsSource = _taskManager.RetrieveAllActiveTasksByEventID(100000);
            datViewAllTasksForEvent.RowHeaderWidth = 0;


            foreach(DataGridColumn column in datViewAllTasksForEvent.Columns)
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
            
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/02/02
        /// 
        /// Description:
        /// Add task button brings up pgTaskListCreate which will update the list upon 
        /// psTaskListCreate's closing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTaskListCreate_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Button will bring up Task Create page soon.");
            Uri pageUri = new Uri("Event/pgTaskListCreate.xaml", UriKind.Relative);
            this.NavigationService.Navigate(pageUri);
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/02/03
        /// 
        /// Description:
        /// View details of task. To be changed to Edit Task
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void datViewAllTasksForEvent_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var task = (TasksVM)datViewAllTasksForEvent.SelectedItem;

            MessageBox.Show("Task Name: " + task.Name + "\n\n" +
                            "Description: " + task.Description + "\n\n" +
                            "Due Date: " + task.DueDate + "\n\n" +
                            "Priority: " + task.TaskPriority);
        }
    }
}