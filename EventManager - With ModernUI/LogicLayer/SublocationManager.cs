using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using LogicLayerInterfaces;
using DataAccessInterfaces;
using DataAccessLayer;

namespace LogicLayer
{
    /// <summary>
    /// Austin Timmerman
    /// Created: 2022/02/22
    /// 
    /// Description:
    /// Class for handling Sublocation manager methods
    /// </summary>
    public class SublocationManager : ISublocationManager
    {
        ISublocationAccessor _sublocationAccessor = null;

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/22
        /// 
        /// Description:
        /// Default constructor for sublocation manager using the live sublocation accessor
        /// </summary>
        public SublocationManager()
        {
            _sublocationAccessor = new SublocationAccessor();
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/22
        /// 
        /// Description:
        /// Constructor for sublocation manager using a given sublocation accessor
        /// </summary>
        /// <param name="sublocationAccessor"></param>
        public SublocationManager(ISublocationAccessor sublocationAccessor)
        {
            _sublocationAccessor = sublocationAccessor;
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/22
        /// 
        /// Description:
        /// Method retrieving sublocations by the passed locationID
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns>List of Sublocation objects</returns>
        public List<Sublocation> RetrieveSublocationsByLocationID(int locationID)
        {
            List<Sublocation> sublocations = new List<Sublocation>();

            try
            {
                sublocations = _sublocationAccessor.SelectSublocationsByLocationID(locationID);
            }
            catch (Exception)
            {

                throw;
            }

            return sublocations;
        }
    }
}
