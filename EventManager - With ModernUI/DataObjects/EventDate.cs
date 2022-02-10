﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Derrick Nagy
    /// Created: 2022/01/29
    /// 
    /// Description:
    /// Class for event date
    /// </summary>
    public class EventDate
    {
        public DateTime EventDateID { get; set; }
        public int EventID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool Active { get; set; }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/02/07
        /// 
        /// Description:
        /// Default constuctor that sets DateTime objects to DateTime.MinValue,
        /// used for testing when left blank
        /// </summary>
        public EventDate()
        {
            EventDateID = DateTime.MinValue;
            EventID = 0;
            StartTime = DateTime.MinValue;
            EndTime = DateTime.MinValue;
            Active = true;
        }
    }
}
