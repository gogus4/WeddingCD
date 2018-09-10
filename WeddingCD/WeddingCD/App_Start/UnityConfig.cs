using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using WeddingCD.IoC;

namespace WeddingCD.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        /// <summary>
        /// The container
        /// </summary>
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        /// <returns>
        /// The configured container
        /// </returns>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or API controllers (unless you want to
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            UnityContainerConfigurator.ConfigureAzure(container);
            UnityContainerConfigurator.Configure(container, new PerRequestLifetimeManager());

            // Other configurations for interfaces that are internal to the current assembly
        }
    }
}
