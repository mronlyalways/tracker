using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerDemo.ViewModel;

namespace TrackerDemo
{
    class ResourceLocator : IDisposable
    {
        private IKernel kernel;

        public ResourceLocator()
        {
            kernel = new StandardKernel();

            kernel.Bind<MainViewModel>().ToSelf().InSingletonScope();
        }

        public T GetInstance<T>()
        {
            return kernel.Get<T>();
        }

        public MainViewModel Main
        {
            get
            {
                return kernel.Get<MainViewModel>();
            }
        }

        public void Dispose()
        {
            if (kernel != null)
                kernel.Dispose();
        }
    }
}
