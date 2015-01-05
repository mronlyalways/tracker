using System;

namespace TrackerDemo.Model
{
    public class Element
    {
        public Element() { }

        public Element(double value, DateTime date)
        {
            Value = value;
            Date = date;
        }

        public double Value { get; set; }

        public DateTime Date { get; set; }
    }
}
