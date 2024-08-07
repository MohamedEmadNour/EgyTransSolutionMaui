
using EgyTrans.OwenSystem.DBContext.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EgyTrans.OwenSystem.Services
{
    public class CarEntryView : StackLayout
    {
        private Entry _carBrandEntry;
        private Entry _carModelEntry;
        private Entry _carNumEntry;
        private Entry _carPriceByDayEntry;
        private Entry _carPriceByHourEntry;
        private Entry _carDescriptionEntry;

        public CarEntryView()
        {
            _carBrandEntry = new Entry { Placeholder = "Car Brand" };
            _carModelEntry = new Entry { Placeholder = "Car Model" };
            _carNumEntry = new Entry { Placeholder = "Car Number" };
            _carPriceByDayEntry = new Entry { Placeholder = "Price by Day", Keyboard = Keyboard.Numeric };
            _carPriceByHourEntry = new Entry { Placeholder = "Price by Hour", Keyboard = Keyboard.Numeric };
            _carDescriptionEntry = new Entry { Placeholder = "Description" };

            Children.Add(_carBrandEntry);
            Children.Add(_carModelEntry);
            Children.Add(_carNumEntry);
            Children.Add(_carPriceByDayEntry);
            Children.Add(_carPriceByHourEntry);
            Children.Add(_carDescriptionEntry);
        }

        public CarData GetCarData()
        {
            return new CarData
            {
                CarPrand = _carBrandEntry.Text,
                CarModel = _carModelEntry.Text,
                CarNum = _carNumEntry.Text,
                CarPrincebyDay = int.Parse(_carPriceByDayEntry.Text),
                CarPrincebyHour = int.Parse(_carPriceByHourEntry.Text),
                CarDescription = _carDescriptionEntry.Text
            };
        }
    }
}
