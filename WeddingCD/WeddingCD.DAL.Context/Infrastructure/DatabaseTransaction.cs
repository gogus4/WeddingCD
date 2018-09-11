using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace WeddingCD.DAL.Context.Infrastructure
{
    /// <summary>
    /// Represents a transaction for the database
    /// </summary>
    public class DatabaseTransaction : WeddingCD.Common.Infrastructure.Transaction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseTransaction" /> class.
        /// </summary>
        /// <param name="isAsync">if set to <c>true</c> the transaction can be used in an async context.</param>
        /// <param name="isolationLevel">The isolation level.</param>
        /// <param name="scopeOptions">The transaction scope options.</param>
        public DatabaseTransaction(bool isAsync, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, TransactionScopeOption scopeOptions = TransactionScopeOption.Required)
            : base(isAsync, isolationLevel, scopeOptions)
        {
            DataContextConfiguration.SuspendExecutionStrategy = true;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();

            DataContextConfiguration.SuspendExecutionStrategy = false;
        }
    }
}