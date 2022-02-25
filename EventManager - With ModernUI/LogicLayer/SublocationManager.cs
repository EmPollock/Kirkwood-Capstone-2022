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
      
    }
}
