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
            serviceCollection.AddSingleton<ScoringService>();
            serviceCollection.AddSingleton<GameViewModel>();

            var container = serviceCollection.BuildServiceProvider();
            IOCContainer.SetContainer(container);
        }
    }
}
