using EgyTrans.OwenSystem.DBContext.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyTrans.OwenSystem.Services
{
    public class TravelInfoEntryView : StackLayout
    {
        private Entry _transferFromEntry;
        private Entry _transferToEntry;

        public TravelInfoEntryView()
        {
            _transferFromEntry = new Entry { Placeholder = "Transfer From" };
            _transferToEntry = new Entry { Placeholder = "Transfer To" };

            Children.Add(_transferFromEntry);
            Children.Add(_transferToEntry);
        }

        public TravelInfo GetTravelInfo()
        {
            return new TravelInfo
            {
                TransferFrom = _transferFromEntry.Text,
                TransferTo = _transferToEntry.Text
            };
        }
    }
}
