using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using WeddingCD.DAL.Entities;
using WeddingCD.DAL.Mappings;

namespace WeddingCD.DAL.Context
{
    /// <summary>
    /// The WeddingCD model class.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "No description provided for DbSets.")]
    [DbConfigurationType(typeof(DataContextConfiguration))]
    public partial class WeddingCDModel : DbContext
    {
        /// <summary>
        /// Initializes static members of the <see cref="WeddingCDModel"/> class.
        /// </summary>
        static WeddingCDModel()
        {
            Database.SetInitializer<WeddingCDModel>(null);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeddingCDModel" /> class.
        /// </summary>
        /// <param name="nameOrConnectionString">Either the database name or a connection string.</param>
        public WeddingCDModel(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeddingCDModel" /> class.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        public WeddingCDModel(DbConnection connection)
            : base(connection, true)
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        #region DbSets

        public DbSet<Category> Categories { get; set; }

        #endregion

        /// <summary>
        /// This method is called when the model for a derived context has been initialized, but
        /// before the model has been locked down and used to initialize the context.  The default
        /// implementation of this method does nothing, but it can be overridden in a derived class
        /// such that the model can be further configured before it is locked down.
        /// </summary>
        /// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
        /// <remarks>
        /// Typically, this method is called only once when the first instance of a derived context
        /// is created.  The model for that context is then cached and is for all further instances of
        /// the context in the app domain.  This caching can be disabled by setting the ModelCaching
        /// property on the given ModelBuidler, but note that this can seriously degrade performance.
        /// More control over caching is provided through use of the DbModelBuilder and DbContextFactory
        /// classes directly.
        /// </remarks>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Adds every mapping configuration here
            modelBuilder.Configurations.Add(new CategoryMap());
        }
    }
}