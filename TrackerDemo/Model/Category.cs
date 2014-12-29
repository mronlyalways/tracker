using System.Collections.Generic;

namespace TrackerDemo.Model
{
    class Category
    {
        public string Name
        {
            get;
            set;
        }

        public string Unit
        {
            get;
            set;
        }

        public IList<Element> Elements
        {
            get;
            set;
        }
    }
}
