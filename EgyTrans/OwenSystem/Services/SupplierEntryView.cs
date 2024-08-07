using EgyTrans.OwenSystem.DBContext.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EgyTrans.OwenSystem.Services
{
    public class SupplierEntryView : StackLayout
    {
        private Entry _supplierNameEntry;
        private Entry _supplierPhoneEntry;

        public SupplierEntryView()
        {
            _supplierNameEntry = new Entry { Placeholder = "Supplier Name" };
            _supplierPhoneEntry = new Entry { Placeholder = "Supplier Phone", Keyboard = Keyboard.Telephone };

            Children.Add(_supplierNameEntry);
            Children.Add(_supplierPhoneEntry);
        }

        public SupplierData GetSupplierData()
        {
            return new SupplierData
            {
                SupplierName = _supplierNameEntry.Text,
                SupplierPhone = int.Parse(_supplierPhoneEntry.Text)
            };
        }
    }
}
