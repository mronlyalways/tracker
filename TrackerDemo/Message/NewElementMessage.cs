using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerDemo.Model;

namespace TrackerDemo.Message
{
    public class NewElementMessage
    {
        public Element Element { get; set; }

        public NewElementMessage(Element element)
        {
            Element = element;
        }
    }
}
