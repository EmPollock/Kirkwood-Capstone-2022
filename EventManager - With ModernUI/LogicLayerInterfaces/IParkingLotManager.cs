﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayerInterfaces
{
    public interface IParkingLotManager
    {
        List<ParkingLotVM> RetrieveParkingLotByLocationID(int locationID);
        int CreateParkingLot(ParkingLot parkingLot);

        bool UserCanEditParkingLot(int userID);
        bool RemoveParkingLotByLotID(int lotID);

    }
}