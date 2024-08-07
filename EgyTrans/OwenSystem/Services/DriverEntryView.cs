using EgyTrans.OwenSystem.DBContext.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EgyTrans.OwenSystem.Services
{
    public class DriverEntryView : StackLayout
    {
        private Entry _driverNameEntry;
        private Entry _driverPhoneEntry;
        private Entry _driverPriceEntry;
        private Entry _driverTipsEntry;

        public DriverEntryView()
        {
            _driverNameEntry = new Entry { Placeholder = "Driver Name" };
            _driverPhoneEntry = new Entry { Placeholder = "Driver Phone", Keyboard = Keyboard.Telephone };
            _driverPriceEntry = new Entry { Placeholder = "Driver Price", Keyboard = Keyboard.Numeric };
            _driverTipsEntry = new Entry { Placeholder = "Driver Tips", Keyboard = Keyboard.Numeric };

            Children.Add(_driverNameEntry);
            Children.Add(_driverPhoneEntry);
            Children.Add(_driverPriceEntry);
            Children.Add(_driverTipsEntry);
        }

        public DriverData GetDriverData()
        {
            return new DriverData
            {
                DriverName = _driverNameEntry.Text,
                DriverPhone = int.Parse(_driverPhoneEntry.Text),
                DriverPrice = int.Parse(_driverPriceEntry.Text),
                DriverTeps = int.Parse(_driverTipsEntry.Text)
            };
        }
    }
}
