using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessInterfaces;

namespace DataAccessFakes
{
    public class ParkingLotAccessorFake : IParkingLotAccessor
    {        
        private List<ParkingLotVM> _fakeParkingLots = new List<ParkingLotVM>();

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/01
        /// 
        /// Description:
        /// Constructor that adds fake parking lots to a list for tesing purposes
        /// 
        /// </summary>
        public ParkingLotAccessorFake()
        {
            _fakeParkingLots.Add(new ParkingLotVM()
            {
                LotID = 100000,
                LocationID = 100000,
                Name = "Test Parking Lot A",
                Description = "A description for test parking lot A",
                ImageName = null,
                LocationName = "Test Location"
            });

            _fakeParkingLots.Add(new ParkingLotVM()
            {
                LotID = 100001,
                LocationID = 100000,
                Name = "Test Parking Lot B",
                Description = "A description for test parking lot B",
                ImageName = null,
                LocationName = "Test Location"
            });

            _fakeParkingLots.Add(new ParkingLotVM()
            {
                LotID = 100002,
                LocationID = 100000,
                Name = "Test Parking Lot C",
                Description = "A description for test parking lot C",
                ImageName = null,
                LocationName = "Test Location"
            });

            _fakeParkingLots.Add(new ParkingLotVM()
            {
                LotID = 100003,
                LocationID = 100001,
                Name = "Test Parking Lot For Location 2",
                Description = "A description for test parking lot 4 for Test location 2",
                ImageName = null,
                LocationName = "Test Location 2"
            });

            _fakeParkingLots.Add(new ParkingLotVM()
            {
                LotID = 100004,
                LocationID = 100002,
                Name = "Test Parking Lot For Location 3",
                Description = "A description for test parking lot 5 for Test location 3",
                ImageName = null,
                LocationName = "Test Location 3"
            });

        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/01
        /// 
        /// Description:
        /// Inserts a fake parking lot object
        /// </summary>
        /// <param name="parkingLot"></param>
        /// <returns>The inserted parking lot id</returns>
        public int InsertParkingLot(ParkingLot parkingLot)
        {
            int lotID = 0;
            lotID = nextAvailableLotID();

            _fakeParkingLots.Add(new ParkingLotVM()
            {
                LotID = lotID,
                LocationID = parkingLot.LocationID,
                Name = parkingLot.Name,
                Description = parkingLot.Description,
                ImageName = parkingLot.ImageName
            });

            return lotID;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/01
        /// 
        /// Description:
        /// Selects the list of parking lots for the location ID
        /// </summary>
        /// <param name="locationID">The locationID</param>
        /// <returns>The ParkingLotVMs for the location </returns>
        public List<ParkingLotVM> SelectParkingLotByLocationID(int locationID)
        {
            return _fakeParkingLots.FindAll(pl => pl.LocationID == locationID);
        }

        private int nextAvailableLotID()
        {
            int lotID = 0;

            lotID = _fakeParkingLots[_fakeParkingLots.Count - 1].LotID + 1;

            return lotID;
        }
    }
}
