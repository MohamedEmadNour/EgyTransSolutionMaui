using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyTrans.OwenSystem.DBContext.Entites
{
    public class TravelInfo : IIdentifiable
    {
        [PrimaryKey , AutoIncrement]
        public int TravelInfoID {  get; set; }
        public string TransferFrom { get; set; }
        public string TransferTo { get; set; }

        public DateTime AtDay  { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public object GetId() => TravelInfoID;
    }
}
