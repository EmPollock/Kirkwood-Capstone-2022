using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayerInterfaces
{
    /// <summary>
    /// Austin Timmerman
    /// Created: 2022/02/22
    /// 
    /// Description:
    /// Interface for handling Sublocation manager class methods
    /// </summary>
    public interface ISublocationManager
    {
        List<Sublocation> RetrieveSublocationsByLocationID(int locationID);
    }
}
