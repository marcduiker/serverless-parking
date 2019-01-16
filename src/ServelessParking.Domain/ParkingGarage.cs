using System;
using System.Collections.Generic;
using System.Linq;

namespace ServelessParking.Domain
{
    public class ParkingGarage
    {
        public ParkingGarage(
            string name,
            DateTime day,
            int capacity)
        {
            Name = name;
            Day = day;
            ParkingSpaces = new List<ParkingSpace>(capacity);
        }

        public string Name { get; set; }

        public DateTime Day { get; set; }

        public List<ParkingSpace> ParkingSpaces { get; set; }

        public bool ReserveParkingSpace()
        {

            var firstFreeSpace = ParkingSpaces.FirstOrDefault(p => p.Status == ParkingSpaceStatusType.Free);
            if (firstFreeSpace == null)
            {
                return false;
            }

            firstFreeSpace.Reserve();

            return true;
        }

        public bool OccupyParkingSpace(bool withReservation = false)
        {
            var firstAvailableSpace = withReservation ? 
                ParkingSpaces.FirstOrDefault(p => p.Status == ParkingSpaceStatusType.Reserved) 
                : ParkingSpaces.FirstOrDefault(p => p.Status == ParkingSpaceStatusType.Free);

            if (firstAvailableSpace == null)
            {
                return false;
            }

            firstAvailableSpace.Occupy();

            return true;
        }

        public bool FreeParkingSpace()
        {
            var firstOccupiedSpace = ParkingSpaces.FirstOrDefault(p => p.Status == ParkingSpaceStatusType.Occupied);
            if (firstOccupiedSpace == null)
            {
                return false;
            }

            firstOccupiedSpace.Free();

            return true;
        }

    }
}
