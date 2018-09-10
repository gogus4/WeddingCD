using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace WeddingCD.Common.Infrastructure
{
    /// <summary>
    /// Represents a transaction
    /// </summary>
    public class Transaction : IDisposable
    {
        /// <summary>
        /// The transaction scope
        /// </summary>
        private TransactionScope transactionScope;

        /// <summary>
        /// Initializes a new instance of the <see cref="Transaction" /> class.
        /// </summary>
        /// <param name="isAsync">if set to <c>true</c> the transaction can be used in an async context.</param>
        /// <param name="isolationLevel">The isolation level.</param>
        /// <param name="scopeOptions">The transaction scope options.</param>
        public Transaction(bool isAsync, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, TransactionScopeOption scopeOptions = TransactionScopeOption.Required)
        {
            // Lower transaction level than the default one.
            if (isAsync)
            {
                this.transactionScope = new TransactionScope(
                    scopeOptions,
                    new TransactionOptions
                    {
                        IsolationLevel = isolationLevel
                    },
                    TransactionScopeAsyncFlowOption.Enabled);
            }
            else
            {
                this.transactionScope = new TransactionScope(
                    scopeOptions,
                    new TransactionOptions
                    {
                        IsolationLevel = isolationLevel
                    });
            }
        }

        /// <summary>
        /// Indicates that all operations within the scope are completed successfully.
        /// </summary>
        public void Complete()
        {
            if (this.transactionScope != null)
            {
                this.transactionScope.Complete();
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public virtual void Dispose()
        {
            if (this.transactionScope != null)
            {
                this.transactionScope.Dispose();
                this.transactionScope = null;
            }
        }
    }
}