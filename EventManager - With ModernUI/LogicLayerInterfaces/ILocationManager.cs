using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayerInterfaces
{
    /// <summary>
    /// Kris Howell
    /// Created: 2022/02/03
    /// 
    /// Description:
    /// Interface for handling Location manager class methods
    /// </summary>
    public interface ILocationManager
    {
        List<Location> RetrieveActiveLocations();
    }
}
