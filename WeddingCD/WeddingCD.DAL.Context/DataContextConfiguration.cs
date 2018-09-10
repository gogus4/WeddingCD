using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace WeddingCD.DAL.Context
{
    /// <summary>
    /// DataContext configuration class.
    /// </summary>
    public class DataContextConfiguration : DbConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataContextConfiguration" /> class.
        /// </summary>
        public DataContextConfiguration()
        {
            this.SetExecutionStrategy(
                "System.Data.SqlClient",
                () => SuspendExecutionStrategy
                    ? (IDbExecutionStrategy)new DefaultExecutionStrategy()
                    : new SqlAzureExecutionStrategy());
        }

        /// <summary>
        /// Gets or sets a value indicating whether the execution strategy is suspended.
        /// </summary>
        /// <value>
        /// <c>true</c> if the execution strategy is suspended; otherwise, <c>false</c>.
        /// </value>
        public static bool SuspendExecutionStrategy
        {
            get
            {
                return (bool?)CallContext.LogicalGetData("SuspendExecutionStrategy") ?? false;
            }

            set
            {
                CallContext.LogicalSetData("SuspendExecutionStrategy", value);
            }
        }
    }
}