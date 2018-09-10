using Microsoft.Practices.Unity;

namespace WeddingCD.IoC
{
    /// <summary>
    /// Configuration class for Unity.
    /// </summary>
    public class UnityContainerConfigurator
    {
        /// <summary>
        /// Configures the specified unity container.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="dbContextLifetimeManager">The database context lifetime manager.</param>
        public static void Configure(IUnityContainer container, LifetimeManager dbContextLifetimeManager)
        {
            //// Gets the DB context configuration
            //var dbContextParam = container.Resolve<IConfiguration>().GetString(ConfigurationKey.DatabaseConnectionString);
            //container.RegisterType<ThermiboxDbContext, ThermiboxDbContext>(dbContextLifetimeManager);

            //// Logging
            //container.RegisterType<ILogFactory, LogFactory>();
            //container.RegisterType<ILogReader, AzureDbLogReader>();

            //// Web
            //container.RegisterType<IAuth, FormsAuthWrapper>();
            //container.RegisterType<IWebService, WebServiceImplementation>();
            //container.RegisterType<ISigfoxQualityService, SigfoxQualityService>();

            //// Logic
            //container.RegisterType<IProcessingPipe, ProcessingPipe>();
            //container.RegisterType<IIntentDataSender, IntentDataSender>();
            //container.RegisterType<IIntentClient, IntentClient>();
            //container.RegisterType<IIntentStreamClient, IntentStreamClient>();
            //container.RegisterType<IIntentManagement, IntentManagement>();

            //// Map
            //container.RegisterType<IPathMapper, ServerPathMapper>();

            //// Business layer
            ////container.RegisterType<IChaudiereManagement, ChaudiereManagementClient>();                      // Proxifyed

            //// Do NOT proxify this !!! It is ONLY used by the Web API DAL/BL, so it's OK to directly access the DB
            //// without the need of a proxy
            //container.RegisterType<IBusinessTokenAuthenticationManagement, BusinessTokenAuthenticationManagement>();
        }

        /// <summary>
        /// Configures the specified unity container.
        /// </summary>
        /// <param name="container">The container.</param>
        public static void ConfigureAzure(IUnityContainer container)
        {
            //container.RegisterInstance<IConfiguration>(new AzureConfiguration());
            //container.RegisterType<IQueue, ServiceBusQueue>();
            //container.RegisterType<IEmailSender, QueueEmailSender>();
            //container.RegisterType<IPersistentStorage, Thermibox.PersistentStorage.Azure.PersistentStorage>();
        }
    }
}