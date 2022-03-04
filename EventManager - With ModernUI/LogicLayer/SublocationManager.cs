﻿using System;
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
        /// Christopher Repko
        /// Created 2022/02/24
        /// 
        /// Description:
        /// Retrieves a sublocation based on a sublocationID
        /// </summary>
        /// <param name="sublocationID">SublocationID to retrieve the sublocation for</param>
        /// <returns>A sublocation matching the sublocationID passed in.</returns>
        public Sublocation RetrieveSublocationBySublocationID(int sublocationID)
        {
            Sublocation result = null;
            try
            {
                result = this._sublocationAccessor.SelectSublocationBySublocationID(sublocationID);
            } catch(Exception ex)
            {
                throw new ApplicationException("Failed to retrieve sublocation", ex);
            }
            return result;
        }

        /// <summary>
        /// Christopher Repko
        /// Created 2022/02/24
        /// 
        /// Description:
        /// Retrieves a list of sublocations based on a locationID
        /// </summary>
        /// <param name="locationID">LocationID to retrieve sublocations matching.</param>
        /// <returns>A list of sublocations matching the locationID passed in.</returns>
        public List<Sublocation> RetrieveSublocationsByLocationID(int locationID)
        {
            List<Sublocation> result = new List<Sublocation>();
            try
            {
                result = this._sublocationAccessor.SelectSublocationsByLocationID(locationID);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to retrieve list of sublocations for location", ex);
            }
            return result;
        }
    }
}