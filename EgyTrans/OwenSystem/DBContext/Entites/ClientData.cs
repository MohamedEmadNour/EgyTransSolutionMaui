using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace EgyTrans.OwenSystem.DBContext.Entites
{
    public class ClientData : IIdentifiable
    {
        [Key]
        public int ClientID { get; set; }
        public string ClientName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string TypeOfContact { get; set; }
        public string Country { get; set; }
        public int PersonsCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public object GetId() => ClientID;

    }
}
