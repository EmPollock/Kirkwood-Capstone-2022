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
    /// Austin Timmerman
    /// Created: 2022/01/26
    /// 
    /// Interaction logic for pgViewAllVolunteers.xaml
    /// </summary>
    public partial class pgViewAllVolunteers : Page
    {
        IVolunteerManager _volunteerManager = null;

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/01/26
        /// 
        /// Description:
        /// The default constructor for the ViewAllVolunteersPage
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        public pgViewAllVolunteers()
        {
            // fake accessor
            //_volunteerManager = new VolunteerManager(new VolunteerAccessorFake());

            // default accessor
            _volunteerManager = new VolunteerManager();

            InitializeComponent();
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/01/26
        /// 
        /// Description:
        /// Helper method that loads the volunteers data grid
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        public void loadAllVolunteers()
        {
            // This is used to display the users review next to their name. Because not every volunteer may have
            // a review, this will set a "default" for those missing from the volunteer reviews table.

            List<Volunteer> volunteers = new List<Volunteer>();
            List<Volunteer> volunteerReviews = new List<Volunteer>();
            try
            {
                volunteers = _volunteerManager.RetrieveAllVolunteers();
                volunteerReviews = _volunteerManager.RetrieveAllVolunteerReviews();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + "\n\n" + ex.Message);
            }

            for (int i = 0; i < volunteers.Count; i++)
            {
                for (int j = 0; j <= volunteerReviews.Count; j++)
                {
                    if (j == volunteerReviews.Count)
                    {
                        volunteers[i].Rating = 0;
                        break;
                    }

                    if (volunteers[i].VolunteerID == volunteerReviews[j].VolunteerID)
                    {
                        volunteers[i].Rating = volunteerReviews[j].Rating;
                        break;
                    }
                }

            }

            datVolunteers.ItemsSource = volunteers;

        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/01/29
        /// 
        /// Description:
        /// Method that calls the loadAllVolunteers helper method when the page is loaded
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            loadAllVolunteers();
        }

    }
}
