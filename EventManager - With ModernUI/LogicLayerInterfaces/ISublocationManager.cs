using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects; 

namespace LogicLayerInterfaces
{
    public interface ISublocationManager
    {
        int InsertSubLocation(int locationID, string sublocationName, string sublocationDescription);
        List<Sublocation> RetrieveSublocationsByLocationID(int locationID);
    }
}
