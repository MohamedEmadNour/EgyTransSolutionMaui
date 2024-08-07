
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyTransDb.OwenSystem.DBContext.Entites
{
    public class ClientTypeVisit : IIdentifiable
    {
 
        public int TypeID { get; set; }
        public string TypeName { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public object GetId() => TypeID;
    }
}
