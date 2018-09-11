using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingCD.Configuration;
using WeddingCD.DAL.Context;

namespace WeddingCD.Business
{
    /// <summary>
    /// Base class for XXXManagement classes.
    /// </summary>
    public abstract partial class BaseManagement
    {
        /// <summary>
        /// The database context
        /// </summary>
        private readonly WeddingCDDbContext dbContext;

        /// <summary>
        /// The configuration instance
        /// </summary>
        private readonly IConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseManagement" /> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        protected BaseManagement(WeddingCDDbContext dbContext, IConfiguration configuration)
        {
            this.dbContext = dbContext;
            this.configuration = configuration;
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        protected IConfiguration Configuration
        {
            get
            {
                return this.configuration;
            }
        }

        /// <summary>
        /// Gets the database context.
        /// </summary>
        /// <value>
        /// The database context.
        /// </value>
        protected WeddingCDDbContext DbContext
        {
            get
            {
                return this.dbContext;
            }
        }
    }
}