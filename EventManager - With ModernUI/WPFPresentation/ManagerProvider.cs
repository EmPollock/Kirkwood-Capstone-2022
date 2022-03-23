using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayerInterfaces;
using LogicLayer;
using DataAccessFakes;

namespace WPFPresentation
{
    /// <summary>
    /// Chris Repko
    /// Created: 2022/01/27
    /// 
    /// Provider class for Manager classes
    /// </summary>
    internal class ManagerProvider
    {
        public IActivityManager ActivityManager { get; set; }
        public IEventDateManager EventDateManager { get; set; }
        public IEventManager EventManager { get; set; }
        public ILocationManager LocationManager { get; set; }
        public ISublocationManager SublocationManager { get; set; }
        public ISupplierManager SupplierManager { get; set; }
        public ITaskManager TaskManager { get; set; }
        public IUserManager UserManager { get; set; }
        public IVolunteerManager VolunteerManager { get; set; }
        public IVolunteerRequestManager VolunteerRequestManager { get; set; }
        public IServiceManager ServiceManager { get; set; }
        public IParkingLotManager ParkingLotManager { get; set; }
        public IImageHelper ImageHelper { get; set; }
        public IVolunteerSkillSetManager VolunteerSkillSetManager { get; set; }
        public IUserImageManager UserImageManager { get; set; }
        public IVolunteerReviewManager VolunteerReviewManager { get; set; }

        public ManagerProvider()
        {
            // Live versions here
            ActivityManager = new ActivityManager();
            EventDateManager = new EventDateManager();
            EventManager = new EventManager();
            LocationManager = new LocationManager();
            SublocationManager = new SublocationManager();
            SupplierManager = new SupplierManager();
            TaskManager = new TaskManager();
            UserManager = new UserManager();
            VolunteerManager = new VolunteerManager();
            VolunteerRequestManager = new VolunteerRequestManager();
            ServiceManager = new ServiceManager();
            ParkingLotManager = new ParkingLotManager();
            VolunteerSkillSetManager = new VolunteerSkillSetManager();
            UserImageManager = new UserImageManager();
            VolunteerReviewManager = new VolunteerReviewManager();


            // Fake versions here
            //ActivityManager = new ActivityManager(new ActivityAccessorFake(), new EventDateAccessorFake(), new SublocationAccessorFake(), new ActivityResultAccessorFake());
            //EventDateManager = new EventDateManager(new EventDateAccessorFake());
            //EventManager = new EventManager(new EventAccessorFake());
            //LocationManager = new LocationManager(new LocationAccessorFake());
            //SublocationManager = new SublocationManager(new SublocationAccessorFake());
            //SupplierManager = new SupplierManager(new SupplierAccessorFake());
            //TaskManager = new TaskManager(new TaskAccessorFakes());
            //UserManager = new UserManager(new UserAccessorFake());
            //VolunteerManager = new VolunteerManager(new VolunteerAccessorFake());
            //VolunteerRequestManager = new VolunteerRequestManager(new VolunteerRequestAccessorFake());
            //ServiceManager = new ServiceManager(new ServiceAccessorFake());
            //ParkingLotManager = new ParkingLotManager(new ParkingLotAccessorFake());
            //VolunteerSkillSetManager = new VolunteerSkillSetManager(new VolunteerSkillSetAccessorFake());
            //UserImageManager = new UserImageManager(new UserImageAccessorFake());
            //VolunteerReviewManager = new VolunteerReviewManager(new VolunteerReviewAccessorFake());

            ImageHelper = new ImageHelperDevelopment();
        }
    }
}
