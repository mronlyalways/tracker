using System.Collections.Generic;
using System.Windows.Media;

namespace TrackerDemo.Model
{
    public class Category
    {
        public Category()
        {
            Elements = new List<Element>();
        }

        public Category(string name, string unit, SolidColorBrush color) : this()
        {
            Name = name;
            Unit = unit;
            Color = color;
        }

        public string Name { get; set; }

        public string Unit { get; set; }

        public SolidColorBrush Color { get; set; }

        public IList<Element> Elements { get; set; }
    }
}
