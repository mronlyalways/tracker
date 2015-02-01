using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerDemo.Message
{
    public class ResultNotification<T> : INotification
    {
        public object Content { get; set; }
        public string Title { get; set; }
        public T Result { get; set; }
    }
}
