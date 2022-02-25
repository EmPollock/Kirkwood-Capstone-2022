using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessInterfaces
{
    public interface ISublocationAccessor
    {
        Sublocation SelectSublocationBySublocationID(int sublocationID);
        int InsertSublocationByLocationID(int locationID, string sublocationName, string description);
        List<Sublocation> SelectSublocationsByLocationID(int locationID);
    }
}
