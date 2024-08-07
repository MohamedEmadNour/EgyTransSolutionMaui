using SQLite;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EgyTrans.OwenSystem.DBContext.Entites
{
    public class CarData : IIdentifiable
    {
        [PrimaryKey, AutoIncrement]
        public int CarID { get; set; }
        public string CarPrand { get; set; }
        public string CarModel { get; set; }
        public string CarNum { get; set; }
        public int CarPrincebyDay { get; set; }
        public int CarPrincebyHour { get; set; }
        public string CarDescription { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public object GetId() => CarID;
    }
}
