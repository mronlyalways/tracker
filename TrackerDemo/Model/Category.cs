using System.Collections.Generic;

namespace TrackerDemo.Model
{
    public class Category
    {
        public Category()
        {
            Elements = new List<Element>();
        }

        public Category(string name, string unit) : this()
        {
            Name = name;
            Unit = unit;
        }

        public string Name { get; set; }

        public string Unit { get; set; }

        public IList<Element> Elements { get; set; }
    }
}
