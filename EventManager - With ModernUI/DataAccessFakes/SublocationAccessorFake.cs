using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessInterfaces;

namespace DataAccessFakes
{
    public class SublocationAccessorFake : ISublocationAccessor
    {
        private List<Sublocation> _fakeSublocations = new List<Sublocation>();

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/02/01
        /// 
        /// Description:
        /// Constructor that adds fake Sublocations to the _fakeSublocations list for test data
        /// 
        /// Christopher Repko
        /// Updated: 2022/02/25
        /// 
        /// Description: Added Location IDs
        /// </summary>
         
        public SublocationAccessorFake()
        {
            _fakeSublocations.Add(new Sublocation()
            {
                SublocationID = 1000001,

                LocationID = 1000000,

                SublocationName = "Fake Sublocation 1",
                SublocationDescription = "The first fake sublocation"
            });

            _fakeSublocations.Add(new Sublocation()
            {
                SublocationID = 1000001,

                LocationID = 1000001,

                SublocationName = "Fake Sublocation 2",
                SublocationDescription = "The second fake sublocation"
            });

            _fakeSublocations.Add(new Sublocation()
            {
                SublocationID = 1000002,

                LocationID = 1000000,
                SublocationName = "Fake Sublocation 3",
                SublocationDescription = "The third fake sublocation"
            });

            _fakeSublocations.Add(new Sublocation()
            {
                SublocationID = 1000003,
                LocationID = 1000001,
				
                SublocationName = "Fake Sublocation 4",
                SublocationDescription = "The fourth fake sublocation"
            });

            _fakeSublocations.Add(new Sublocation()
            {
                SublocationID = 1000004,
                LocationID = 1000000,

                SublocationName = "Fake Sublocation 5",
                SublocationDescription = "The fith fake sublocation"
            });
        }

        public int InsertSublocationByLocationID(int locationID, string sublocationName, string description)
        {
            int rows = 0;
            _fakeSublocations.Add(new Sublocation() 
            {
                LocationID = 1000005,
                SublocationID = 1000005,
                SublocationName = "Fake Sublocation",
                SublocationDescription = ""
            });
            rows++;

            return rows;
            
        }

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/02/03
        /// 
        /// Description:
        /// Returns a specific Sublocation
        /// 
        /// Christopher Repko
        /// Updated: 2022/02/24
        /// Changed logic to be more consistent with a database call.
        ///
        /// </summary>
        /// <param name="sublocationID"></param>
        /// <returns>a fake Sublocation object</returns>
        public Sublocation SelectSublocationBySublocationID(int sublocationID)
        {          

            foreach (Sublocation sublocation in _fakeSublocations)
            {
                if(sublocation.SublocationID == sublocationID)
                {
                    return sublocation;
                }
            }

            return null;
        }


        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/22
        /// 
        /// Description:
        /// Returns a list of sublocations by the locationID
        /// 
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns>a list of fake sublocation objects</returns>
        public List<Sublocation> SelectSublocationsByLocationID(int locationID)
        {
            List<Sublocation> sublocations = new List<Sublocation>();

            try
            {
                foreach(Sublocation sublocation in _fakeSublocations)
                {
                    if(sublocation.LocationID == locationID)
                    {
                        sublocations.Add(sublocation);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return sublocations;
        }
    }
}
