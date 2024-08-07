
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyTransDb.OwenSystem.DBContext.Entites
{
    public class SupplierData : IIdentifiable
    {

        public int SupplieID { get; set; }
        public string SupplierName { get; set; }
        public int SupplierPhone { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public object GetId() => SupplieID;
    }
}
