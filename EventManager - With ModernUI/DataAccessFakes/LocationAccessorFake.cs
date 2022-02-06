﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessInterfaces;

namespace DataAccessFakes
{
    public class LocationAccessorFake : ILocationAccessor
    {
        private List<Location> _fakeLocations = new List<Location>();
        private List<LocationReview> _fakeLocationReviews = new List<LocationReview>();
        private List<LocationImage> _fakeLocationImages = new List<LocationImage>();

        /// <summary>
        /// Kris Howell
        /// Created: 2022/01/27
        /// 
        /// Description:
        /// Constructor to populate _fakeLocations with dummy values for testing purposes
        /// 
        /// </summary>
        public LocationAccessorFake()
        {
            _fakeLocations.Add(new Location()
            {
                LocationID = 100000,
                UserID = 100000,
                Name = "Test Location 1",
                Description = "Description of Test Location 1 goes here.",
                PricingInfo = "Pricing information for renting Test Location 1 goes here.",
                Phone = "111-111-1111",
                Email = "testLocation1@locations.com",
                Address1 = "Test Location 1 Street",
                City = "Iowa City",
                State = "Iowa",
                ZipCode = "52240",
                ImagePath = "http://imagehost.com/testlocation1.png",
                Active = true
            });

            _fakeLocations.Add(new Location()
            {
                LocationID = 100001,
                UserID = 100000,
                Name = "Test Location 2",
                Description = "Description of Test Location 2 goes here.",
                PricingInfo = "Pricing information for renting Test Location 2 goes here.",
                Phone = "222-222-2222",
                Email = "testLocation2@locations.com",
                Address1 = "Test Location 2 Street",
                Address2 = "Apt 2",
                City = "Cedar Rapids",
                State = "Iowa",
                ZipCode = "52404",
                ImagePath = "http://imagehost.com/testlocation2.png",
                Active = true
            });

            _fakeLocations.Add(new Location()
            {
                LocationID = 100002,
                UserID = 100000,
                Name = "Test Location 3",
                Description = "Description of Test Location 3 goes here.",
                PricingInfo = "Pricing information for renting Test Location 3 goes here.",
                Phone = "333-333-3333",
                Email = "testLocation3@locations.com",
                Address1 = "Test Location 3 Street",
                Address2 = "Apt 33",
                City = "Chicago",
                State = "Illinois",
                ZipCode = "60007",
                ImagePath = "http://imagehost.com/testlocation3.png",
                Active = true
            });

            _fakeLocations.Add(new Location()
            {
                LocationID = 100003,
                UserID = 100000,
                Name = "Test Location 4",
                Description = "Description of Test Location 4 goes here.",
                PricingInfo = "Pricing information for renting Test Location 4 goes here.",
                Phone = "444-444-4444",
                Email = "testLocation4@locations.com",
                Address1 = "Test Location 4 Street",
                Address2 = "Apt 44",
                City = "New York City",
                State = "New York",
                ZipCode = "10036",
                ImagePath = "http://imagehost.com/testlocation4.png",
                Active = true
            });

            _fakeLocations.Add(new Location()
            {
                LocationID = 100004,
                UserID = 100000,
                Name = "Test Location 5 Inactive",
                Description = "Description of Inactive Test Location 5 goes here.",
                PricingInfo = "Pricing information for renting inactive Test Location 5 goes here.",
                Phone = "555-555-5555",
                Email = "testLocation5@locations.com",
                Address1 = "Test Location 5 Street",
                Address2 = "Apt 55",
                City = "Detroit",
                State = "Michigan",
                ZipCode = "48202",
                ImagePath = "http://imagehost.com/testlocation5.png",
                Active = false
            });

            _fakeLocations.Add(new Location()
            {
                LocationID = 100005,
                Name = "Test Location 6 Min Info",
                Address1 = "Test Location 6 Street",
                City = "Detroit",
                State = "Michigan",
                ZipCode = "48202",
                Active = true
            });

            _fakeLocationReviews.Add(new LocationReview()
            {
                LocationID = 100000,
                ReviewID = 100000,
                FullName = "Tess Data",
                ReviewType = "Location Review",
                Rating = 5,
                Review = "Great place",
                DateCreated = DateTime.Now,
                Active = true
            });

            _fakeLocationReviews.Add(new LocationReview()
            {
                LocationID = 100000,
                ReviewID = 100001,
                FullName = "Joey Speed",
                ReviewType = "Location Review",
                Rating = 2,
                Review = "Not a good place one bit",
                DateCreated = DateTime.Now,
                Active = true
            });

            _fakeLocationImages.Add(new LocationImage()
            {
                LocationID = 100000,
                ImageName = "f43faecc-5d0f-4b4a-ba47-4c1d3ce56912.jpg"
            });

            _fakeLocationImages.Add(new LocationImage()
            {
                LocationID = 100000,
                ImageName = "7263a839-3428-49f2-b26f-875d3811ef85.jpg"
            });
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/02/03
        /// 
        /// Description:
        /// Returns all active locations in fake location list
        /// </summary>
        /// 
        /// <returns>List of active locations</returns>
        public List<Location> SelectActiveLocations()
        {
            List<Location> locations = new List<Location>();

            foreach (Location fakeLocation in _fakeLocations)
            {
                if (fakeLocation.Active)
                {
                    locations.Add(fakeLocation);
                }
            }

            return locations;
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/03
        /// 
        /// Description:
        /// The method that selects a location by its LocationID
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        /// <param name="locationID"></param>
        /// <returns>A Location object that matches the provided locationID or a null location if not there</returns>
        public Location SelectLocationByLocationID(int locationID)
        {
            Location location = new Location();

            foreach (Location fakeLocation in _fakeLocations)
            {
                if (fakeLocation.LocationID == locationID)
                {
                    location = fakeLocation;
                }
            }

            return location;
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/05
        /// 
        /// Description:
        /// The method that selects a location images by its LocationID
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        /// <param name="locationID"></param>
        /// <returns>A list of LocationImage object that matches the provided locationID or a null location if not there</returns>
        public List<LocationImage> SelectLocationImages(int locationID)
        {
            List<LocationImage> locationImages = new List<LocationImage>();

            foreach (LocationImage fakeLocationImage in _fakeLocationImages)
            {
                if (fakeLocationImage.LocationID == locationID)
                {
                    locationImages.Add(fakeLocationImage);
                }
            }

            return locationImages;
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/04
        /// 
        /// Description:
        /// The method that selects a location reviews by its LocationID
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        /// <param name="locationID"></param>
        /// <returns>A list of location review objects</returns>
        public List<LocationReview> SelectLocationReviews(int locationID)
        {
            List<LocationReview> locationReviews = new List<LocationReview>();

            foreach (LocationReview fakeLocationReview in _fakeLocationReviews)
            {
                if (fakeLocationReview.LocationID == locationID)
                {
                    locationReviews.Add(fakeLocationReview);
                }
            }

            return locationReviews;
        }
    }
}