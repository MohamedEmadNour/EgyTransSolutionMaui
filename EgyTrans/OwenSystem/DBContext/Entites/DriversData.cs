using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EgyTrans.OwenSystem.DBContext.Entites
{
    public class DriverData : IIdentifiable
    {
        [PrimaryKey, AutoIncrement]
        public int DriverDataID { get; set; }
        public string DriverName { get; set; }
        public int DriverPhone { get; set; }
        public int DriverPrice { get; set; }
        public int DriverTeps { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public object GetId() => DriverDataID;
    }
}
