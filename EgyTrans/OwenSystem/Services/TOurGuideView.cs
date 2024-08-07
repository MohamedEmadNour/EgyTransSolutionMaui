using EgyTrans.OwenSystem.DBContext.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyTrans.OwenSystem.Services
{
    public class TOurGuideView : StackLayout
    {

        private Entry _GuiderNameEntry;
        private Entry _GuiderPhoneEntry;
        private Entry _GuidePriceEntry;
        private Entry _GuideLanguageEntry;

        public TOurGuideView()
        {
            _GuiderNameEntry = new Entry { Placeholder = "Guide Name" };
            _GuiderPhoneEntry = new Entry { Placeholder = "Guide Phone", Keyboard = Keyboard.Telephone };
            _GuidePriceEntry = new Entry { Placeholder = "Guide Price", Keyboard = Keyboard.Numeric };
            _GuideLanguageEntry = new Entry { Placeholder = "Guide Language"};

            Children.Add(_GuiderNameEntry);
            Children.Add(_GuiderPhoneEntry);
            Children.Add(_GuidePriceEntry);
            Children.Add(_GuideLanguageEntry);
        }

        public TourGuideClass GetTOurGuideData()
        {
            return new TourGuideClass
            {
                GuideName = _GuiderNameEntry.Text,
                Language = _GuideLanguageEntry.Text,
                Price = int.Parse(_GuidePriceEntry.Text),
                Telphne = int.Parse(_GuiderPhoneEntry.Text)
            };
        }
    }
}
