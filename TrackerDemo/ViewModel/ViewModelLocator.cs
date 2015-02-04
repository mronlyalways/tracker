using Microsoft.Practices.ServiceLocation;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerDemo.Data;
using TrackerDemo.View;
using TrackerDemo.ViewModel;

namespace TrackerDemo.ViewModel
{
    public class ViewModelLocator : IDisposable
    {
        private static IKernel kernel;

        static ViewModelLocator()
        {
            kernel = new StandardKernel();
            kernel.Bind<IDataService>().To<DesignTimeDataService>().InSingletonScope();
            kernel.Bind<NotificationViewModel>().ToSelf().InSingletonScope();
            kernel.Bind<ChromeViewModel>().ToSelf().InSingletonScope();
            kernel.Bind<HomeViewModel>().ToSelf().InSingletonScope();
            kernel.Bind<NewCategoryViewModel>().ToSelf().InSingletonScope();
            kernel.Bind<NewElementViewModel>().ToSelf().InSingletonScope();
        }

        public ChromeViewModel Chrome
        {
            get
            {
                return kernel.Get<ChromeViewModel>();
            }
        }

        public HomeViewModel Home
        {
            get
            {
                return kernel.Get<HomeViewModel>();
            }
        }

        public NewCategoryViewModel NewCategory
        {
            get
            {
                return kernel.Get<NewCategoryViewModel>();
            }
        }

        public NewElementViewModel NewElement
        {
            get
            {
                return kernel.Get<NewElementViewModel>();
            }
        }

        public NotificationViewModel Notification
        {
            get
            {
                return kernel.Get<NotificationViewModel>();
            }
        }

        public void Dispose()
        {
            if (kernel != null)
                kernel.Dispose();
        }
    }
}
