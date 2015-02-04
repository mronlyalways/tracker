using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerDemo.Model;

namespace TrackerDemo.Data
{
    public interface IDataService
    {
        IEnumerable<Category> Load();

        void Persist(Category c);
    }
}
