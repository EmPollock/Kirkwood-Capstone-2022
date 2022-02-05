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
        /// </summary>
         
        public SublocationAccessorFake()
        {
            _fakeSublocations.Add(new Sublocation()
            {
                SublocationID = 1000001,
                //LocationID = ,
                SublocationName = "Fake Sublocation 1",
                SublocationDescription = "The first fake sublocation"
            });

            _fakeSublocations.Add(new Sublocation()
            {
                SublocationID = 1000001,
                //LocationID = ,
                SublocationName = "Fake Sublocation 2",
                SublocationDescription = "The second fake sublocation"
            });

            _fakeSublocations.Add(new Sublocation()
            {
                SublocationID = 1000002,
                //LocationID = ,
                SublocationName = "Fake Sublocation 3",
                SublocationDescription = "The third fake sublocation"
            });

            _fakeSublocations.Add(new Sublocation()
            {
                SublocationID = 1000003,
                //LocationID = ,
                SublocationName = "Fake Sublocation 4",
                SublocationDescription = "The fourth fake sublocation"
            });

            _fakeSublocations.Add(new Sublocation()
            {
                SublocationID = 1000004,
                //LocationID = ,
                SublocationName = "Fake Sublocation 5",
                SublocationDescription = "The fith fake sublocation"
            });
        }

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/02/03
        /// 
        /// Description:
        /// Returns a specific Sublocation
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

            throw new ArgumentException();
        }
    }
}
