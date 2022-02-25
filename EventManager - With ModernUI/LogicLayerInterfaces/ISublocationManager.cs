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
        Sublocation RetrieveSublocationBySublocationID(int sublocationID);
        List<Sublocation> RetrieveSublocationsByLocationID(int locationID);
    }
}
