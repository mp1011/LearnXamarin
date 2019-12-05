using LearnXamarin.Services;
using LearnXamarin.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace LearnXamarin.IOC
{
    class DIRegistrar
    {
        public static void RegisterTypes()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<RandomService>();
            serviceCollection.AddSingleton<GridService>();
            serviceCollection.AddSingleton<GridViewModel>();
            serviceCollection.AddSingleton<GridViewModel>();

            serviceCollection.AddSingleton<MainViewModel>();

            var container = serviceCollection.BuildServiceProvider();
            IOCContainer.SetContainer(container);
        }
    }
}
