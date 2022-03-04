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
    public class EntranceManager : IEntranceManager
    {

        IEntranceAccessor _entranceAccessor = null;

        /// <summary>
        /// Alaina Gilson
        /// Created: 2022/02/27
        /// 
        /// Description:
        /// Constructor for entrance manager using the entrance accessor
        /// </summary>
        public EntranceManager()
        {
            _entranceAccessor = new EntranceAccessor();
        }

        /// <summary>
        /// Alaina Gilson
        /// Created: 2022/02/27
        /// 
        /// Description:
        /// Constructor that takes an IEntranceAccessor and sets it to the _entranceAccessor field for passing test data
        /// </summary>
        /// <param name="entranceAccessor"></param>
        public EntranceManager(IEntranceAccessor entranceAccessor)
        {
            _entranceAccessor = entranceAccessor;
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/03/04
        /// 
        /// Description:
        /// Method that retrieves all entrances for a location by it's LocationID
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns></returns>
        public List<Entrance> RetrieveEntranceByLocationID(int locationID)
        {
            List<Entrance> entrances = new List<Entrance>();

            try
            {
                entrances = _entranceAccessor.SelectEntranceByLocationID(locationID);
            }
            catch (Exception)
            {

                throw;
            }

            return entrances;
        }
    }
}
