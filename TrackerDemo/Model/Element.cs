using GalaSoft.MvvmLight;
using System;

namespace TrackerDemo.Model
{
    public class Element : ObservableObject
    {
        public Element() { }

        public Element(double value, DateTime date)
        {
            Value = value;
            Date = date;
        }

        private double _value;
        public double Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                RaisePropertyChanged(() => Value);
            }
        }

        private DateTime date;
        public DateTime Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
                RaisePropertyChanged(() => Date);
            }
        }

        public override bool Equals(Object obj)
        {
            if (obj != null && obj is Element)
            {
                var tmp = obj as Element;
                return Value == tmp.Value && Date.Equals(tmp.Date);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return (int) Value + Date.DayOfYear + Date.Year;
        }
    }
}
