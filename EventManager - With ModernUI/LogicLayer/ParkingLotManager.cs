using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataAccessLayer;
using DataObjects;
using LogicLayerInterfaces; 

namespace LogicLayer
{
    public class ParkingLotManager : IParkingLotManager
    {
        IParkingLotAccessor _parkingLotAccessor = null;


        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/01
        /// 
        /// Description:
        /// Constructor for parking lot manager using the parking lot accessor
        /// </summary>
        public ParkingLotManager()
        {
            _parkingLotAccessor = new ParkingLotAccessor();
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/01
        /// 
        /// Description:
        /// Constructor for parking lot manager that takes an IParkingLotAccessor for testing purposes
        /// </summary>
        /// <param name="parkingLotAccessor">An object that implements IParkingLotAccessor</param>
        public ParkingLotManager(IParkingLotAccessor parkingLotAccessor)
        {
            _parkingLotAccessor = parkingLotAccessor;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/01
        /// 
        /// Description:
        /// Inserts a parking lot for a location
        /// </summary>
        /// <param name="parkingLot"></param>
        /// <returns></returns>
        public int CreateParkingLot(ParkingLot parkingLot)
        {
            int lotID = 0;
            // Green
            //lotID = 100005;

            if (parkingLot.LocationID == 0)
            {
                throw new ApplicationException("No Location to associate with the parking lot. Add a location.");
            }

            if (parkingLot.Name == "" || parkingLot.Name == null)
            {
                throw new ApplicationException("Please enter a name for the parking lot.");
            }

            if (parkingLot.Name.Length > 160)
            {
                throw new ApplicationException("The name of the parking lot is too long.");
            }

            if (parkingLot.Description.Length > 3000)
            {
                throw new ApplicationException("The description of the parking lot is too long.");
            }

            try
            {
                lotID = _parkingLotAccessor.InsertParkingLot(parkingLot);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return lotID;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/01
        /// 
        /// Description:
        /// Selects the ParkingLotVM for the event location
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns></returns>
        public List<ParkingLotVM> RetrieveParkingLotByLocationID(int locationID)
        {
            List<ParkingLotVM> parkingLots = new List<ParkingLotVM>();

            // Green
            //parkingLots.Add(new ParkingLotVM());
            //parkingLots.Add(new ParkingLotVM());
            //parkingLots.Add(new ParkingLotVM());

            try
            {
                parkingLots = _parkingLotAccessor.SelectParkingLotByLocationID(locationID);
            }
            catch (Exception ex)
            {

                throw ex;
            }


            return parkingLots;
        }

    }
}
