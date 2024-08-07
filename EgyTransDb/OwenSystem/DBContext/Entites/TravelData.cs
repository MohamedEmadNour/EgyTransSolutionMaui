
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyTransDb.OwenSystem.DBContext.Entites
{
    public class TravelData : IIdentifiable
    {

        public int TravelID { get; set; }
        public int ClientID { get; set; }
        public int TypeID { get; set; }
        public DateTime TransferDateTime { get; set; } = DateTime.Now;
        public decimal NetCost { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal Profit { get; set; }
        public string Status { get; set; }



        public List<CarData> Cars { get; set; }


        public List<DriverData> Drivers { get; set; }


        public List<SupplierData> Suppliers { get; set; }


        public List<TourGuideClass> TourGuides { get; set; }


        public ClientData Client { get; set; }


        public ClientTypeVisit Type { get; set; }


        public List<TravelInfo> TravelInfos { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public object GetId() => TravelID;

    }
}
