using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyTrans.OwenSystem.DBContext.Entites
{
    public class TourGuideClass : IIdentifiable
    {
        [PrimaryKey , AutoIncrement]
        public int GuideID { get; set; }
        public string GuideName { get; set; }

        public int Price { get; set; }
        public int Telphne { get; set; }

        public string Language { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public object GetId() => GuideID;
    }
}
