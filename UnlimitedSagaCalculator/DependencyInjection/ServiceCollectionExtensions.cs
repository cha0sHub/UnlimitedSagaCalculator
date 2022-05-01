using Microsoft.Extensions.DependencyInjection;
using UnlimitedSagaCalculator.Controllers;
using UnlimitedSagaCalculator.Model;
using UnlimitedSagaCalculator.Logic;
using UnlimitedSagaCalculator.Views;
using UnlimitedSagaCalculator.Configuration;

namespace UnlimitedSagaCalculator.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IMainController, MainController>();
            serviceCollection.AddSingleton<IMainWindow, MainWindow>();
            serviceCollection.AddSingleton<IDamageCalculatorController, DamageCalculatorController>();
            serviceCollection.AddSingleton<IDamageCalculatorView, DamageCalculatorView>();
            serviceCollection.AddSingleton<IUsagaModel, UsagaModel>();
            serviceCollection.AddSingleton<ICharacterStatCalculatorController, CharacterStatCalculatorController>();
            serviceCollection.AddSingleton<ICharacterStatCalculatorView, CharacterStatCalculatorView>();
            serviceCollection.AddSingleton<IWeightCalculatorController, WeightCalculatorController>();
            serviceCollection.AddSingleton<IWeightCalculatorView, WeightCalculatorView>();
            serviceCollection.AddSingleton<IItemCostCalculatorController, ItemCostCalculatorController>();
            serviceCollection.AddSingleton<IItemCostCalculatorView, ItemCostCalculatorView>();
            serviceCollection.AddSingleton<IConfigurationManager, ConfigurationManager>();

            serviceCollection.AddTransient<IHpDamageCalculator, HpDamageCalculator>();
            serviceCollection.AddTransient<ILpDamageCalculator, LpDamageCalculator>();
            serviceCollection.AddTransient<ICharacterStatCalculator, CharacterStatCalculator>();
            serviceCollection.AddTransient<IWeightCalculator, WeightCalculator>();
            serviceCollection.AddTransient<IItemCostCalculator, ItemCostCalculator>();

            return serviceCollection;
        }
    }
}
