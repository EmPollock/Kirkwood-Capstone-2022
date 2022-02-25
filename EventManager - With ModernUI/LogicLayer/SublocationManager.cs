using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataObjects;
using DataAccessInterfaces;
using LogicLayerInterfaces; 

namespace LogicLayer
{
    public class SublocationManager : ISublocationManager
    {
        private SublocationAccessor _sublocationAccessor;

        public SublocationManager(DataAccessFakes.SublocationAccessorFake sublocationAccessorFake)
        {
            _sublocationAccessor = new SublocationAccessor();
        }
        public int InsertSubLocation(int locationID, string sublocationName, string sublocationDescription)
        {
            int rows = 0;
            try
            {
                rows = _sublocationAccessor.InsertSublocationByLocationID(locationID, sublocationName, sublocationDescription);
            }
            catch (Exception e) 
            {
                throw e;
            }
            return rows;
        }

        public List<Sublocation> RetrieveSublocationsByLocationID(int locationID)
        {
            List<Sublocation> sublocations = new List<Sublocation>();
            try
            {
                _sublocationAccessor.SelectSublocationsByLocationID(locationID);
            }
            catch (Exception e) 
            {
                throw e;
            }
            return sublocations;
        }
    }
}
