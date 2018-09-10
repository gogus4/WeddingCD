using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace WeddingCD.DAL.Context.Infrastructure
{
    /// <summary>
    /// Factory for transaction scopes.
    /// </summary>
    public static class DatabaseTransactionFactory
    {
        /// <summary>
        /// Starts the new transaction.
        /// </summary>
        /// <param name="isolationLevel">The isolation level.</param>
        /// <returns>
        /// The transaction scope.
        /// </returns>
        public static WeddingCD.Common.Infrastructure.Transaction StartNewTransactionForAsync(System.Data.IsolationLevel isolationLevel = System.Data.IsolationLevel.ReadCommitted)
        {
            System.Transactions.IsolationLevel mappedLevel = IsolationLevel.Unspecified;
            switch (isolationLevel)
            {
                case System.Data.IsolationLevel.Chaos:
                    mappedLevel = IsolationLevel.Chaos;
                    break;

                case System.Data.IsolationLevel.ReadCommitted:
                    mappedLevel = IsolationLevel.ReadCommitted;
                    break;

                case System.Data.IsolationLevel.ReadUncommitted:
                    mappedLevel = IsolationLevel.ReadUncommitted;
                    break;

                case System.Data.IsolationLevel.RepeatableRead:
                    mappedLevel = IsolationLevel.RepeatableRead;
                    break;

                case System.Data.IsolationLevel.Serializable:
                    mappedLevel = IsolationLevel.Serializable;
                    break;

                case System.Data.IsolationLevel.Snapshot:
                    mappedLevel = IsolationLevel.Snapshot;
                    break;
            }

            return new DatabaseTransaction(true, mappedLevel);
        }
    }
}