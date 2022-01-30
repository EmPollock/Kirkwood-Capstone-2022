using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessInterfaces;

namespace DataAccessFakes
{
    /// <summary>
    /// Austin Timmerman
    /// Created: 2022/01/26
    /// 
    /// The VolunteerAccessorFake for all volunteer fake data
    /// </summary>
    public class VolunteerAccessorFake : IVolunteerAccessor
    {
        List<Volunteer> _fakeVolunteers = new List<Volunteer>();
        private List<String> _fakePasswordHashes = new List<string>();

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/01/26
        /// 
        /// Description:
        /// The volunteer accessor fakes for fake volunteers
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        public VolunteerAccessorFake()
        {

            this._fakeVolunteers.Add(new Volunteer()
            {
                VolunteerID = 999999,
                UserID = 999999,
                GivenName = "Tess",
                FamilyName = "Data",
                Email = "tess@company.com",
                State = "IA",
                City = "Atkins",
                Zip = 52206,
                Active = true,
                Rating = 5,
                DateCreated = DateTime.Now
            });
            this._fakeVolunteers.Add(new Volunteer()
            {
                VolunteerID = 999998,
                UserID = 999998,
                GivenName = "Duplicate",
                FamilyName = "Data",
                Email = "duplicate@company.com",
                State = "IA",
                City = "Atkins",
                Zip = 52206,
                Active = true,
                Rating = 4,
                DateCreated = DateTime.Now
            });
            this._fakeVolunteers.Add(new Volunteer()
            {
                VolunteerID = 999997,
                UserID = 999997,
                GivenName = "Duplicate",
                FamilyName = "Data",
                Email = "duplicate@company.com",
                State = "IA",
                City = "Atkins",
                Zip = 52206,
                Active = true,
                Rating = 3,
                DateCreated = DateTime.Now
            });

            this._fakePasswordHashes.Add("9C9064C59F1FFA2E174EE754D2979BE80DD30DB552EC03E7E327E9B1A4BD594E");
            this._fakePasswordHashes.Add("dup-9C9064C59F1FFA2E174EE754D2979BE80DD30DB552EC03E7E327E9B1A4BD594E");
            this._fakePasswordHashes.Add("dup-9C9064C59F1FFA2E174EE754D2979BE80DD30DB552EC03E7E327E9B1A4BD594E");
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/01/26
        /// 
        /// Description:
        /// The accessor fake to select all volunteer reviews
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        /// <exception cref="Exception">Selection fails</exception>
        /// <returns>A list of volunteer data object shells for the volunteer ID and rating</returns>
        public List<Volunteer> SelectAllVolunteerReviews()
        {
            List<Volunteer> volunteers = new List<Volunteer>();

            try
            {
                volunteers = _fakeVolunteers;
            }
            catch (Exception)
            {

                throw;
            }

            return volunteers;
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/01/26
        /// 
        /// Description:
        /// The accessor fake to select all volunteers
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        /// <exception cref="Exception">Selection fails</exception>
        /// <returns>A list of volunteer data objects</returns>

        public List<Volunteer> SelectAllVolunteers()
        {
            List<Volunteer> volunteers = new List<Volunteer>();

            try
            {
                volunteers = _fakeVolunteers;
            }
            catch (Exception)
            {

                throw;
            }

            return volunteers;
        }
    }
}
