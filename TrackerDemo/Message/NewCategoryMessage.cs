using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerDemo.Model;

namespace TrackerDemo.Message
{
    public class NewCategoryMessage
    {
        public Category Category { get; set; }

        public NewCategoryMessage(Category category)
        {
            Category = category;
        }
    }
}
