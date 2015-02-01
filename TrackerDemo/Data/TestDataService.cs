using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using TrackerDemo.Model;

namespace TrackerDemo.Data
{
    class TestDataService : IDataService
    {
        private IList<Category> categories;

        public TestDataService()
        {
            categories = new List<Category>();
            var c1 = new Category("Weight", "kg", 0, new SolidColorBrush(Colors.AliceBlue));
            var c2 = new Category("Pushups", "reps", 0, new SolidColorBrush(Colors.LightSeaGreen));
            var e1 = new Element(78, DateTime.Now);
            var e2 = new Element(79, DateTime.Now);
            var e3 = new Element(10, DateTime.Now);
            var e4 = new Element(9, DateTime.Now);
            c1.Elements.Add(e1);
            c1.Elements.Add(e2);
            c2.Elements.Add(e3);
            c2.Elements.Add(e4);
            categories.Add(c1);
            categories.Add(c2);
        }

        public IEnumerable<Category> Load()
        {
            return categories;
        }

        public void Persist(Category c)
        {
            categories.Add(c);
        }
    }
}
